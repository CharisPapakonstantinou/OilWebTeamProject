using OilTeamProject.Models.Employees;
using OilTeamProject.Persistence;
using OilTeamProject.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace OilTeamProject.Areas.Admin.Controllers
{
    public class QuestionsController : Controller
    {
        private ApplicationDbContext _context;

        public QuestionsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Questions
        public ActionResult Index()
        {

            return View();
        }


        public ActionResult Create()
        {

            var viewModel = new EvaluationsData()
            {
                Questions = _context.Questions.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EvaluationsData viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Questions = _context.Questions.ToList();
                return View("Create", viewModel);
            }

            var question = new Question()
            {
                Text = viewModel.Question.Text
            };

            //I will send it to the database
            _context.Questions.Add(question);
            _context.SaveChanges();

            //where will I send the user?

            return RedirectToAction("Create", "Forms");
        }


    }
}