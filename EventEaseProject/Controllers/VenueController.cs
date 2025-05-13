using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventEaseProject.Models;
using Azure.Storage.Blobs;


namespace EventEaseProject.Controllers
{
    public class VenueController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly EventEaseDbContext _context;

        public VenueController(EventEaseDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        // GET: Venue
        public async Task<IActionResult> Index()
        {
            return View(await _context.Venues.ToListAsync());
        }

        // GET: Venue/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.Venues
                .FirstOrDefaultAsync(m => m.VenueId == id);
            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        // GET: Venue/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Venue/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Venue venue)
        {
            if (ModelState.IsValid)
            {
                if (venue.ImageFile != null)
                {
                    // UPLOAD TO AZURE
                    string containerName = "eventeasecldv6211"; // whatever your blob container is
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(venue.ImageFile.FileName);

                    

                }

                // SAVE TO DATABASE
                _context.Add(venue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }

        // GET: Venue/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.Venues.FindAsync(id);
            if (venue == null)
            {
                return NotFound();
            }
            return View(venue);
        }

        // POST: Venue/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VenueId,VenueName,VenueLocation,Capacity,ImageFile")] Venue venue)
        {
            if (id != venue.VenueId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingVenue = await _context.Venues.FindAsync(id);
                    if (existingVenue == null)
                    {
                        return NotFound();
                    }
                    
                    // Update text fields
                    existingVenue.VenueName = venue.VenueName;
                    existingVenue.VenueLocation = venue.VenueLocation;
                    existingVenue.Capacity = venue.Capacity;

                    

                    _context.Update(existingVenue);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Venue updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenueExists(venue.VenueId))
                        return NotFound();
                    else
                        throw;
                }
            }

            return View(venue);
        }

        // GET: Venue/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.Venues
                .FirstOrDefaultAsync(m => m.VenueId == id);
            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venue = await _context.Venues.FindAsync(id);

            // Check for active bookings
            bool hasBookings = await _context.Bookings.AnyAsync(b => b.VenueId == id);

            if (hasBookings)
            {
                TempData["Error"] = "Cannot delete venue. There are active bookings for this venue.";
                return RedirectToAction(nameof(Index));
            }

            if (venue != null)
            {
                _context.Venues.Remove(venue);
                await _context.SaveChangesAsync();
            }
            TempData["SuccessMessage"] = "Venue successfully deleted !";
            return RedirectToAction(nameof(Index));
        }



        private bool VenueExists(int id)
        {
            return _context.Venues.Any(e => e.VenueId == id);
        }

        
    }
}
