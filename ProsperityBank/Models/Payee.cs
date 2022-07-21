using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProsperityBank.Models
{
    public class Payee
    {
        [Display(Name = "Payee ID")]
        public int PayeeID { get; init; }

        [Required, StringLength(50)]
        [Display(Name = "Payee Name")]
        public string PayeeName { get; init; }

        [StringLength(50)]
        public string Address { get; init; }

        [StringLength(40)]
        public string City { get; init; }

        [StringLength(1)]
        public StateType State { get; init; }

        [StringLength(4)]
        public string PostCode { get; init; }

        [Required, StringLength(15)]
        [RegularExpression(@"^(61)-\d{​​​​9}​​​​$", ErrorMessage = "Must be of the format:(61)-XXXXXXXXX")]
        public string Phone { get; init; }

        public virtual List<BillPay> BillPays { get; set; }
    }
}
