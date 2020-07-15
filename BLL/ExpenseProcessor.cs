using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ExpenseProcessor
    {
        GenericRepository<Expense> repo;
        ICalculatorContext context;

        public ExpenseProcessor()
        {
            context = new CalculatorContext();
            repo = new GenericRepository<Expense>(context, context.Expenses);
        }

        public ExpenseProcessor(ICalculatorContext calcContext)
        {
            context = calcContext;
            repo = new GenericRepository<Expense>(context, context.Expenses);
        }

        public void createExpense(Expense newExpense)
        {
            repo.Insert(newExpense);
            repo.Save();
        }

        public Expense getExpenseById(int? id)
        {
            if (id == null) return null;
            return repo.GetByID(id);
        }

        public void deleteExpense(int id)
        {
            repo.Delete(id);
            repo.Save();
        }

    }
}
