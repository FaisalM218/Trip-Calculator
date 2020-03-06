using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TripCalculator.ViewModel
{
    public class UserSubTotal
    {
        public string name;
        [DataType(DataType.Currency)]
        public decimal subtotal;
        [DataType(DataType.Currency)]
        public decimal balance;
    }
}