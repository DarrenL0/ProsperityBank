using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProsperityBank_ADMIN.Models
{

    public enum TransactionType
    {
        Deposit = 1,
        Withdraw = 2,
        Transfer = 3,
        ServiceCharge = 4,
        BillPay = 5
    }
    public class TransactionDTO
    {
        public int TransactionID { get; init; }

        [Required]
        public TransactionType TransactionType { get; init; }

        [ForeignKey("Account")]
        public int AccountNumber { get; init; }

        [ForeignKey("DestinationAccount")]
        public int? DestinationAccountNumber { get; init; }

        [Column(TypeName = "money")]
        public decimal Amount { get; init; }

        [StringLength(255)]
        public string Comment { get; init; }

        public DateTime TransactionTimeUtc { get; init; }
    }
}
