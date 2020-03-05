using System;
using System.Collections.Generic;
using System.Linq;
using TripCalculator.Models;

namespace TripCalculator.ViewModel
{
    public class TripBreakDownViewModel
    {
        public decimal TotalCost { get; set; }
        public List<UserSubTotal> debtors { get; set; }
        public List<UserSubTotal> creditors { get; set; }
        public List<Debt> debts { get; set; }

        public TripBreakDownViewModel(List<Booking> bookings)
        {
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

            foreach(Booking b in bookings)
            {
                UserSubTotal newSubTotal = new UserSubTotal { name = b.User.UserName, subtotal = 0, balance = 0 };
                foreach(Expense e in b.Expenses)
                {
                    total += e.Cost;
                    newSubTotal.subtotal += e.Cost;
                }
                subTotals.Add(newSubTotal);
            }

            decimal costForEach = total / subTotals.Count;

            foreach(UserSubTotal st in subTotals)
            {
                decimal difference = st.subtotal - costForEach;
                if (difference < 0)
                {
                    st.balance = Math.Abs(difference);
                    debtors.Add(st);
                }
                else
                {
                    st.balance = difference;
                    creditors.Add(st);
                }
            }

            this.TotalCost = total;
        }

        private void calculateDebts()
        {
            int debtorsIndex = 0 ;
            int creditorsIndex = 0;
            while(debtorsIndex < debtors.Count && creditorsIndex < creditors.Count && debtors[debtorsIndex].balance > 0)
            {
                UserSubTotal currDebtor = debtors[debtorsIndex];
                UserSubTotal currCreditor = creditors[creditorsIndex];
                decimal currentPaid = 0;
                if(currDebtor.balance > currCreditor.balance)
                {
                    currentPaid = currCreditor.balance;
                    currDebtor.balance -= currCreditor.balance;
                    currCreditor.balance = 0;
                    creditorsIndex += 1;
                }
                else if(currDebtor.balance < currCreditor.balance)
                {
                    currentPaid = currDebtor.balance;
                    currCreditor.balance -= currDebtor.balance;
                    currDebtor.balance = 0;
                    debtorsIndex += 1;
                }
                else
                {
                    currentPaid = currDebtor.balance;
                    currCreditor.balance = 0;
                    currDebtor.balance = 0;
                    debtorsIndex += 1;
                    creditorsIndex += 1;
                }
                Debt newDebt = new Debt() { debtor = currDebtor.name, creditor = currCreditor.name, amount = currentPaid };
                debts.Add(newDebt);
            }
            
        }
    }
}