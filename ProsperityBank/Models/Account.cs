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
}
