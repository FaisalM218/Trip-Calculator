using AutoMapper;
using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TripCalculator.Models;

namespace TripCalculator.Controllers
{
    public class ExpensesController : Controller
    {
        private BookingProcessor bookingProcessor = new BookingProcessor();
        private ExpenseProcessor expenseProcessor = new ExpenseProcessor();
        private IMapper mapper = GlobalVariables.mapper.CreateMapper();

        // GET: Expenses/Create?bookingId
        public ActionResult Create(int bookingId)
        {
            Booking booking = mapper.Map<Booking>(bookingProcessor.getBookingById(bookingId));
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
                expenseProcessor.createExpense(mapper.Map<DAL.Models.Expense>(expense));
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
            Expense expense = mapper.Map<Expense>(expenseProcessor.getExpenseById(id));
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
            expenseProcessor.deleteExpense(id);
            return RedirectToAction("Details", "Trips", new { id = TempData["tripId"] });
        }
    }
}
