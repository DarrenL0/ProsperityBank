using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProsperityBank.Models
{

    public enum AccountType
    {
        Checking = 1,
        Saving = 2
    }
    public class Account
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Account Number")]
        public int AccountNumber { get; set; }

        [Display(Name = "Type"), Required]
        [StringLength(1)]
        public AccountType AccountType { get; set; }

        [ForeignKey("Customer")]
        [Display(Name = "Customer ID")]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        [Column(TypeName = "datetime2")]
        [DataType(DataType.DateTime)]
        public DateTime ModifyDate { get; set; }

        [Column(TypeName = "money"), Required]
        [DataType(DataType.Currency)]
        [Range(0.01, (Double)Decimal.MaxValue, ErrorMessage = "The Amount must be large than 0")]
        public decimal Balance { get; set; }

        public virtual List<Transaction> Transactions { get; set; }
        public virtual List<BillPay> BillPays { get; set; }


    }

    public static class AccountExtension
    {
        private static readonly decimal _savingsMinimum = 0;
        private static readonly decimal _checkingMinimum = 200;

        private static readonly int NUMBER_FREE_TRANSACTIONS = 4;

        private static readonly decimal ATMWithdrawFee = 0.10m;
        private static readonly decimal AccountTransferFee = 0.20m;

        public static void Deposit(this Account account, decimal amount, string comment)
        {
            //logic for deposit
            account.Balance += amount;

            //create transaction for every deposit made
            //create a public method in model to add transactions
            account.Transactions.Add(
                new Transaction
                {
                    TransactionType = TransactionType.Deposit,
                    Amount = amount,
                    TransactionTimeUtc = DateTime.UtcNow,
                    Comment = comment
                });
        }

    }
 }
