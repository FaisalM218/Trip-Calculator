using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TripCalculator.ViewModel
{
    public class Debt
    {
        public string debtor;
        public string creditor;
        [DataType(DataType.Currency)]
        public decimal amount;
    }
}