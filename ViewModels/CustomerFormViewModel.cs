using OilTeamProject.Models.Customers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OilTeamProject.ViewModels
{
    public class CustomerFormViewModel
    {
        public int CustomerID { get; set; }

        [Required]
        [Display(Name = "First Νame")]
        [StringLength(30, ErrorMessage = "'First Name' must be between 3-30 characters long", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(30, ErrorMessage = "'Last Name' must be between 3-30 characters long", MinimumLength = 3)]
        public string LastName { get; set; }

        [Display(Name = "Company")]
        [StringLength(50, ErrorMessage = "'Company Name' must be between 3-30 characters long", MinimumLength = 3)]
        public string CompanyName { get; set; }

        [Display(Name = "Company Type")]
        public CommercialType? CompanyType { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        //[ValidDate]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "'Country' must be between 3-30 characters long", MinimumLength = 3)]
        public string Country { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "'City' must be between 3-30 characters long", MinimumLength = 3)]
        public string City { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "'Address' must be between 3-30 characters long", MinimumLength = 3)]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        [DataType(DataType.PostalCode)]
        public int PostalCode { get; set; }

        [Required]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
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

        public CreditCard CreditCard { get; set; }
        public ICollection<Order> Orders { get; set; }
        public MemberCard MemberCard { get; set; }

        public string Heading { get; set; }

        public string Action
        {
            get
            {
                return (CustomerID != 0) ? "Update" : "Create";
            }
        }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public DateTime GetCreationDate()
        {
            return CreationDate = DateTime.Now;
        }
        public CustomerFormViewModel()
        {
            Orders = new List<Order>();
        }
    }
}