using System.Collections.Generic;


namespace ProsperityBank_API.Model
{
    public class TransactionViewModel
    {
        public List<Transaction> Transactions { get; set; }

        public List<Customer> Customers { get; set; }

        public int? CustomerID { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
    }
}
