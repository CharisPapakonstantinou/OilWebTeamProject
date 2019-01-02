using OilTeamProject.Models.Employees;
using OilTeamProject.Persistence;
using OilTeamProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace OilTeamProject.Areas.Admin.Controllers
{
    public class PerformancesController : Controller
    {
        private ApplicationDbContext _context;

        public PerformancesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Performances
        public ActionResult Index()
        {
            var performances = _context.Performances.Include(p => p.Employee).Include(p => p.Evaluation);
            return View(performances.ToList());
        }

        // GET: Performances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Performance performance = _context.Performances.Find(id);
            if (performance == null)
            {
                return HttpNotFound();
            }
            return View(performance);
        }

        // GET: Performances/Create
        public ActionResult Create(int? formId)
        {
            

            var viewModel = new PerformanceData()
            {
                Employees = _context.Employees.ToList(),
                Evaluations = _context.Evaluations.ToList(),
                Forms = _context.Forms.ToList(),
                DateTime = DateTime.Now,
                

            };

            if (formId != null)
            {
                if (formId == 0)
                {
                    
                }
                viewModel.FormId = (int)formId;
                viewModel.Questions = viewModel.GetFormQuestions((int)formId);
            }




            return View(viewModel);
        }

        // POST: Performances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PerformanceData performanceData)
          {
            if (performanceData.FormId == 0 ||  
                performanceData.Evaluation.ID == 0 ||
                performanceData.Employee.Id == 0)
            {
                performanceData.Evaluations = _context.Evaluations.ToList();
                performanceData.Employees = _context.Employees.ToList();
                performanceData.Forms = _context.Forms.ToList();



                return View("Create", performanceData);
            }
            
            var performance = new Performance()
            {
                FormID = performanceData.FormId,
                EmployeeID = performanceData.Employee.Id,
                EvaluationID = performanceData.Evaluation.ID,
                DateEvaluated = DateTime.Now,
                

            };
            performance.OveralRating = OverallRatingCalculate(performanceData.Answers);




            if(_context.Performances.Any(p => p.EmployeeID == performance.EmployeeID && p.EvaluationID == performance.EvaluationID))
            {
                performanceData.ExistingPerformance = true;
                performanceData.Forms = _context.Forms;
                performanceData.Employees = _context.Employees;
                performanceData.Evaluations = _context.Evaluations.ToList();
                performanceData.Questions = performanceData.GetFormQuestions(performanceData.FormId);
                return View("Create", performanceData);
            }

            _context.Performances.Add(performance);
            _context.SaveChanges();

            for (int i = 0; i < performanceData.Questions.Count; i++)
            {
                performanceData.Answers[i].QuestionID = performanceData.Questions[i].ID;
            }

            foreach (var answer in performanceData.Answers)
            {
                answer.PerformanceID = performance.ID;
                _context.Answers.Add(answer);
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Evaluations");
        }

        public int OverallRatingCalculate(List<Answer> answers)
        {
            int allValuesOfAnswers = 0;
            foreach ( var answer in answers)
            {
                allValuesOfAnswers += (int)answer.QuestionAnswer;
            }

            var finalRating = allValuesOfAnswers / answers.Count();
            finalRating = finalRating * 100 / 5;
            return finalRating;
        }

        // GET: Performances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Performance performance = _context.Performances.Find(id);
            if (performance == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(_context.Employees, "ID", "Name", performance.EmployeeID);
            ViewBag.EvaluationID = new SelectList(_context.Evaluations, "EvaluationID", "EvaluationID", performance.EvaluationID);
            return View(performance);
        }

        // POST: Performances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EmployeeID,EvaluationID,OveralRating,DateEvaluated")] Performance performance)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(performance).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(_context.Employees, "ID", "Name", performance.EmployeeID);
            ViewBag.EvaluationID = new SelectList(_context.Evaluations, "EvaluationID", "EvaluationID", performance.EvaluationID);
            return View(performance);
        }

        // GET: Performances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Performance performance = _context.Performances.Find(id);
            if (performance == null)
            {
                return HttpNotFound();
            }
            return View(performance);
        }

        // POST: Performances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Performance performance = _context.Performances.Find(id);
            _context.Performances.Remove(performance);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
