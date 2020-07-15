using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserProcessor
    {
        GenericRepository<User> repo;
        ICalculatorContext context;
        public UserProcessor()
        {
            context = new CalculatorContext();
            repo = new GenericRepository<User>(context, context.Users);
        }

        public UserProcessor(ICalculatorContext calcContext)
        {
            context = calcContext;
            repo = new GenericRepository<User>(context, context.Users);
        }

        public IEnumerable<User> getAllUsers()
        {
            return repo.Get();
        }

        public void addUser(User newUser)
        {
            repo.Insert(newUser);
            repo.Save();
        }

        public List<User> getUsersNotBookedForTrip(int tripId)
        {
            BookingProcessor bp = new BookingProcessor(context);
            List<Booking> bookings = bp.getBookingsForTrip(tripId);
            List<int> usersWhoBooked = bookings.Select(b => b.UserId).ToList();
            return repo.Get(u => !usersWhoBooked.Contains(u.UserId)).ToList();
        }
    }
}
