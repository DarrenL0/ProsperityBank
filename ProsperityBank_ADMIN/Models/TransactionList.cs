using System.Collections.Generic;


namespace ProsperityBank_ADMIN.Models
{
    public class TransactionList
    {
        public List<TransactionDTO> Transactions { get; set; }

        public List<CustomerDTO> Customers { get; set; }

        public int? CustomerID { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
    }
}
