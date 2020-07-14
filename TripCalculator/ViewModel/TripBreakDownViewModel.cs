using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TripCalculator.Data_Access_Layer;
using TripCalculator.Models;

namespace TripCalculator.ViewModel
{
    public class TripBreakDownViewModel
    {
        [DataType(DataType.Currency)]
        public decimal TotalCost { get; set; } //Total cost of the trip
        public List<UserSubTotal> SubTotals { get; set; } //Subtotals of each user in the trip
        public List<UserSubTotal> debtors { get; set; } //The users who owe money
        public List<UserSubTotal> creditors { get; set; } //The users who will receive money
        public List<Debt> debts { get; set; } //This is a list of who will pay who and how much 
        private CalculatorContext db;

        private readonly decimal conversion = 1.44m;

        public TripBreakDownViewModel(List<Booking> bookings)
        {
            db = new CalculatorContext();
            debtors = new List<UserSubTotal>();
            creditors = new List<UserSubTotal>();
            debts = new List<Debt>();
            calculateTotalCostAndSubTotals(bookings);
            calculateDebts();
        }

        private void calculateTotalCostAndSubTotals(List<Booking> bookings)
        {
            
            decimal total = 0;
            List<UserSubTotal> subTotals = new List<UserSubTotal>(); 

            //For each booking, we go through each expense to populate the total cost of the trip, and the subtotal for each
            //booking a.k.a user
            foreach(Booking b in bookings)
            {
                UserSubTotal newSubTotal = new UserSubTotal { name = b.User.UserName, subtotal = 0, balance = 0 };
                foreach(Expense e in b.Expenses)
                {
                    Currency currentCurrency = db.currencies.Where(c => c.Name == e.Currency).FirstOrDefault();
                    decimal cost = e.Cost / currentCurrency.Conversion;
                    total += cost;
                    newSubTotal.subtotal += cost;
                }
                subTotals.Add(newSubTotal);
            }

            //This is the equally divided cost of the trip for each user.
            decimal costForEach = subTotals.Count == 0 ? 0 : total / subTotals.Count;

            //For each user subtotal, we are seeing if they spent more or less than the costForEach.
            //We add them to the creditors list or debtors list.
            foreach(UserSubTotal st in subTotals)
            {
                decimal difference = st.subtotal - costForEach;
                if (difference < 0)
                {
                    //In this case, the user spent less than the costForEach. So they have an owing balance, and they are
                    //added to the debtors list.
                    st.balance = Math.Abs(difference);
                    debtors.Add(st);
                }
                else
                {
                    st.balance = difference;
                    creditors.Add(st);
                }
            }

            this.SubTotals = subTotals;
            this.TotalCost = total;
        }

        private void calculateDebts()
        {
            //We are iterating over the debtors list and the creditors list.
            //For each debtor we pay as much as we can to each creditor, until our balance is 0. Then we move to the next
            //debtor. If the creditor's balance reaches 0, we move to the next creditor.
            int debtorsIndex = 0 ;
            int creditorsIndex = 0;
            while(debtorsIndex < debtors.Count && creditorsIndex < creditors.Count)
            {
                UserSubTotal currDebtor = debtors[debtorsIndex];
                UserSubTotal currCreditor = creditors[creditorsIndex];
                decimal currentPaid = 0;
                if(currDebtor.balance > currCreditor.balance)
                {
                    //in this case the current debtor has a larger balance than the current creditor balance.
                    //the current debtor will pay the entire balance of the current creditor.
                    //we reduce the balance of the current debtor by the balance of the current creditor.
                    //we move to the next creditor that needs to be payed.
                    currentPaid = currCreditor.balance;
                    currDebtor.balance -= currCreditor.balance;
                    currCreditor.balance = 0;
                    creditorsIndex += 1;
                }
                else if(currDebtor.balance < currCreditor.balance)
                {
                    //in this case the current creditor has a larger balance than the current debtor's balance.
                    //the current debtor will pay their entire balance to the current creditor.
                    //we reduce the balance of the current creditor by the balance of the current debtor.
                    //we move to the next debtor that owes money.
                    currentPaid = currDebtor.balance;
                    currCreditor.balance -= currDebtor.balance;
                    currDebtor.balance = 0;
                    debtorsIndex += 1;
                }
                else
                {
                    //In this case the current creditor has an equal balance as the current debtor.
                    //The debtor will pay off the creditors balance.
                    //We move to the next creditor and debtor.
                    currentPaid = currDebtor.balance;
                    currCreditor.balance = 0;
                    currDebtor.balance = 0;
                    debtorsIndex += 1;
                    creditorsIndex += 1;
                }

                //We add an object to keep track of who payed who, and how much.
                Debt newDebt = new Debt() { debtor = currDebtor.name, creditor = currCreditor.name, amount = currentPaid };
                debts.Add(newDebt);
            }
            
        }
    }
}