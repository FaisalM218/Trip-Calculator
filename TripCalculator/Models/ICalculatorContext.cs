using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCalculator.Models
{
    public interface ICalculatorContext : IDisposable
    {
        DbSet<User> Users { get; }
        DbSet<Expense> Expenses { get; }
        DbSet<Trip> Trips { get; }
        DbSet<Booking> Bookings { get; }

        int SaveChanges();
    }
}
