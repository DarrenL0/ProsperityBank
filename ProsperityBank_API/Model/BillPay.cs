using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ProsperityBank_API.Model
{
    public partial class BillPay
    {
        public BillPay()
        {
            Payees = new HashSet<Payee>();
        }

        public int BillPayId { get; set; }
        [Display(Name = "Account Number")]
        public int AccountNumber { get; set; }
        public int PayeeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime ScheduleDate { get; set; }
        public int PeriodType { get; set; }
        public DateTime ModifyDate { get; set; }
        public bool? Blocked { get; set; }

        public virtual Account AccountNumberNavigation { get; set; }
        public virtual Payee Payee { get; set; }
        public virtual ICollection<Payee> Payees { get; set; }
    }
}
