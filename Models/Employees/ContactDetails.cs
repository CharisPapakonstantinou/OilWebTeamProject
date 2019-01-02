using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OilTeamProject.Models.Employees
{
    public class ContactDetails
    {
        [Key]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        [Display(Name = "Mobile Phone")]
        public string MobilePhone { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
    }
}