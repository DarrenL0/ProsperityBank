using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProsperityBank.Models
{

    public enum PeriodType
    {
        Monthly = 1,
        Quarterly = 2,
        OnceOff = 3
    }
    public class BillPay
    {
        public int BillPayID { get; set; }

        [ForeignKey("Account")]
        [Display(Name = "Account Number")]
        public int AccountNumber { get; set; }
        public virtual Account Account { get; set; }

        [Required]
        [ForeignKey("Payee")]
        [Display(Name = "Payee ID")]
        public int PayeeID { get; set; }
        public virtual Payee Payee { get; set; }

        [Required, Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Required, Column(TypeName = "datetime2")]
        [Display(Name = "Schedule Date")]
        public DateTime ScheduleDate { get; set; }

        [Required, Display(Name = "Period"), StringLength(1)]
        public PeriodType PeriodType { get; set; }

        [Required, Column(TypeName = "datetime2")]
        [DataType(DataType.DateTime)]
        public DateTime ModifyDate { get; set; }
        public bool Blocked { get; set; }

        public virtual List<Payee> Payees { get; set; }
    }
}
