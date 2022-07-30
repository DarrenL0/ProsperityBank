using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProsperityBank_ADMIN.Models
{
    public enum StateType
    {
        Vic = 1,
        NSW = 2,
        SA = 3,
        QLD = 4,
        TAS = 5
    }
    public class CustomerDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustomerID { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [StringLength(11)]
        public string TFN { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(40)]
        public string City { get; set; }

        public StateType State { get; set; }

        [StringLength(4)]
        public string PostCode { get; set; }

        [Required, StringLength(15)]
        [RegularExpression("+61")]
        public string PhoneNumber { get; set; }



    }
}
