using Microsoft.Extensions.DependencyInjection;
using ProsperityBank.Models;
using System;
using System.Linq;


namespace ProsperityBank.Data
{
   
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ProsperityBankDBContext>();

            // Look for customers.
            if (context.Customers.Any())
                return; // DB has already been seeded.

            context.Customers.AddRange(
                new Customer
                {
                    CustomerId = 2100,
                    CustomerName = "Matt Pole",
                    Address = "123 Fake Street",
                    City = "Melbourne",
                    Postcode = "3000",
                    PhoneNumber = "+6100000000",
                    TFN = "99999999999"
                },
                new Customer
                {
                    CustomerId = 2200,
                    CustomerName = "Rudy Med",
                    Address = "456 Real Road",
                    City = "Melbourne",
                    Postcode = "3005",
                    PhoneNumber = "+61 2222 2222",
                    TFN = "11111111111"
                },
                new Customer
                {
                    CustomerId = 2300,
                    CustomerName = "Samantha Greek",
                    PhoneNumber = "+61 3333 3333"
                });

            context.Logins.AddRange(
                new Login
                {
                    LoginID = "12345678",
                    CustomerID = 2100,
                    PasswordHash = "YBNbEL4Lk8yMEWxiKkGBeoILHTU7WZ9n8jJSy8TNx0DAzNEFVsIVNRktiQV+I8d2",
                    CreationTimeUtc = DateTime.UtcNow
                },
                new Login
                {
                    LoginID = "38074569",
                    CustomerID = 2200,
                    PasswordHash = "EehwB3qMkWImf/fQPlhcka6pBMZBLlPWyiDW6NLkAh4ZFu2KNDQKONxElNsg7V04",
                    CreationTimeUtc = DateTime.UtcNow
                },
                new Login
                {
                    LoginID = "17963428",
                    CustomerID = 2300,
                    PasswordHash = "LuiVJWbY4A3y1SilhMU5P00K54cGEvClx5Y+xWHq7VpyIUe5fe7m+WeI0iwid7GE",
                    CreationTimeUtc = DateTime.UtcNow
                });

            context.Accounts.AddRange(
                new Account
                {
                    AccountNumber = 4100,
                    AccountType = AccountType.Saving,
                    CustomerID = 2100,
                    Balance = 500,
                    ModifyDate = DateTime.UtcNow
                },
                new Account
                {
                    AccountNumber = 4101,
                    AccountType = AccountType.Checking,
                    CustomerID = 2100,
                    Balance = 500,
                    ModifyDate = DateTime.UtcNow
                },
                new Account
                {
                    AccountNumber = 4200,
                    AccountType = AccountType.Saving,
                    CustomerID = 2200,
                    Balance = 500.95m,
                    ModifyDate = DateTime.UtcNow
                },
                new Account
                {
                    AccountNumber = 4300,
                    AccountType = AccountType.Checking,
                    CustomerID = 2300,
                    Balance = 1250.50m,
                    ModifyDate = DateTime.UtcNow
                });

            const string initialDeposit = "Initial deposit";
            const string format = "dd/MM/yyyy hh:mm:ss tt";

            context.Transactions.AddRange(
                new Transaction
                {
                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4100,
                    Amount = 100,
                    Comment = initialDeposit,
                    TransactionTimeUtc = DateTime.ParseExact("08/06/2020 08:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4100,
                    Amount = 100,
                    Comment = initialDeposit,
                    TransactionTimeUtc = DateTime.ParseExact("09/06/2020 09:00:00 AM", format, null)
                },
                new Transaction
                {
                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4100,
                    Amount = 100,
                    Comment = initialDeposit,
                    TransactionTimeUtc = DateTime.ParseExact("09/06/2020 01:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4100,
                    Amount = 100,
                    Comment = initialDeposit,
                    TransactionTimeUtc = DateTime.ParseExact("09/06/2020 03:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4100,
                    Amount = 100,
                    Comment = initialDeposit,
                    TransactionTimeUtc = DateTime.ParseExact("10/06/2020 11:00:00 AM", format, null)
                },
                new Transaction
                {
                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4101,
                    Amount = 500,
                    Comment = initialDeposit,
                    TransactionTimeUtc = DateTime.ParseExact("08/06/2020 08:30:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4200,
                    Amount = 500,
                    Comment = initialDeposit,
                    TransactionTimeUtc = DateTime.ParseExact("08/06/2020 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4200,
                    Amount = 0.95m,
                    Comment = initialDeposit,
                    TransactionTimeUtc = DateTime.ParseExact("08/06/2020 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4300,
                    Amount = 1250.50m,
                    Comment = initialDeposit,
                    TransactionTimeUtc = DateTime.ParseExact("08/06/2020 10:00:00 PM", format, null)
                });
            context.Payees.AddRange(
                new Payee
                {
                    PayeeName = "Apple",
                    Address = "255 Apple st",
                    City = "Melbourne",
                    State = StateType.VIC,
                    PostCode = "3030",
                    Phone = "6100000000"
                },

                new Payee
                {
                    PayeeName = "Android",
                    Address = "300 Droid st",
                    City = "Melbourne",
                    State = StateType.VIC,
                    PostCode = "3333",
                    Phone = "6199999999"
                },

                new Payee
                {
                    PayeeName = "Google",
                    Address = "44 Google st",
                    City = "Sydney",
                    State = StateType.NSW,
                    PostCode = "3020",
                    Phone = "6123232323"
                });

            context.SaveChanges();
        }
    }
}
