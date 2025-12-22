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
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
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

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var guard = RedirectIfNotAdmin();
            if (guard != null) return guard;

            return View(await _context.Bookings.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var guard = RedirectIfNotAdmin();
            if (guard != null) return guard;
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            var guard = RedirectIfNotAdmin();
            if (guard != null) return guard;
            var trips = _context.Trips
                .Include(t => t.Bus)
                .Include(t => t.Route)
                .Select(t => new { 
                    t.Id, 
                    DisplayText = $"Trip #{t.Id} - Bus: {t.Bus.RegistrationNumber} ({t.Route.FromCity} → {t.Route.ToCity})" 
                });
            ViewBag.TipId = new SelectList(trips, "Id", "DisplayText");
            ViewBag.PassengerId = new SelectList(_context.Passengers, "Id", "FullName");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipId,PassengerId,SeatNumber,BookingDate")] Booking booking)
        {
            var guard = RedirectIfNotAdmin();
            if (guard != null) return guard;
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var trips = _context.Trips
                .Include(t => t.Bus)
                .Include(t => t.Route)
                .Select(t => new { 
                    t.Id, 
                    DisplayText = $"Trip #{t.Id} - Bus: {t.Bus.RegistrationNumber} ({t.Route.FromCity} → {t.Route.ToCity})" 
                });
            ViewBag.TipId = new SelectList(trips, "Id", "DisplayText", booking.TipId);
            ViewBag.PassengerId = new SelectList(_context.Passengers, "Id", "FullName", booking.PassengerId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var guard = RedirectIfNotAdmin();
            if (guard != null) return guard;
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            var trips = _context.Trips
                .Include(t => t.Bus)
                .Include(t => t.Route)
                .Select(t => new { 
                    t.Id, 
                    DisplayText = $"Trip #{t.Id} - Bus: {t.Bus.RegistrationNumber} ({t.Route.FromCity} → {t.Route.ToCity})" 
                });
            ViewBag.TipId = new SelectList(trips, "Id", "DisplayText", booking.TipId);
            ViewBag.PassengerId = new SelectList(_context.Passengers, "Id", "FullName", booking.PassengerId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipId,PassengerId,SeatNumber,BookingDate")] Booking booking)
        {
            var guard = RedirectIfNotAdmin();
            if (guard != null) return guard;
            if (id != booking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var trips2 = _context.Trips
                .Include(t => t.Bus)
                .Include(t => t.Route)
                .Select(t => new { 
                    t.Id, 
                    DisplayText = $"Trip #{t.Id} - Bus: {t.Bus.RegistrationNumber} ({t.Route.FromCity} → {t.Route.ToCity})" 
                });
            ViewBag.TipId = new SelectList(trips2, "Id", "DisplayText", booking.TipId);
            ViewBag.PassengerId = new SelectList(_context.Passengers, "Id", "FullName", booking.PassengerId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var guard = RedirectIfNotAdmin();
            if (guard != null) return guard;
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guard = RedirectIfNotAdmin();
            if (guard != null) return guard;
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}
