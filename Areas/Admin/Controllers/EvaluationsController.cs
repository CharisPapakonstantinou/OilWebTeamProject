using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OilTeamProject.Models.Employees;
using OilTeamProject.Persistence;
using OilTeamProject.ViewModels;

namespace OilTeamProject.Areas.Admin.Controllers
{
    public class EvaluationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Evaluations
        public ActionResult Index(int? id, int? performanceId)
        {
            var evaluations = db.Evaluations
                //.Include(e => e.Performances)
                .OrderByDescending(e => e.ID);
                

            var viewModel = new EvaluationsData()
            {
                Evaluations = evaluations

            };

            if (id != null)
            {
                ViewBag.EvaluationID = id.Value;
                viewModel.Performances = viewModel.Evaluations
                    .Where(e => e.ID == id.Value)
                    .Single()
                    .Performances;
            }

            if (performanceId != null)
            {
                ViewBag.PerformanceId = performanceId.Value;
                viewModel.Questions = viewModel.Performances
                    .Where(p => p.ID == performanceId.Value)
                    .Single()
                    .Form
                    .Questions
                    .ToList();
                //New Dashboard
                var thisEmployee = db.Performances.Find(performanceId).EmployeeID;
                var listOfPerformancesOfThisEmployee = db.Performances.Where(p => p.EmployeeID == thisEmployee).OrderByDescending(p => p.DateEvaluated).ToList();
                ViewBag.listOfRatings = listOfPerformancesOfThisEmployee.Select(p => p.OveralRating).ToList();
                ViewBag.EvaluationsId = db.Evaluations.OrderByDescending(e => e.ID).Select(e => e.ID).ToList().Take(6);


                foreach (var item in viewModel.Questions)
                {
                    item.AnswersForAQuestion = item.Answers.Where(a => a.QuestionID == item.ID && a.PerformanceID == performanceId).SingleOrDefault();
                }
                    
            }

            return View(viewModel);
        }
        
            

        // GET: Evaluations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluation evaluation = db.Evaluations.Find(id);
            if (evaluation == null)
            {
                return HttpNotFound();
            }
            return View(evaluation);
        }

        // GET: Evaluations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Evaluations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StartEvaluationDate,EndEvaluationDate")] Evaluation evaluation)
        {
            if (ModelState.IsValid)
            {
                db.Evaluations.Add(evaluation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(evaluation);
        }

        // GET: Evaluations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluation evaluation = db.Evaluations.Find(id);
            if (evaluation == null)
            {
                return HttpNotFound();
            }
            return View(evaluation);
        }

        // POST: Evaluations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EvaluationID,StartEvaluationDate,EndEvaluationDate")] Evaluation evaluation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evaluation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(evaluation);
        }

        public ActionResult Delete(int id)
        {
            Evaluation evaluation = db.Evaluations.Find(id);
            db.Evaluations.Remove(evaluation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
