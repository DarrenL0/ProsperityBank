
using ProsperityBank_API.Models;
using System.Collections.Generic;
using System.Linq;


namespace ProsperityBank_API.Model.DataManager
{
    public class BillPayManager : IDataRepository<BillPay, int>
    {

        private readonly ProsperityBank_API_Context _context;

        public BillPayManager(ProsperityBank_API_Context context)
        {
            _context = context;
        }


        public int Add(BillPay billPay)
        {
            _context.BillPays.Add(billPay);
            _context.SaveChanges();

            return billPay.BillPayId;
        }

        public int Delete(int id)
        {
            _context.BillPays.Remove(_context.BillPays.Find(id));
            _context.SaveChanges();

            return id;
        }

        public BillPay Get(int id)
        {
            return _context.BillPays.Where(x => x.BillPayId == id).FirstOrDefault();
        }


        public BillPayViewModel GetAll(int? customerID)
        {
            // _context.Transactions.Where(x => x.AccountNumber == id).OrderBy(x => x.TransactionTimeUtc).Reverse()
            var billpays = from b in _context.BillPays select b;

            if (customerID != null)
            {
                billpays = billpays.Where(s => s.AccountNumberNavigation.CustomerId == customerID);
            }

            var billPaysList = billpays.ToList();
            var customerList = _context.Customers.ToList();

            var billPayReturnList = new BillPayViewModel
            {
                BillPays = billPaysList,
                Customers = customerList,
                CustomerID = customerID,
            };

            return billPayReturnList;
        }

        public IEnumerable<BillPay> GetAll()
        {
            return _context.BillPays.ToList();
        }

        public int Update(int id, BillPay billPay)
        {
            _context.Update(billPay);
            _context.SaveChanges();

            return id;
        }


        // blocking a billPay using its billPayID
        public bool BlockBillPay(int id)
        {
            var billPay = _context.BillPays.Where(x => x.BillPayId == id).FirstOrDefault();

            if (billPay != null)
            {
                billPay.BlockBillPay();
                _context.SaveChanges();
                return true;
            }

            return false;

        }

        // UNblocking a billPay using its billPayID
        public bool UnBlockBillPay(int id)
        {
            var billPay = _context.BillPays.Where(x => x.BillPayId == id).FirstOrDefault();

            if (billPay != null)
            {
                billPay.UnBlockBillPay();
                _context.SaveChanges();
                return true;
            }

            return false;

        }
    }
}
