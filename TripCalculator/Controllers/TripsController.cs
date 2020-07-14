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
using TripCalculator.ViewModel;

namespace TripCalculator.Controllers
{
    [Authorize]
    public class TripsController : Controller
    {
        private CalculatorContext db = new CalculatorContext();

        // GET: Trips
        public ActionResult Index()
        {
            return View(db.Trips.ToList());
        }

        // GET: Trips/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Where(t => t.TripId == id).Include(t  => t.Bookings.Select(b => b.Expenses))
                                .SingleOrDefault();
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // GET: Trips/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trips/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TripId,Description")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                db.Trips.Add(trip);
                db.SaveChanges();
                return View("Details", trip);
            }

            return View(trip);
        }

        // GET: Trips/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trip trip = db.Trips.Find(id);
            db.Trips.Remove(trip);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Trips/BreakDown/5
        public ActionResult BreakDown(int id)
        {
            Trip trip = db.Trips.Where(t => t.TripId == id).Include(t => t.Bookings.Select(b => b.Expenses))
                                .SingleOrDefault();

            //This view model calculates the sub totals for each user, and how much each user owes.
            TripBreakDownViewModel breakDownModel = new TripBreakDownViewModel(trip.Bookings.ToList());

            return View(breakDownModel);
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
