using OilTeamProject.Models.Customers;
using System.ComponentModel.DataAnnotations;

namespace OilTeamProject.ViewModels
{
    public class CustomerBasicDetailsViewModel
    {
        public int CustomerID { get; set; }

        public ActivityStatus ActivityStatus { get; set; }

        [Display(Name = "First Νame")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Company")]
        public string CompanyName { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public MemberCard MemberCard { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}