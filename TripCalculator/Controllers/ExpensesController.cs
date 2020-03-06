using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TripCalculator.Data_Access_Layer;
using TripCalculator.Models;

namespace TripCalculator.Controllers
{
    public class ExpensesController : Controller
    {
        private CalculatorContext db = new CalculatorContext();

        // GET: Expenses/Create?bookingId
        public ActionResult Create(int bookingId)
        {
            Booking booking = db.Bookings.Where(b => b.BookingId == bookingId).Include(b => b.User).Include(b => b.Trip)
                                         .SingleOrDefault();
            ViewBag.userName = booking.User.FirstName;
            ViewBag.tripDescription = booking.Trip.Description;
            //the trip id is used to navigate back ot the trip details page once this expense is added
            TempData["tripId"] = booking.TripId;
            Expense newExpense = new Expense { BookingId = bookingId };

            return View(newExpense);
        }

        // POST: Expenses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExpenseId,Description,Cost,BookingId")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.Expenses.Add(expense);
                db.SaveChanges();
                int tripId = TempData["tripId"] != null ? (int)TempData["tripId"] : 0;
                return RedirectToAction("Details", "Trips", new { id = tripId });
            }

            return View(expense);
        }

        // GET: Expenses/Delete/5?tripId=5
        public ActionResult Delete(int? id, int tripId =-1)
        {
            if (id == null || tripId == -1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }

            //We use the tempdata and viewbag to be able to redirect back to the details page of the trip.
            TempData["tripId"] = tripId;
            ViewBag.tripId = tripId;
            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Expense expense = db.Expenses.Find(id);
            db.Expenses.Remove(expense);
            db.SaveChanges();
            return RedirectToAction("Details", "Trips", new { id = TempData["tripId"] });
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
