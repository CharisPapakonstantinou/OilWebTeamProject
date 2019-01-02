using OilTeamProject.Models.Customers;
using System;
using System.ComponentModel.DataAnnotations;

namespace OilTeamProject.ViewModels
{
    public class CustomerProfileViewModel
    {
        public int CustomerID { get; set; }

        [Display(Name = "First Νame")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Company")]
        public string CompanyName { get; set; }

        [Display(Name = "Company Type")]
        public CommercialType? CompanyType { get; set; }

        public Gender Gender { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        [Display(Name = "Postal Code")]
        [DataType(DataType.PostalCode)]
        public int PostalCode { get; set; }

        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public DateTime CreationDate { get; set; }

        [Display(Name = "Fulfilled")]
        public int FulfilledOrders { get; set; }

        [Display(Name = "In Progress")]
        public int InProgressOrders { get; set; }

        [Display(Name = "Deleted")]
        public int DeletedOrders { get; set; }

        [Display(Name = "Total")]
        public int TotalOrders { get; set; }

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