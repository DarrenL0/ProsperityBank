using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProsperityBank.Models
{

    public enum StateType
    {
        VIC = 1,
        NSW = 2,
        SA = 3,
        QLD = 4,
        TAS =5
    }
    public class Customer
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Customer ID")]
        public int CustomerId { get; set; }

        [Required, StringLength(50)]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [StringLength(11)]
        public string TFN { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(40)]
        public string City { get; set; }

        [StringLength(1)]
        public StateType State { get; set; }

        [StringLength(4)]
        public string Postcode { get; set; }

        [Required, StringLength(15)]
        [RegularExpression(@"^(61)-\d{​​​​9}​​​​$", ErrorMessage = "Must be of the format:(61)-XXXXXXXXX")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public virtual List<Account> Accounts { get; set; }

    }
}
