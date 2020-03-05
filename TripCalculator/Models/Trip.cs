using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TripCalculator.Models
{
    public class Trip
    {
        public int TripId { get; set; }
        public string Description { get; set; }
        public ICollection<Booking> Bookings { get; set; }

        public Trip()
        {
            this.Bookings = new HashSet<Booking>(); 
        }
    }
}