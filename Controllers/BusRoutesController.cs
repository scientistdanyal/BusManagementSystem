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
    public class BusRoutesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BusRoutesController(ApplicationDbContext context)
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

        // GET: BusRoutes
        public async Task<IActionResult> Index()
        {
            return View(await _context.BusRoutes.ToListAsync());
        }

        // GET: BusRoutes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busRoute = await _context.BusRoutes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (busRoute == null)
            {
                return NotFound();
            }

            return View(busRoute);
        }

        // GET: BusRoutes/Create
        public IActionResult Create()
        {
            var guard = RedirectIfNotAdmin();
            if (guard != null) return guard;

            return View();
        }

        // POST: BusRoutes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FromCity,ToCity,DistanceKm")] BusRoute busRoute)
        {
            var guard = RedirectIfNotAdmin();
            if (guard != null) return guard;

            if (ModelState.IsValid)
            {
                _context.Add(busRoute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(busRoute);
        }

        // GET: BusRoutes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var guard = RedirectIfNotAdmin();
            if (guard != null) return guard;

            if (id == null)
            {
                return NotFound();
            }

            var busRoute = await _context.BusRoutes.FindAsync(id);
            if (busRoute == null)
            {
                return NotFound();
            }
            return View(busRoute);
        }

        // POST: BusRoutes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FromCity,ToCity,DistanceKm")] BusRoute busRoute)
        {
            var guard = RedirectIfNotAdmin();
            if (guard != null) return guard;

            if (id != busRoute.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(busRoute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusRouteExists(busRoute.Id))
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
            return View(busRoute);
        }

        // GET: BusRoutes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var guard = RedirectIfNotAdmin();
            if (guard != null) return guard;

            if (id == null)
            {
                return NotFound();
            }

            var busRoute = await _context.BusRoutes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (busRoute == null)
            {
                return NotFound();
            }

            return View(busRoute);
        }

        // POST: BusRoutes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guard = RedirectIfNotAdmin();
            if (guard != null) return guard;
            var busRoute = await _context.BusRoutes.FindAsync(id);
            if (busRoute != null)
            {
                _context.BusRoutes.Remove(busRoute);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusRouteExists(int id)
        {
            return _context.BusRoutes.Any(e => e.Id == id);
        }
    }
}
