using System.Collections.Generic;
using System.Linq;

namespace ProsperityBank_API.Model.DataManager
{
    public class CustomerManager : IDataRepository<Customer, int>
    {

        private readonly ProsperityBank_API_Context _context;

        public CustomerManager(ProsperityBank_API_Context context)
        {
            _context = context;
        }


        public int Add(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer.CustomerId;
        }

        public int Delete(int id)
        {
            _context.Customers.Remove(_context.Customers.Find(id));
            _context.SaveChanges();

            return id;
        }

        public Customer Get(int id)
        {
            return _context.Customers.Find(id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }


        public int Update(int id, Customer customer)
        {
            _context.Update(customer);
            _context.SaveChanges();

            return id;
        }
    }
}
