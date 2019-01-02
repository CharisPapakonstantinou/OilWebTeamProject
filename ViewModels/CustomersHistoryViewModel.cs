using OilTeamProject.Models.Customers;
using OilTeamProject.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OilTeamProject.ViewModels
{
    public class CustomersHistoryViewModel
    {
        public ICollection<CustomerBasicDetailsViewModel> CustomersList { get; set; }

        public CustomersHistoryViewModel()
        {
            CustomersList = new List<CustomerBasicDetailsViewModel>();
        }

        public static void GetCustomers(ApplicationDbContext _context, CustomersHistoryViewModel viewModel)
        {
            var customers = _context.Customers
              .Where(c => c.ActivityStatus == ActivityStatus.Active)
              .ToList();

            foreach (var customer in customers)
            {
                viewModel.CustomersList.Add(new CustomerBasicDetailsViewModel()
                {
                    CustomerID = customer.CustomerID,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    CompanyName = customer.CompanyName,
                    Country = customer.Country,
                    City = customer.City,
                    MemberCard = customer.MemberCard
                });
            }
        }



        public static void SortingCustomers(ApplicationDbContext _context, CustomersHistoryViewModel viewModel, string sortOrder)
        {
            switch (sortOrder)
            {
                case "lastName_desc":
                    viewModel.CustomersList = viewModel.CustomersList.OrderByDescending(c => c.LastName).ToList();
                    break;
                case "firstName_asc":
                    viewModel.CustomersList = viewModel.CustomersList.OrderBy(c => c.FirstName).ToList();
                    break;
                case "firstName_desc":
                    viewModel.CustomersList = viewModel.CustomersList.OrderByDescending(c => c.FirstName).ToList();
                    break;
                case "companyName_asc":
                    viewModel.CustomersList = viewModel.CustomersList.OrderBy(c => c.CompanyName).ToList();
                    break;
                case "companyName_desc":
                    viewModel.CustomersList = viewModel.CustomersList.OrderByDescending(c => c.CompanyName).ToList();
                    break;
                case "country_asc":
                    viewModel.CustomersList = viewModel.CustomersList.OrderBy(c => c.Country).ToList();
                    break;
                case "country_desc":
                    viewModel.CustomersList = viewModel.CustomersList.OrderByDescending(c => c.Country).ToList();
                    break;
                case "member_asc":
                    viewModel.CustomersList = viewModel.CustomersList.OrderBy(c => c.MemberCard.Type).ToList();
                    break;
                case "member_desc":
                    viewModel.CustomersList = viewModel.CustomersList.OrderByDescending(c => c.MemberCard.Type).ToList();
                    break;
                default:
                    viewModel.CustomersList = viewModel.CustomersList.OrderBy(c => c.LastName).ToList();
                    break;
            }
        }

        public static void SearchingCustomers(ApplicationDbContext _context, CustomersHistoryViewModel viewModel, string searchString)
        {
            foreach (var customer in viewModel.CustomersList)
            {
                if (customer.CompanyName == null)
                {
                    customer.CompanyName = "";
                }
            }

            if (!String.IsNullOrWhiteSpace(searchString))
            {
                viewModel.CustomersList = viewModel.CustomersList.Where(c => c.FullName.ToUpper().Contains(searchString.ToUpper())).ToList();
            }
        }
    }
}