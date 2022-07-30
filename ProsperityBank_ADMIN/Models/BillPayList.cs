using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProsperityBank_ADMIN.Models
{
    public class BillPayList
    {
        public List<BillPayDTO> BillPays { get; set; }

        public List<CustomerDTO> Customers { get; set; }

        public int? CustomerID { get; set; }

    }
}
