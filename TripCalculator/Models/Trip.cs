using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TripCalculator.Models
{
    public class Trip
    {
        public int TripId { get; set; }
        
        [Required]
        [StringLength(30, ErrorMessage = "Do not enter more than 30 characters")]
        [Display(Name = "Trip Title")]
        public string Description { get; set; }

        public ICollection<Booking> Bookings { get; set; }

        public Trip()
        {
            this.Bookings = new HashSet<Booking>(); 
        }
    }
}