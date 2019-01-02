using OilTeamProject.Models;
using OilTeamProject.Models.Customers;
using OilTeamProject.Persistence;
using System.Data;
using System.Linq;
using System.Web.Http;

namespace OilTeamProject.Controllers.api
{
    public class CustomersController : ApiController
    {
        public ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Remove(int? id)
        {
            var customer = _context.Customers
                .Single(c => c.CustomerID == id && c.ActivityStatus == ActivityStatus.Active);
            Customer.Remove(customer);

            try
            {
                _context.SaveChanges();
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes");
            }

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Delete(int? id)
        {
            var customer = _context.Customers.Find(id);

            _context.Customers.Remove(customer);

            try
            {
                _context.SaveChanges();
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes");
            }

            return Ok();
        }
    }
}
