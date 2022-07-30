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

        public static void Withdraw(this Account account, decimal amount, string comment)
        {
            // checking if fee applicable
            bool chargeFee = DetermineFees(account.Transactions);

            // checks if amount + fee will cause violation of business rules
            if (ValidateDebit(account, amount, TransactionType.Withdraw))
            {
                account.Balance -= amount;

                //create transaction for every withdraw made 
                account.Transactions.Add(
                    new Transaction
                    {
                        TransactionType = TransactionType.Withdraw,
                        Amount = amount,
                        TransactionTimeUtc = DateTime.UtcNow,
                        Comment = comment
                    });

                // charging fee if applicable
                if (chargeFee)
                {
                    account.Balance -= ATMWithdrawFee;

                    account.Transactions.Add(
                    new Transaction
                    {
                        TransactionType = TransactionType.ServiceCharge,
                        Amount = ATMWithdrawFee,
                        TransactionTimeUtc = DateTime.UtcNow,
                        Comment = "ATM withdraw fee"
                    });
                }
            }
        }

        public static bool Transfer(this Account account, Account destAccount, decimal amount, string comment)
        {
            // checking not transfering to the same account
            if (account.AccountNumber == destAccount.AccountNumber)
            {
                return false;
            }


            // checking if fee applicable
            bool chargeFee = DetermineFees(account.Transactions);

            if (ValidateDebit(account, amount, TransactionType.Transfer))
            {
                account.Balance -= amount;

                destAccount.Balance += amount;

                // adding transaction for debit account (this account)
                account.Transactions.Add(
                    new Transaction
                    {
                        TransactionType = TransactionType.Transfer,
                        Amount = amount,
                        TransactionTimeUtc = DateTime.UtcNow,
                        DestinationAccountNumber = destAccount.AccountNumber,
                        Comment = comment

                    });


                // adding transaction for credit account (dest account)
                destAccount.Transactions.Add(
                    new Transaction
                    {
                        TransactionType = TransactionType.Transfer,
                        Amount = amount,
                        TransactionTimeUtc = DateTime.UtcNow,
                        Comment = comment
                    });


                // charging fee if applicable
                if (chargeFee)
                {
                    account.Balance -= AccountTransferFee;

                    account.Transactions.Add(
                    new Transaction
                    {
                        TransactionType = TransactionType.ServiceCharge,
                        Amount = AccountTransferFee,
                        TransactionTimeUtc = DateTime.UtcNow,
                        Comment = "Account transfer fee"
                    });
                }
            }

            return true;
        }

        public static bool ValidateDebit(this Account account, decimal amount, TransactionType type)
        {
            // adding fee to total transaction amount to ensure fees can be paid on 
            // any transaction without breaking business rules (negative balance)
            if (DetermineFees(account.Transactions))
            {
                if (type == TransactionType.Withdraw)
                {
                    amount += ATMWithdrawFee;
                }
                if (type == TransactionType.Transfer)
                {
                    amount += AccountTransferFee;
                }
            }


            if (account.AccountType == AccountType.Checking)
            {
                if (account.Balance - amount >= _checkingMinimum)
                {
                    return true;
                }
            }
            else if (account.AccountType == AccountType.Saving)
            {
                if (account.Balance - amount >= _savingsMinimum)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool DetermineFees(List<Transaction> transactions)
        {
            int numFeeTransactions = 0;
            bool chargeFee = false;

            foreach (var t in transactions)
            {
                if (t.TransactionType == TransactionType.Withdraw)
                {
                    numFeeTransactions++;
                }
                // only outgoing transaction incurr fee, hence must have destination account number
                else if (t.TransactionType == TransactionType.Transfer && t.DestinationAccountNumber != null)
                {
                    numFeeTransactions++;
                }

                if (numFeeTransactions > NUMBER_FREE_TRANSACTIONS)
                {
                    chargeFee = true;
                    break;
                }
            }

            return chargeFee;
        }

        public static void ScheduleBillPay(this Account account, BillPay billpay)
        {
            //decrease the account balance by the bill pay amount 
            account.Balance -= billpay.Amount;

            //add transaction
            account.Transactions.Add(
                    new Transaction
                    {
                        TransactionType = TransactionType.BillPay,
                        Amount = billpay.Amount,
                        TransactionTimeUtc = DateTime.UtcNow,
                        Comment = "Schedule billpay"
                    });

        }

    }
 }
