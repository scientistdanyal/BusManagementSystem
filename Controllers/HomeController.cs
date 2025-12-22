using System.Diagnostics;
using BusManagementSystem.Data;
using BusManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BusManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var now = DateTime.Now;

            // Base set of upcoming trips we will reuse for multiple widgets
            var upcomingTripsRaw = await _context.Trips
                .Include(t => t.Bus)
                .Include(t => t.Route)
                .Where(t => t.DepartureTime >= now)
                .OrderBy(t => t.DepartureTime)
                .Take(20)
                .ToListAsync();

            var upcomingTripIds = upcomingTripsRaw.Select(t => t.Id).ToList();

            var upcomingBookings = await _context.Bookings
                .Where(b => upcomingTripIds.Contains(b.TipId))
                .GroupBy(b => b.TipId)
                .Select(g => new { TripId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.TripId, x => x.Count);

            int GetBookingsForTrip(int tripId) =>
                upcomingBookings.TryGetValue(tripId, out var count) ? count : 0;

            // Trips Timing widget
            var upcomingTrips = upcomingTripsRaw
                .Select(t => new UpcomingTripItem
                {
                    TripId = t.Id,
                    RouteName = t.Route != null ? $"{t.Route.FromCity} → {t.Route.ToCity}" : "Unknown route",
                    BusLabel = t.Bus?.RegistrationNumber ?? "Bus",
                    DepartureTime = t.DepartureTime,
                    ArrivalTime = t.ArrivalTime,
                    Capacity = t.Bus?.Capacity ?? 0,
                    BookedSeats = GetBookingsForTrip(t.Id)
                })
                .ToList();

            // Cache all bookings for utilization calculations
            var allBookingsLookup = await _context.Bookings
                .GroupBy(b => b.TipId)
                .Select(g => new { TripId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.TripId, x => x.Count);

            int GetTotalBookingsForTrip(int tripId) =>
                allBookingsLookup.TryGetValue(tripId, out var count) ? count : 0;

            // Routes overview widget
            var allTrips = await _context.Trips
                .Include(t => t.Route)
                .ToListAsync();

            var routes = await _context.BusRoutes.ToListAsync();

            var routeItems = routes
                .Select(r =>
                {
                    var routeTrips = allTrips.Where(t => t.RouteId == r.Id).ToList();
                    var routeTripsToday = routeTrips.Where(t => t.DepartureTime.Date == now.Date).ToList();
                    var approxBookedSeats = routeTrips.Sum(t => GetTotalBookingsForTrip(t.Id));

                    return new RouteOverviewItem
                    {
                        RouteId = r.Id,
                        RouteName = $"{r.FromCity} → {r.ToCity}",
                        TripCountToday = routeTripsToday.Count,
                        TotalTrips = routeTrips.Count,
                        ApproxBookedSeats = approxBookedSeats
                    };
                })
                .OrderByDescending(r => r.TripCountToday)
                .ThenByDescending(r => r.TotalTrips)
                .Take(5)
                .ToList();

            // Seats per bus widget
            var busSeatItems = upcomingTripsRaw
                .Where(t => t.Bus != null && t.Route != null)
                .GroupBy(t => new
                {
                    t.BusId,
                    BusLabel = t.Bus!.RegistrationNumber,
                    t.Bus!.Capacity,
                    RouteName = $"{t.Route!.FromCity} → {t.Route!.ToCity}"
                })
                .Select(g =>
                {
                    var booked = g.Sum(t => GetBookingsForTrip(t.Id));
                    var capacity = g.Key.Capacity;
                    var free = capacity > booked ? capacity - booked : 0;
                    var utilization = capacity > 0 ? (double)booked / capacity : 0d;

                    return new BusSeatsItem
                    {
                        BusId = g.Key.BusId,
                        BusLabel = g.Key.BusLabel,
                        RouteName = g.Key.RouteName,
                        Capacity = capacity,
                        BookedSeats = booked,
                        FreeSeats = free,
                        Utilization = utilization
                    };
                })
                .OrderByDescending(b => b.Utilization)
                .ThenBy(b => b.BusLabel)
                .Take(5)
                .ToList();

            // Route per bus widget
            var busAssignments = upcomingTripsRaw
                .Where(t => t.Bus != null && t.Route != null)
                .GroupBy(t => new
                {
                    t.BusId,
                    BusLabel = t.Bus!.RegistrationNumber,
                    RouteName = $"{t.Route!.FromCity} → {t.Route!.ToCity}"
                })
                .Select(g =>
                {
                    var nextTrip = g.OrderBy(t => t.DepartureTime).First();
                    var capacity = nextTrip.Bus?.Capacity ?? 0;
                    var booked = GetBookingsForTrip(nextTrip.Id);
                    var freeSeats = capacity > booked ? capacity - booked : 0;

                    return new BusRouteAssignmentItem
                    {
                        BusId = g.Key.BusId,
                        BusLabel = g.Key.BusLabel,
                        RouteName = g.Key.RouteName,
                        NextDeparture = nextTrip.DepartureTime,
                        FreeSeats = freeSeats
                    };
                })
                .OrderBy(t => t.NextDeparture)
                .Take(5)
                .ToList();

            var viewModel = new DashboardViewModel
            {
                UpcomingTrips = upcomingTrips,
                Routes = routeItems,
                BusSeats = busSeatItems,
                BusRouteAssignments = busAssignments
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
