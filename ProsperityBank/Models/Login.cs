using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProsperityBank.Models
{
    public class Login
    {
        [Required, StringLength(8)]
        [Display(Name = "Login ID")]
        public string LoginID { get; set; }

        [Display(Name = "Customer ID")]
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        [Required, StringLength(64)]
        public string PasswordHash { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        [Display(Name = "Creation Time")]
        [DataType(DataType.DateTime)]
        public DateTime CreationTimeUtc { get; set; }

        [Column(TypeName = "datetime2")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Locked Until")]
        public DateTime LockedUntil { get; set; }
    }
}
