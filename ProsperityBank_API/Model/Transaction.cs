using System;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ProsperityBank_API.Model
{
    public partial class Transaction
    {
        [Display(Name = "Transaction ID")]
        public int TransactionId { get; set; }
        [Display(Name = "Transaction Type")]
        public int TransactionType { get; set; }
        [Display(Name = "Account Number")]
        public int AccountNumber { get; set; }

        [Display(Name = "Destination Account Number")]
        public int? DestinationAccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }

        [Display(Name = "Transaction Time")]
        public DateTime TransactionTimeUtc { get; set; }

        public virtual Account AccountNumberNavigation { get; set; }
        public virtual Account DestinationAccountNumberNavigation { get; set; }
    }
}
