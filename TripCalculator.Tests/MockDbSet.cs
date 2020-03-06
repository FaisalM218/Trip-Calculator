using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCalculator.Tests
{
    //Mock of DbSet for use in testing
    public class MockDbSet<T> : DbSet<T>, IQueryable, IEnumerable<T> where T : class
    {
        ObservableCollection<T> _data;
        IQueryable _query;

        public MockDbSet()
        {
            _data = new ObservableCollection<T>();
            _query = _data.AsQueryable();
        }

        public override T Add(T newItem)
        {
            _data.Add(newItem);
            return newItem;
        }

        public override T Remove(T itemToRemove)
        {
            _data.Remove(itemToRemove);
            return itemToRemove;
        }
    }
}
