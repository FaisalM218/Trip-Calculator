using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TripProcessor
    {
        GenericRepository<Trip> repo;
        ICalculatorContext context;
        public TripProcessor()
        {
            context = new CalculatorContext();
            repo = new GenericRepository<Trip>(context, context.Trips);
        }

        public TripProcessor(ICalculatorContext calcContext)
        {
            context = calcContext;
            repo = new GenericRepository<Trip>(context, context.Trips);
        }

        public IEnumerable<Trip> getAllTrips()
        {
            return repo.Get();
        }

        public Trip getTripDetails(int? id)
        {
            if (id == null) return null;
            var res = repo.Get(t => t.TripId == id, null, "Bookings.Expenses").FirstOrDefault();
            return res;
        }

        public void createTrip(Trip newTrip)
        {
            repo.Insert(newTrip);
            repo.Save();
        }

        public void deleteTrip(int? id)
        {
            if (id == null) return;
            repo.Delete(id);
            repo.Save();
        }
    }
}
