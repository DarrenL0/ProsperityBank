using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ProsperityBank_API.Model
{
    public partial class Customer
    {
        public Customer()
        {
            Accounts = new HashSet<Account>();
            Logins = new HashSet<Login>();
        }

        [Display(Name = "Customer ID")]
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Tfn { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int State { get; set; }
        [Display(Name = "Post Code")]
        public string Postcode { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Login> Logins { get; set; }
    }
}
