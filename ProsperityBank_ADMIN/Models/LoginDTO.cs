using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProsperityBank_ADMIN.Models
{
    public class LoginDTO
    {
        [Required, StringLength(8)]
        [Display(Name = "Login ID")]
        public string LoginID { get; set; }

        [ForeignKey("Customer")]
        public int CustomerID { get; set; }

        [Required, StringLength(64)]
        public string PasswordHash { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        [DataType(DataType.DateTime)]
        public DateTime CreationTimeUtc { get; set; }
        public DateTime LockedUntil { get; set; }
    }
}
