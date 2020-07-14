using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TripCalculator.Models;

namespace TripCalculator.Data_Access_Layer
{
    public class CalculatorContext : DbContext, ICalculatorContext
    {
        public CalculatorContext() : base("CalculatorContext")
        {
            currencies = new List<Currency>();
            Currency cadCurrency = new Currency() { Name = "CAD", Conversion = 1.44m };

            Currency usdCurrency = new Currency() { Name = "USD", Conversion = 1.00m };
            currencies.Add(cadCurrency);
            currencies.Add(usdCurrency);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Trip> Trips { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public List<Currency> currencies { get; set; }
    }
}