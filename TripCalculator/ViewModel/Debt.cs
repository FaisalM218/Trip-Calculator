using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TripCalculator.ViewModel
{
    //This class represents a debt owed by a user.
    public class Debt
    {
        public string debtor; //Name of person who owes money
        public string creditor; //Name of person who needs to be paid that money
        [DataType(DataType.Currency)]
        public decimal amount; //Amount of money owed
    }
}