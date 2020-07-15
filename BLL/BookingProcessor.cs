using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BookingProcessor
    {
        GenericRepository<Booking> repo;
        ICalculatorContext context;

        public BookingProcessor()
        {
            context = new CalculatorContext();
            repo = new GenericRepository<Booking>(context, context.Bookings);

        }

        public BookingProcessor(ICalculatorContext calcContext)
        {
            context = calcContext;
            repo = new GenericRepository<Booking>(context, context.Bookings);

        }

        public Booking getBookingById(int? bookingId)
        {
            if (bookingId == null) return null;
            return repo.Get(b => b.BookingId == bookingId, null, "User,Trip").FirstOrDefault();
        }

        public List<Booking> getBookingsForTrip(int tripId)
        {
            return repo.Get(b => b.TripId == tripId).ToList();
        }

        public void createBooking(Booking newBooking)
        {
            repo.Insert(newBooking);
            repo.Save();
        }

        public void deleteBooking(int? id)
        {
            if (id == null) return;
            repo.Delete(id);
            repo.Save();
        }
    }
}
