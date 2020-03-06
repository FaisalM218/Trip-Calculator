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
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        public decimal Cost { get; set; }

        [Required]
        public int BookingId { get; set; }
    }
}