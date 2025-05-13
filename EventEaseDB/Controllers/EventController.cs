using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EventEaseDB.Models;

namespace EventEaseDB.Controllers
{
    public class EventController : Controller
    {
        private DBEventEase db = new DBEventEase();

        // GET: Event
        public ActionResult Index()
        {
            var eventsses = db.Eventsses.Include(e => e.Venue);
            return View(eventsses.ToList());
        }

        // GET: Event/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eventss eventss = db.Eventsses.Find(id);
            if (eventss == null)
            {
                return HttpNotFound();
            }
            return View(eventss);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            ViewBag.VenueId = new SelectList(db.Venues, "VenueId", "venueName");
            return View();
        }

        // POST: Event/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventId,eventName,eventDate,eventTime,eventDescription,ImageURL,VenueId")] Eventss eventss)
        {
            if (ModelState.IsValid)
            {
                db.Eventsses.Add(eventss);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VenueId = new SelectList(db.Venues, "VenueId", "venueName", eventss.VenueId);
            return View(eventss);
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eventss eventss = db.Eventsses.Find(id);
            if (eventss == null)
            {
                return HttpNotFound();
            }
            ViewBag.VenueId = new SelectList(db.Venues, "VenueId", "venueName", eventss.VenueId);
            return View(eventss);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventId,eventName,eventDate,eventTime,eventDescription,ImageURL,VenueId")] Eventss eventss)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventss).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VenueId = new SelectList(db.Venues, "VenueId", "venueName", eventss.VenueId);
            return View(eventss);
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eventss eventss = db.Eventsses.Find(id);
            if (eventss == null)
            {
                return HttpNotFound();
            }
            return View(eventss);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Eventss eventss = db.Eventsses.Find(id);
            db.Eventsses.Remove(eventss);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
