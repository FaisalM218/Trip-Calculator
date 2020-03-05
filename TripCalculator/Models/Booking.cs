using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TripCalculator.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        [Index("IX_TripIdAndUserId", 1, IsUnique = true)]
        [Required]
        public int TripId { get; set; }

        [Index("IX_TripIdAndUserId", 2, IsUnique = true)]
        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual Trip Trip { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; }

        [NotMapped]
        public string BookingSummary
        {
            get
            {
                if (User != null && Trip != null) return "User: " + User.UserName + ". Trip: " + Trip.Description;
                else return "";
            }
        }

        public Booking()
        {
            Expenses = new List<Expense>();
        }
    }
}