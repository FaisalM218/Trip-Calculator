using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripCalculator.Models;
using TripCalculator.ViewModel;

namespace TripCalculator.Tests.Models
{
    /// <summary>
    /// Summary description for TestBreakDownViewModel
    /// </summary>
    [TestClass]
    public class TestBreakDownViewModel
    {
        [TestMethod]
        public void TestTotalCosts()
        {
            //Ensure that we are correctly calculating the total cost of the trip
            List<Booking> bookings = createBookings();
            TripBreakDownViewModel viewModel = new TripBreakDownViewModel(bookings);
            Assert.AreEqual(viewModel.TotalCost, (decimal)220.00);
        }

        //Create a list of bookings for testing.
        public List<Booking> createBookings()
        {
            User u1 = new User() { FirstName = "Bob", LastName = "John", UserId = 1, UserName = "BobJohn" };
            User u2 = new User() { FirstName = "Harry", LastName = "Smith", UserId = 2, UserName = "HarrySmith" };
            User u3 = new User() { FirstName = "Joe", LastName = "William", UserId = 3, UserName = "JoeWilliam" };

            Trip t = new Trip() { TripId = 1, Description = "Trip to Toronto" };

            Expense e1 = new Expense() { BookingId = 1, Description = "Candy", Cost = 30, ExpenseId = 1 };
            Expense e2 = new Expense() { BookingId = 2, Description = "Car", Cost = 90, ExpenseId = 2 };
            Expense e3 = new Expense() { BookingId = 3, Description = "Rent", Cost = 100, ExpenseId = 3 };

            Booking b1 = new Booking() { BookingId = 1, TripId = 1, UserId = 1, User = u1, Expenses = new List<Expense> { e1 } };
            Booking b2 = new Booking() { BookingId = 2, TripId = 1, UserId = 2, User = u2, Expenses = new List<Expense> { e2 } };
            Booking b3 = new Booking() { BookingId = 3, TripId = 1, UserId = 3, User = u3, Expenses = new List<Expense> { e3 } };

            return new List<Booking> { b1, b2, b3 };
        }
    }
}
