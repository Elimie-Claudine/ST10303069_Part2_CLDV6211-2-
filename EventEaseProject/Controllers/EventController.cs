using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventEaseProject.Models;

namespace EventEaseProject.Controllers
{
    public class EventController : Controller
    {
        private readonly EventEaseDbContext _context;

        public EventController(EventEaseDbContext context)
        {
            _context = context;
        }

        // GET: Event
        public async Task<IActionResult> Index()
        {
            var eventEaseDbContext = _context.Eventsses.Include(e => e.Venue);
            return View(await eventEaseDbContext.ToListAsync());
        }

        // GET: Event/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventss = await _context.Eventsses
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (eventss == null)
            {
                return NotFound();
            }

            return View(eventss);
        }

        // GET: Event/Create
        public IActionResult Create()
        {
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueId");
            return View();
        }

        // POST: Event/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,EventName,EventDate,EventTime,EventDescription,ImageUrl,VenueId")] Eventss eventss)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventss);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Event successfully created!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueId", eventss.VenueId);
            return View(eventss);
        }

        // GET: Event/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventss = await _context.Eventsses.FindAsync(id);
            if (eventss == null)
            {
                return NotFound();
            }
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueId", eventss.VenueId);
            return View(eventss);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,EventName,EventDate,EventTime,EventDescription,ImageUrl,VenueId")] Eventss eventss)
        {
            if (id != eventss.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventss);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventssExists(eventss.EventId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["SuccessMessage"] = "Event edited successfully!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueId", eventss.VenueId);
            return View(eventss);
        }

        // GET: Event/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventss = await _context.Eventsses
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (eventss == null)
            {
                return NotFound();
            }

            return View(eventss);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventss = await _context.Eventsses.FindAsync(id);

            // Check for active bookings
            bool hasBookings = await _context.Bookings.AnyAsync(b => b.EventId == id);

            if (hasBookings)
            {
                TempData["Error"] = "Sorry, we cannot delete event. There are active bookings for this event.";
                return RedirectToAction(nameof(Index));
            }

            if (eventss != null)
            {
                _context.Eventsses.Remove(eventss);
                await _context.SaveChangesAsync();
            }
            TempData["SuccessMessage"] = "Event added successfully!";
            return RedirectToAction(nameof(Index));
        }


        private bool EventssExists(int id)
        {
            return _context.Eventsses.Any(e => e.EventId == id);
        }
    }
}
