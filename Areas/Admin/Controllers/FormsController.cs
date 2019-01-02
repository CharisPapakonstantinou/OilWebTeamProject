using OilTeamProject.Models;
using OilTeamProject.Models.Employees;
using OilTeamProject.Persistence;
using OilTeamProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OilTeamProject.Areas.Admin.Controllers
{
    public class FormsController : Controller
    {
        private ApplicationDbContext _context;

        public FormsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Forms
        public ActionResult Index()
        {


            return View();
        }

        [HttpGet]
        public ActionResult Create(bool? createNewQuestion)
        {
            var questions = _context.Questions.ToList();

            var viewModel = new EvaluationsData();

            //viewModel.Questions = questions;

            //foreach (var item in questions)
            //{
            //    viewModel.SelectedQuestions = new List<SelectListItem>
            //    {
            //        new SelectListItem {Text = item.Text, Value = item.ID.ToString()}
            //    };
            //}

            var selectedQuestions = new List<SelectListItem>();



            foreach (var item in questions)
            {
                selectedQuestions.Add(new SelectListItem { Text = item.Text, Value = item.ID.ToString() });
            }


            viewModel.SelectedQuestions = selectedQuestions;

            //if (createNewQuestion == true)
            //{
            //    ViewBag.NewQuestionTextBox = viewModel.CreateNewQuestion;
            //}


            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EvaluationsData viewModel)
        {
            var questionsFromViewModel = new List<Question>();
            var questions = _context.Questions.ToList();

            viewModel.CheckedQuestions.ToList();
            foreach (var item in viewModel.CheckedQuestions)
            {
                var itemNumber = Int32.Parse(item);
                var questionIDs = _context.Questions.Find(itemNumber);
                questionsFromViewModel.Add(questionIDs);
            }


            var form = new Form()
            {
                Theme = viewModel.Form.Theme,
                Questions = questionsFromViewModel

            };

            _context.Forms.Add(form);
            _context.SaveChanges();

            return RedirectToAction("Create", "Performances");
        }



        //[HttpPost]
        //public ActionResult Create(EvaluationsData evaluationsData, FormCollection fc)
        //{
        //    string text = fc["txttest"];


        //    return RedirectToAction("Create", "Performances");
        //}
    }
}