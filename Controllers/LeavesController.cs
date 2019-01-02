using OilTeamProject.Persistence;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace OilTeamProject.Controllers
{
    public class LeavesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeavesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Leaves
        public ActionResult Index()
        {
            var requests = _context.Requests
                                .Include(r => r.Leave)
                                .Include(r => r.Employee)
                                .Where(r => r.Leave.EndDateOfLeave > DateTime.Now)
                                .ToList();

            return View(requests);
        }
    }
}