using ProsperityBank_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProsperityBank_API.Models
{
    public class BillPayViewModel
    {
        public List<BillPay> BillPays { get; set; }

        public List<Customer> Customers { get; set; }

        public int? CustomerID { get; set; }

    }
}
