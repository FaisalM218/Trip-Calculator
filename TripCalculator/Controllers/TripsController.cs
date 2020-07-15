using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TripCalculator.Models;
using TripCalculator.ViewModel;
using BLL;
using AutoMapper;

namespace TripCalculator.Controllers
{
    public class TripsController : Controller
    {
        private TripProcessor tripProcessor = new TripProcessor();
        private IMapper mapper = GlobalVariables.mapper.CreateMapper();

        // GET: Trips
        public ActionResult Index()
        {
            List<Trip> trips = tripProcessor.getAllTrips().ToList().Select(t => mapper.Map<Trip>(t)).ToList();
            return View(trips);
        }

        // GET: Trips/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Trip trip = mapper.Map<Trip>(tripProcessor.getTripDetails(id));
            
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
                tripProcessor.createTrip(mapper.Map<DAL.Models.Trip>(trip));
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
            Trip trip = mapper.Map<Trip>(tripProcessor.getTripDetails(id));
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
            tripProcessor.deleteTrip(id);
            return RedirectToAction("Index");
        }

        // GET: Trips/BreakDown/5
        public ActionResult BreakDown(int id)
        {
            Trip trip = mapper.Map<Trip>(tripProcessor.getTripDetails(id));

            //This view model calculates the sub totals for each user, and how much each user owes.
            TripBreakDownViewModel breakDownModel = new TripBreakDownViewModel(trip.Bookings.ToList());

            return View(breakDownModel);
        }
    }
}
