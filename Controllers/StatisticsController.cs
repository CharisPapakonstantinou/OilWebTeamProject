using ClosedXML.Excel;
using OilTeamProject.Persistence;
using OilTeamProject.ViewModels;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace OilTeamProject.Controllers
{
    public class StatisticsController : Controller
    {        
        ApplicationDbContext _context = new ApplicationDbContext();

        public StatisticsController()
        {
            _context = new ApplicationDbContext();
        }
        
        // GET: Statistics
        public ActionResult Customers(string sortOrder)
        {
            var data = StatisticsViewModel.CustomersStatistics(_context);
            return View(data);

        }
        [HttpPost]
        public ActionResult ExportToExcel()
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("Customer Name"),
                                            new DataColumn("Total Orders"),
                                            new DataColumn("Total Orders Cost Summary"),
                                            new DataColumn("Orders Status Fullfilleds"),
                                            new DataColumn("Orders Status Pending")});
            var data = _context.Customers.ToList();

            foreach (var customer in data)
            {
                if (customer.Orders.Count != 0)
                {
                    dt.Rows.Add(customer.FullName, customer.Orders.Count, customer.Orders.Sum(t => t.TotalCost), customer.Orders.Count(e => (int)e.Status == 0),
                    customer.Orders.Count(e => (int)e.Status == 2));
                }

            }
            // using from NugetPackage :  Install-Package ClosedXML
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Statistics.xlsx");
                }
            }
        }
    }
}