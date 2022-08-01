using System.Collections.Generic;


namespace ProsperityBank_ADMIN.Models
{
    public class BillPayList
    {
        public List<BillPayDTO> BillPays { get; set; }

        public List<CustomerDTO> Customers { get; set; }

        public int? CustomerID { get; set; }

    }
}
