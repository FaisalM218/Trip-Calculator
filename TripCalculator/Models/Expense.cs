using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TripCalculator.Models
{
    public class Expense
    {
        public int ExpenseId { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Do not enter more than 30 characters")]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        [Range(0, 100000000000, ErrorMessage = "Please enter number between 0 and 100 billion")]
        public decimal Cost { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public int BookingId { get; set; }
    }
}