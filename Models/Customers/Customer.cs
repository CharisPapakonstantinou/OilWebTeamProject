using OilTeamProject.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OilTeamProject.Models.Customers
{
    public enum ActivityStatus
    {
        Active = 1,
        Inactive = 2
    }

    public enum CommercialType
    {
        Leisure = 1,
        Industrial = 2,
        HealthCare = 3,
        Hospitality = 4
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }

    public class Customer
    {
        public int CustomerID { get; set; }

        public ActivityStatus ActivityStatus { get; set; }

        [Required]
        [Display(Name = "First Νame")]
        [StringLength(30, ErrorMessage = "'First Name' must be between 3-30 characters long", MinimumLength = 3)]
        public string FirstName { get; private set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(30, ErrorMessage = "'Last Name' must be between 3-30 characters long", MinimumLength = 3)]
        public string LastName { get; private set; }

        [Display(Name = "Company")]
        [StringLength(50, ErrorMessage = "'Company Name' must be between 3-30 characters long", MinimumLength = 3)]
        public string CompanyName { get; private set; }

        [Display(Name = "Company Type")]
        public CommercialType? CompanyType { get; private set; }

        [Required]
        public Gender Gender { get; private set; }

        [Required]
        [Display(Name = "Date of Birth")]
        //[ValidDate]
        public DateTime DateOfBirth { get; private set; }

        [Required]
        [StringLength(30, ErrorMessage = "'Country' must be between 3-30 characters long", MinimumLength = 3)]
        public string Country { get; private set; }

        [Required]
        [StringLength(30, ErrorMessage = "'City' must be between 3-30 characters long", MinimumLength = 3)]
        public string City { get; private set; }

        [Required]
        [StringLength(40, ErrorMessage = "'Address' must be between 3-30 characters long", MinimumLength = 3)]
        public string Address { get; private set; }

        [Required]
        [Display(Name = "Postal Code")]
        [DataType(DataType.PostalCode)]
        public int PostalCode { get; private set; }

        [Required]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; private set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; private set; }

        public DateTime CreationDate { get; private set; }

        public virtual CreditCard CreditCard { get; private set; }
        public virtual ICollection<Order> Orders { get; private set; }
        public virtual MemberCard MemberCard { get; private set; }

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

        public Customer()
        {

        }

        public Customer(ActivityStatus status, string firstName, string lastName, string companyName,
                        CommercialType? type, Gender gender, DateTime dateOfBirth, string country,
                        string city, string address, int postalCode, string phoneNumber, string email,
                        DateTime creationDate)
        {
            ActivityStatus = status;
            FirstName = firstName;
            LastName = lastName;
            CompanyName = companyName;
            CompanyType = type;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            Country = country;
            City = city;
            Address = address;
            PostalCode = postalCode;
            PhoneNumber = phoneNumber;
            Email = email;
            CreationDate = GetCreationDate();
        }

        public static Customer Create(CustomerFormViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            return new Customer(ActivityStatus.Active, viewModel.FirstName, viewModel.LastName, viewModel.CompanyName,
                                viewModel.CompanyType, viewModel.Gender, viewModel.DateOfBirth, viewModel.Country,
                                viewModel.City, viewModel.Address, viewModel.PostalCode, viewModel.PhoneNumber, viewModel.Email,
                                viewModel.CreationDate);
        }

        public static Customer Modify(Customer customer, CustomerFormViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            if (customer == null)
            {
                throw new ArgumentNullException("customer");
            }

            customer.FirstName = viewModel.FirstName;
            customer.LastName = viewModel.LastName;
            customer.CompanyName = viewModel.CompanyName;
            customer.CompanyType = viewModel.CompanyType;
            customer.Gender = viewModel.Gender;
            customer.DateOfBirth = viewModel.DateOfBirth;
            customer.Country = viewModel.Country;
            customer.City = viewModel.City;
            customer.Address = viewModel.Address;
            customer.PostalCode = viewModel.PostalCode;
            customer.PhoneNumber = viewModel.PhoneNumber;
            customer.Email = viewModel.Email;

            return customer;
        }

        public static Customer Remove(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("customer");
            }

            customer.ActivityStatus = ActivityStatus.Inactive;

            return customer;
        }
    }
}