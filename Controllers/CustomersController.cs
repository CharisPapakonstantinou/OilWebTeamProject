using OilTeamProject.Models.Customers;
using OilTeamProject.Persistence;
using OilTeamProject.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace OilTeamProject.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            var viewModel = new CustomersHistoryViewModel();
            CustomersHistoryViewModel.GetCustomers(_context, viewModel);

            if (viewModel == null)
            {
                return HttpNotFound();
            }

            ViewBag.LastNameSortParam = String.IsNullOrEmpty(sortOrder) ? "lastName_desc" : "";
            ViewBag.FirstNameSortParam = sortOrder == "firstName_asc" ? "firstName_desc" : "firstName_asc";
            ViewBag.CompanyNameSortParam = sortOrder == "companyName_asc" ? "companyName_desc" : "companyName_asc";
            ViewBag.CountrySortParam = sortOrder == "country_asc" ? "country_desc" : "country_asc";
            ViewBag.MemberSortParam = sortOrder == "member_asc" ? "member_desc" : "member_asc";

            ViewBag.CurrentSort = sortOrder;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            int pageSize = 8;
            int pageNumber = page ?? 1;

            CustomersHistoryViewModel.SearchingCustomers(_context, viewModel, searchString);
            CustomersHistoryViewModel.SortingCustomers(_context, viewModel, sortOrder);

            return View(viewModel.CustomersList.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            var viewModel = new CustomerFormViewModel
            {
                Heading = "Add new Customer"
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("CustomerForm", viewModel);
            }

            var customer = Customer.Create(viewModel);

            try
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes");
            }

            return RedirectToAction("CustomerProfile", new { id = customer.CustomerID });
        }

        public ActionResult CustomerProfile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customer = _context.Customers.Find(id);


            if (customer == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CustomerProfileViewModel
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
                FulfilledOrders = customer.Orders.Where(o => o.Status == OrderStatus.Fulfilled).Count(),
                InProgressOrders = customer.Orders.Where(o => o.Status == OrderStatus.InProgress).Count(),
                DeletedOrders = customer.Orders.Where(o => o.Status == OrderStatus.Deleted).Count(),
                TotalOrders = customer.Orders.Count(),
                MemberCard = customer.MemberCard
            };

            return View(viewModel);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customer = _context.Customers.Single(c => c.CustomerID == id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CustomerFormViewModel
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
                Heading = "Update Customer"
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CustomerFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("CustomerForm", viewModel);
            }

            var customer = _context.Customers.Single(c => c.CustomerID == viewModel.CustomerID);
            customer = Customer.Modify(customer, viewModel);

            try
            {
                _context.SaveChanges();
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes");
            }

            return RedirectToAction("CustomerProfile", new { id = customer.CustomerID });
        }

        public ActionResult OrdersHistory(int? id, string sortOrder, string searchString, string selectedOrderType, string selectedOrderStatus, string currentFilter, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = new OrdersHistoryViewModel();
            OrdersHistoryViewModel.GetCustomer(_context, id, viewModel);

            if (viewModel == null)
            {
                return HttpNotFound();
            }

            ViewBag.Customer = viewModel.Customer;
            ViewBag.OrderNumberSortParam = String.IsNullOrEmpty(sortOrder) ? "orderNumber_desc" : "";
            ViewBag.OrderDateSortParam = sortOrder == "orderDate_asc" ? "orderDate_desc" : "orderDate_asc";
            ViewBag.OrderStatusSortParam = sortOrder == "orderStatus_asc" ? "orderStatus_desc" : "orderStatus_asc";
            ViewBag.OrderTypeSortParam = sortOrder == "orderType_asc" ? "orderType_desc" : "orderType_asc";
            ViewBag.OrderPaidOffSortParam = sortOrder == "orderPaidOff_asc" ? "orderPaidOff_desc" : "orderPaidOff_asc";
            ViewBag.OrderPaymentDateSortParam = sortOrder == "orderPaymentDate_asc" ? "orderPaymentDate_desc" : "orderPaymentDate_asc";
            ViewBag.OrderPaymentSortParam = sortOrder == "orderPayment_asc" ? "orderPayment_desc" : "orderPayment_asc";

            List<OrderType> myOrderType = Enum.GetValues(typeof(OrderType)).Cast<OrderType>().ToList();
            ViewBag.OrderType = new SelectList(myOrderType);

            List<OrderStatus> myOrderStatus = Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>().ToList();
            ViewBag.OrderStatus = new SelectList(myOrderStatus);

            ViewBag.OrderCurrentSort = sortOrder;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.OrderCurrentFilter = searchString;

            ViewBag.Type = selectedOrderType;
            ViewBag.Status = selectedOrderStatus;

            int pageSize = 6;
            int pageNumber = page ?? 1;

            OrdersHistoryViewModel.SearchingOrders(_context, viewModel, searchString, selectedOrderType, selectedOrderStatus);
            OrdersHistoryViewModel.SortingOrders(_context, viewModel, sortOrder);

            return View(viewModel.Customer.Orders.ToPagedList(pageNumber, pageSize));
        }
    }
}