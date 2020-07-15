using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CalculatorContext : DbContext, ICalculatorContext
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
