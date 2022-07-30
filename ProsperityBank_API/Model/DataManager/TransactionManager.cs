using System;
using System.Collections.Generic;
using System.Linq;

namespace ProsperityBank_API.Model.DataManager
{
    public class TransactionManager : IDataRepository<Transaction, int>
    {

        private readonly ProsperityBank_API_Context _context;

        public TransactionManager(ProsperityBank_API_Context context)
        {
            _context = context;
        }


        public int Add(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return transaction.TransactionId;
        }

        public int Delete(int id)
        {
            _context.Transactions.Remove(_context.Transactions.Find(id));
            _context.SaveChanges();

            return id;
        }

        public Transaction Get(int id)
        {
            return _context.Transactions.Find(id);
        }

        
        public TransactionViewModel GetAll(int? customerID, string fromDateTime, string toDateTime)
        {
            // _context.Transactions.Where(x => x.AccountNumber == id).OrderBy(x => x.TransactionTimeUtc).Reverse()
            var transactions = from t in _context.Transactions select t;

            if(customerID != null)
            {
                transactions = transactions.Where(s => (s.AccountNumberNavigation.CustomerId == customerID));
            }

            if (!string.IsNullOrEmpty(fromDateTime))
            {
                DateTime fromDate = DateTime.ParseExact(fromDateTime,
                    "dd/MM/yyyy hh:mm:ss tt",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None);

                transactions = transactions.Where(s => s.TransactionTimeUtc.CompareTo(fromDate) >= 0);
            }

            if (!string.IsNullOrEmpty(toDateTime))
            {
                DateTime toDate = DateTime.ParseExact(toDateTime,
                    "dd/MM/yyyy hh:mm:ss tt",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None);

                transactions = transactions.Where(s => s.TransactionTimeUtc.CompareTo(toDate) <= 0);
            }

            var transactionList = transactions.ToList();
            var customerList = _context.Customers.ToList();

            var transactionHistory = new TransactionViewModel
            {
                Transactions = transactionList,
                Customers = customerList,
                CustomerID = customerID,
                StartDate = fromDateTime,
                EndDate = toDateTime
            };

            return transactionHistory;
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _context.Transactions.ToList();
        }

        public int Update(int id, Transaction transaction)
        {
            _context.Update(transaction);
            _context.SaveChanges();

            return id;
        }
    }
}
