using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using BusManagementSystem.Data;
using BusManagementSystem.Models;
using BusManagementSystem.Controllers;

namespace BusManagementSystem.Controllers
{
    public class TripsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TripsController(ApplicationDbContext context)
        {
            _context = context;
        }

        private bool IsAdmin() =>
            HttpContext.Session.GetString(AdminController.AdminSessionKey) == "true";

        private IActionResult? RedirectIfNotAdmin()
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Login", "Admin");
            }
            return null;
        }

        // GET: Trips
        public async Task<IActionResult> Index()
        {
            var trips = _context.Trips
                .Include(t => t.Bus)
                .Include(t => t.Route);

            return View(await trips.ToListAsync());
        }

        // GET: Trips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var trip = await _context.Trips
                .Include(t => t.Bus)
                .Include(t => t.Route)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (trip == null) return NotFound();

            return View(trip);
        }

        // GET: Trips/Create
        // Controller Create action
        public IActionResult Create()
        {
            var guard = RedirectIfNotAdmin();
            if (guard != null) return guard;

            ViewBag.BusId = new SelectList(_context.Buses, "Id", "RegistrationNumber");
            ViewBag.RouteId = new SelectList(_context.BusRoutes.Select(r => new { r.Id, DisplayText = $"{r.FromCity} → {r.ToCity}" }), "Id", "DisplayText");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BusId,RouteId,DepartureTime,ArrivalTime")] Trip trip)
        {
            var guard = RedirectIfNotAdmin();
            if (guard != null) return guard;
            if (!ModelState.IsValid)
            {
                // Log validation errors to Output window
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine("Validation Error: " + error.ErrorMessage);
                }

                // Refill dropdowns when ModelState fails
                ViewBag.BusId = new SelectList(_context.Buses, "Id", "RegistrationNumber", trip.BusId);
                ViewBag.RouteId = new SelectList(_context.BusRoutes.Select(r => new { r.Id, DisplayText = $"{r.FromCity} → {r.ToCity}" }), "Id", "DisplayText", trip.RouteId);

                return View(trip);
            }

            _context.Add(trip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        // GET: Trips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var guard = RedirectIfNotAdmin();
            if (guard != null) return guard;
            if (id == null) return NotFound();

            var trip = await _context.Trips.FindAsync(id);
            if (trip == null) return NotFound();

            // ADD DROPDOWNS HERE
            ViewBag.BusId = new SelectList(_context.Buses, "Id", "RegistrationNumber", trip.BusId);
            ViewBag.RouteId = new SelectList(_context.BusRoutes.Select(r => new { r.Id, DisplayText = $"{r.FromCity} → {r.ToCity}" }), "Id", "DisplayText", trip.RouteId);

            return View(trip);
        }

        // POST: Trips/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BusId,RouteId,DepartureTime,ArrivalTime")] Trip trip)
        {
            var guard = RedirectIfNotAdmin();
            if (guard != null) return guard;
            if (id != trip.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripExists(trip.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            // Refill dropdowns on failure
            ViewBag.BusId = new SelectList(_context.Buses, "Id", "RegistrationNumber", trip.BusId);
            ViewBag.RouteId = new SelectList(_context.BusRoutes.Select(r => new { r.Id, DisplayText = $"{r.FromCity} → {r.ToCity}" }), "Id", "DisplayText", trip.RouteId);

            return View(trip);
        }

        // GET: Trips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var guard = RedirectIfNotAdmin();
            if (guard != null) return guard;
            if (id == null) return NotFound();

            var trip = await _context.Trips
                .Include(t => t.Bus)
                .Include(t => t.Route)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (trip == null) return NotFound();

            return View(trip);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guard = RedirectIfNotAdmin();
            if (guard != null) return guard;
            var trip = await _context.Trips.FindAsync(id);
            if (trip != null) _context.Trips.Remove(trip);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripExists(int id)
        {
            return _context.Trips.Any(e => e.Id == id);
        }
    }
}
