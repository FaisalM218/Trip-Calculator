using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TripCalculator.ViewModel
{
    //This class represents the total cost incurred by a user
    public class UserSubTotal
    {
        public string name; //Name of the user
        [DataType(DataType.Currency)]
        public decimal subtotal; //Total amount that the user spent on the trip
        [DataType(DataType.Currency)]
        //How much more or less this user spent in relation to the per user cost of the trip.
        //Example: if the equally divided cost of the trip is $25. And this user spent $10. Their balance would be -$15.
        public decimal balance; 
    }
}