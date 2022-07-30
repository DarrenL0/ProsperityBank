using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ProsperityBank_API.Model
{
    public partial class Account
    {
        public Account()
        {
            BillPays = new HashSet<BillPay>();
            TransactionAccountNumberNavigations = new HashSet<Transaction>();
            TransactionDestinationAccountNumberNavigations = new HashSet<Transaction>();
        }

        [Display(Name = "Account Number")]
        public int AccountNumber { get; set; }
        [Display(Name = "Account Type")]
        public int AccountType { get; set; }
        [Display(Name = "Customer ID")]
        public int CustomerId { get; set; }
        public DateTime ModifyDate { get; set; }
        public decimal Balance { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<BillPay> BillPays { get; set; }
        public virtual ICollection<Transaction> TransactionAccountNumberNavigations { get; set; }
        public virtual ICollection<Transaction> TransactionDestinationAccountNumberNavigations { get; set; }
    }
}
