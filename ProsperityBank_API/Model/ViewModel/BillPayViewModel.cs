using ProsperityBank_API.Model;
using System.Collections.Generic;

namespace ProsperityBank_API.Models
{
    public class BillPayViewModel
    {
        public List<BillPay> BillPays { get; set; }

        public List<Customer> Customers { get; set; }

        public int? CustomerID { get; set; }

    }
}
