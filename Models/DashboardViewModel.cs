using System;
using System.Collections.Generic;

namespace BusManagementSystem.Models
{
    public class DashboardViewModel
    {
        public IList<UpcomingTripItem> UpcomingTrips { get; set; } = new List<UpcomingTripItem>();
        public IList<RouteOverviewItem> Routes { get; set; } = new List<RouteOverviewItem>();
        public IList<BusSeatsItem> BusSeats { get; set; } = new List<BusSeatsItem>();
        public IList<BusRouteAssignmentItem> BusRouteAssignments { get; set; } = new List<BusRouteAssignmentItem>();
    }

    public class UpcomingTripItem
    {
        public int TripId { get; set; }
        public string RouteName { get; set; } = string.Empty;
        public string BusLabel { get; set; } = string.Empty;
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int Capacity { get; set; }
        public int BookedSeats { get; set; }
    }

    public class RouteOverviewItem
    {
        public int RouteId { get; set; }
        public string RouteName { get; set; } = string.Empty;
        public int TripCountToday { get; set; }
        public int TotalTrips { get; set; }
        public int ApproxBookedSeats { get; set; }
    }

    public class BusSeatsItem
    {
        public int BusId { get; set; }
        public string BusLabel { get; set; } = string.Empty;
        public string RouteName { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public int BookedSeats { get; set; }
        public int FreeSeats { get; set; }
        public double Utilization { get; set; }
    }

    public class BusRouteAssignmentItem
    {
        public int BusId { get; set; }
        public string BusLabel { get; set; } = string.Empty;
        public string RouteName { get; set; } = string.Empty;
        public DateTime? NextDeparture { get; set; }
        public int FreeSeats { get; set; }
    }
}


