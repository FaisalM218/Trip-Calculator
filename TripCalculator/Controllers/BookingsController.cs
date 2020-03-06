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
            //This query gets all users who are not already booked for this trip
            var usersQuery = from u in db.Users
                    join b in db.Bookings.Where(b => b.TripId == tripId) on u.UserId equals b.UserId into bu
                    from b in bu.DefaultIfEmpty()
                    where b == null
                    select u;
            
            //If a trip id is provided, we make the specified trip the default selected trip in the drop down.
            if (tripId > 0) ViewBag.TripId = new SelectList(db.Trips, "TripId", "Description", new { TripId = tripId });
            else ViewBag.TripId = new SelectList(db.Trips, "TripId", "Description");
            ViewBag.UserId = new SelectList(usersQuery.ToList(), "UserId", "UserName");
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
                //Redirect back to the details page of the trip
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

            //We use the tempdata and viewbag to be able to redirect back to the details page of the trip.
            TempData["tripId"] = tripId;
            ViewBag.tripId = tripId;
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