using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProsperityBank.Models
{

    public enum TransactionType
    {
        Deposit = 1,
        Withdraw = 2,
        Transfer = 3,
        ServiceCharge = 4,
        BillPay = 5
    }
    public class Transaction
    {
        [Display(Name = "Transaction ID")]
        public int TransactionID { get; init; }

        [Required]
        [Display(Name = "Transaction Type")]
        public TransactionType TransactionType { get; init; }

        [ForeignKey("Account")]
        [Display(Name = "Account Number")]
        public int AccountNumber { get; init; }
        public virtual Account Account { get; init; }

        [ForeignKey("DestinationAccount")]
        [Display(Name = "Destination Account Number")]
        public int? DestinationAccountNumber { get; init; }
        public virtual Account DestinationAccount { get; init; }

        [Column(TypeName = "money")]
        public decimal Amount { get; init; }

        [StringLength(255)]
        public string Comment { get; init; }

        [Display(Name = "Transaction Time")]
        public DateTime TransactionTimeUtc { get; init; }
    }
}
