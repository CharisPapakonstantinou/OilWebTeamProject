using OilTeamProject.Persistence;
using System;
using System.Linq;

namespace OilTeamProject.ViewModels
{
    public class OrdersHistoryViewModel
    {
        public CustomerFormViewModel Customer { get; set; }

        public static void GetCustomer(ApplicationDbContext _context, int? id, OrdersHistoryViewModel viewModel)
        {
            var customer = _context.Customers
                                .Single(c => c.CustomerID == id);

            viewModel.Customer = new CustomerFormViewModel()
            {
                CustomerID = customer.CustomerID,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                CompanyName = customer.CompanyName,
                CompanyType = customer.CompanyType,
                Gender = customer.Gender,
                DateOfBirth = customer.DateOfBirth,
                Country = customer.Country,
                City = customer.City,
                Address = customer.Address,
                PostalCode = customer.PostalCode,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                Orders = customer.Orders
            };

        }

        public static void SortingOrders(ApplicationDbContext _context, OrdersHistoryViewModel viewModel, string sortOrder)
        {
            switch (sortOrder)
            {
                case "orderNumber_desc":
                    viewModel.Customer.Orders = viewModel.Customer.Orders.OrderByDescending(o => o.OrderNumber).ToList();
                    break;
                case "orderDate_asc":
                    viewModel.Customer.Orders = viewModel.Customer.Orders.OrderBy(o => o.CreationDate).ToList();
                    break;
                case "orderDate_desc":
                    viewModel.Customer.Orders = viewModel.Customer.Orders.OrderByDescending(o => o.CreationDate).ToList();
                    break;
                case "orderStatus_asc":
                    viewModel.Customer.Orders = viewModel.Customer.Orders.OrderBy(o => o.Status).ToList();
                    break;
                case "orderStatus_desc":
                    viewModel.Customer.Orders = viewModel.Customer.Orders.OrderByDescending(o => o.Status).ToList();
                    break;
                case "orderType_asc":
                    viewModel.Customer.Orders = viewModel.Customer.Orders.OrderBy(o => o.Type).ToList();
                    break;
                case "orderType_desc":
                    viewModel.Customer.Orders = viewModel.Customer.Orders.OrderByDescending(o => o.Type).ToList();
                    break;
                case "orderPaidOff_asc":
                    viewModel.Customer.Orders = viewModel.Customer.Orders.OrderBy(o => o.PaidOff).Distinct().ToList();
                    break;
                case "orderPaidOff_desc":
                    viewModel.Customer.Orders = viewModel.Customer.Orders.OrderByDescending(o => o.PaidOff).Distinct().ToList();
                    break;
                case "orderPaymentDate_asc":
                    viewModel.Customer.Orders = viewModel.Customer.Orders.OrderBy(o => o.PaymentDate).ToList();
                    break;
                case "orderPaymentDate_desc":
                    viewModel.Customer.Orders = viewModel.Customer.Orders.OrderByDescending(o => o.PaymentDate).ToList();
                    break;
                default:
                    viewModel.Customer.Orders = viewModel.Customer.Orders.OrderBy(o => o.OrderNumber).ToList();
                    break;
            }
        }

        public static void SearchingOrders(ApplicationDbContext _context, OrdersHistoryViewModel viewModel, string searchString, string selectedOrderType, string selectedOrderStatus)
        {


            if (!String.IsNullOrWhiteSpace(searchString))
            {
                viewModel.Customer.Orders = viewModel.Customer.Orders.Where(o => o.OrderNumber.ToString().Contains(searchString)).ToList();
            }

            if (!String.IsNullOrWhiteSpace(selectedOrderType))
            {
                viewModel.Customer.Orders = viewModel.Customer.Orders.Where(o => o.Type.ToString() == selectedOrderType).ToList();
            }

            if (!String.IsNullOrWhiteSpace(selectedOrderStatus))
            {
                viewModel.Customer.Orders = viewModel.Customer.Orders.Where(o => o.Status.ToString() == selectedOrderStatus).ToList();
            }
        }
    }
}