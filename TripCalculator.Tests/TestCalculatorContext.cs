using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCalculator.Tests
{
    //Mock DbContext for use in testing
    class TestCalculatorContext : ICalculatorContext
    {
        public TestCalculatorContext()
        {
            Users = new MockDbSet<User>();
            Expenses = new MockDbSet<Expense>();
            Trips = new MockDbSet<Trip>();
            Bookings = new MockDbSet<Booking>();
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public void Dispose(){ }

        public DbEntityEntry Entry(object entity)
        {
            return null;
        }

        public int SaveChanges()
        {
            return 0;
        }
    }
}
