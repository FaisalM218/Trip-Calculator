using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TripCalculator.Models;

namespace TripCalculator.Data_Access_Layer
{
    public class CalculatorContext : DbContext
    {
        public CalculatorContext() : base("CalculatorContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Trip> Trips { get; set; }

        public DbSet<Booking> Bookings { get; set; }
    }
}