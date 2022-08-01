﻿using System.Collections.Generic;

#nullable disable

namespace ProsperityBank_API.Model
{
    public partial class Payee
    {
        public Payee()
        {
            BillPays = new HashSet<BillPay>();
        }

        public int PayeeId { get; set; }
        public string PayeeName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int State { get; set; }
        public string PostCode { get; set; }
        public string Phone { get; set; }
        public int? BillPayId { get; set; }

        public virtual BillPay BillPay { get; set; }
        public virtual ICollection<BillPay> BillPays { get; set; }
    }
}
