using AutoMapper;
using BLL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TripCalculator.Models;

namespace TripCalculator.Controllers
{
    public class BookingsController : Controller
    {
        private UserProcessor userProcessor = new UserProcessor();
        private TripProcessor tripProcessor = new TripProcessor();
        private BookingProcessor bookingProcessor = new BookingProcessor();
        private IMapper mapper = GlobalVariables.mapper.CreateMapper();

        // GET: Bookings/Create
        public ActionResult Create(int tripId = -1)
        {
            //This query gets all users who are not already booked for this trip

            List<User> users = userProcessor.getUsersNotBookedForTrip(tripId).Select(u => mapper.Map<User>(u)).ToList();

            //If a trip id is provided, we make the specified trip the default selected trip in the drop down.
            List<Trip> trips = tripProcessor.getAllTrips().Select(t => mapper.Map<Trip>(t)).ToList();
            if (tripId > 0) ViewBag.TripId = new SelectList(trips, "TripId", "Description", new { TripId = tripId });
            else ViewBag.TripId = new SelectList(trips, "TripId", "Description");
            ViewBag.UserId = new SelectList(users, "UserId", "UserName");
            return View();
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingId,TripId,UserId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                bookingProcessor.createBooking(mapper.Map<DAL.Models.Booking>(booking));
            }

            //Redirect back to the details page of the trip
            return RedirectToAction("Details", "Trips", new { id = booking.TripId });
        }

        // GET: Bookings/Delete/5?tripId=2
        public ActionResult Delete(int? id, int tripId = -1)
        {
            if (id == null || tripId == -1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = mapper.Map<Booking>(bookingProcessor.getBookingById(id));
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
            bookingProcessor.deleteBooking(id);
            return RedirectToAction("Details", "Trips", new { id = TempData["tripId"] });
        }
    }
}