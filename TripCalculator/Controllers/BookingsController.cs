using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TripCalculator.Data_Access_Layer;
using TripCalculator.Models;

namespace TripCalculator.Controllers
{
    public class BookingsController : Controller
    {
        private CalculatorContext db = new CalculatorContext();

        // GET: Bookings/Create
        public ActionResult Create(int tripId = -1)
        {
            if (tripId > 0) ViewBag.TripId = new SelectList(db.Trips, "TripId", "Description", new { TripId = tripId });
            else ViewBag.TripId = new SelectList(db.Trips, "TripId", "Description");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
            return View();
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingId,TripId,UserId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Details", "Trips", new { id = booking.TripId });
            }

            return View(booking);
        }

        // GET: Bookings/Delete/5?tripId=2
        public ActionResult Delete(int? id, int tripId = -1)
        {
            if (id == null || tripId == -1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Where(b => b.BookingId == id).Include(b => b.User).Include(b => b.Trip).SingleOrDefault();
            if (booking == null)
            {
                return HttpNotFound();
            }
            TempData["tripId"] = tripId;
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("Details", "Trips", new { id = TempData["tripId"] });
        }
    }
}