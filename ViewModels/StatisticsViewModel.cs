using OilTeamProject.Models.Customers;
using OilTeamProject.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace OilTeamProject.ViewModels
{
    public class StatisticsViewModel
    {
        public Customer Customer { get; set; }
        public List<Order> Orders { get; set; }
        public int TotalOrders { get; set; }
        public int TotalCostPerCustomer { get; set; }        
        public double TotalOrderCost { get; set; }
        public int StatusTypeFullfilled { get; set; }
        public int StatusTypeInProgress { get; set; }

        public static IQueryable<StatisticsViewModel> CustomersStatistics(ApplicationDbContext _context)
        {
            List<Order> Orders = new List<Order>();

            IQueryable<StatisticsViewModel> data =
                from Order in _context.Orders
                group Order by Order.Customer into TotalOrders

                select new StatisticsViewModel
                {
                    Customer = TotalOrders.Key,
                    TotalOrders = TotalOrders.Count(),
                    TotalOrderCost = TotalOrders.Sum(t => t.TotalCost),
                    StatusTypeFullfilled = TotalOrders.Count(e => (int)e.Status == 0),
                    StatusTypeInProgress = TotalOrders.Count(e => (int)e.Status == 2)


                };

            return data;
        }
              
    }
}