using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventEaseProject.Models;
using EventEaseProject.Models.ViewModels;


namespace EventEaseProject.Controllers
{
    public class BookingController : Controller
    {
        private readonly EventEaseDbContext _context;

        public BookingController(EventEaseDbContext context)
        {
            _context = context;
        }

        // GET: Booking
        public async Task<IActionResult> Index(string searchString)
        {
            var bookingsQuery = _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .Select(b => new BookingDisplayViewModel
                {
                    BookingId = b.BookingId,
                    EventName = b.Event.EventName,
                    VenueName = b.Venue.VenueName,
                    CustomerEmail = b.CustomerEmail,
                    BookingDate = b.BookingDate
                });

            if (!string.IsNullOrEmpty(searchString))
            {
                bookingsQuery = bookingsQuery.Where(b =>
                    b.BookingId.ToString().Contains(searchString) ||
                    b.EventName.Contains(searchString));
            }

            var bookings = await bookingsQuery.ToListAsync();
            return View(bookings);
        }


        // GET: Booking/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .FirstOrDefaultAsync(m => m.BookingId == id);

            if (booking == null) return NotFound();

            return View(booking);
        }

        // GET: Booking/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Eventsses, "EventId", "EventName");
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingId,VenueName,EventId,CustomerEmail,BookingDate")] Booking booking)
        {
            // 🔎 Lookup VenueId based on VenueName
            var venue = await _context.Venues.FirstOrDefaultAsync(v => v.VenueName == booking.VenueName);
            if (venue == null)
            {
                ModelState.AddModelError("VenueName", "The venue name you entered does not exist.");
            }
            else
            {
                booking.VenueId = venue.VenueId;
            }

            // Booking date validation
            if (booking.BookingDate != null && booking.VenueId != null)
            {
                var conflictingBooking = await _context.Bookings
                    .FirstOrDefaultAsync(b =>
                        b.VenueId == booking.VenueId &&
                        b.EventId == booking.EventId &&
                        b.BookingDate == booking.BookingDate);

                if (conflictingBooking != null)
                {
                    ModelState.AddModelError("", "This venue is already booked for the selected date.");
                } 
                
            }

            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Booking created successfully!";
                return RedirectToAction(nameof(Index));
            }


            ViewData["EventId"] = new SelectList(_context.Eventsses, "EventId", "EventName", booking.EventId);
            return View(booking);
        }


        // GET: Booking/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return NotFound();

            ViewData["EventId"] = new SelectList(_context.Eventsses, "EventId", "EventName", booking.EventId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName", booking.VenueId);
            return View(booking);
        }

        // POST: Booking/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,VenueId,EventId,CustomerName,CustomerEmail,BookingDate")] Booking booking)
        {
            if (id != booking.BookingId) return NotFound();

            if (booking.BookingDate != null)
            {
                var conflictingBooking = await _context.Bookings
                    .Where(b => b.BookingId != booking.BookingId) // exclude current
                    .FirstOrDefaultAsync(b =>
                        b.VenueId == booking.VenueId &&
                        b.EventId == booking.EventId &&
                        b.BookingDate == booking.BookingDate
                    );

                if (conflictingBooking != null)
                {
                    ModelState.AddModelError("", "This venue is already booked for the selected date and time.");
                }
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
                    if (!BookingExists(booking.BookingId))
                        return NotFound();
                    else
                        throw;
                }
                TempData["SuccessMessage"] = "Booking edited successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewData["EventId"] = new SelectList(_context.Eventsses, "EventId", "EventName", booking.EventId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName", booking.VenueId);
            return View(booking);
        }

        // GET: Booking/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .FirstOrDefaultAsync(m => m.BookingId == id);

            if (booking == null) return NotFound();

            return View(booking);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Booking successfully deleted !";

            }
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }


    }
}
