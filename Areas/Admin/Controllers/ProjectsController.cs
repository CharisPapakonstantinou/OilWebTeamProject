using OilTeamProject.Models.Employees;
using OilTeamProject.Persistence;
using OilTeamProject.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace OilTeamProject.Areas.Admin.Controllers
{

    public class ProjectsController : Controller
    {

        public readonly ApplicationDbContext _context;

        public ProjectsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult MyTodayProject(int id)
        {
            var assignments = _context.Assignments
                .Include(a => a.Employee)
                .Include(a => a.Project)
                .Where(a => a.EmployeeId == id && a.Project.DueDate > DateTime.Now && a.Project.StartingDate <= DateTime.Now && a.Project.EndDate == null);

            return View(assignments);
        }

        public ActionResult TodayProjectsByDepartment(ProjectFormViewModel viewModel)
        {
            var projects = _context.Projects
                .Include(p => p.Department)
                .Where(p => p.EndDate == null && p.StartingDate <= DateTime.Now && p.DepartmentId == viewModel.DepartmentId && p.EndDate == null)
                .ToList();

            return View(projects);
        }

        //Get Details Of Project
        public ActionResult Details(int id)
        {
            var project = _context.Projects
                .Where(p => p.Id == id)
                .Single();

            var projectAssigments = _context.Assignments
                .Include(a => a.Employee)
                .Where(a => a.ProjectId == project.Id)
                .ToList();

           

            var viewModel = new ProjectDetailsViewModel
            {
                Project = project,
                ProjectsEmployees = projectAssigments
            };

            return View(viewModel);
        }


        //Get All TodayProjects For Today
        public ActionResult GetTodayProjects(ProjectFormViewModel viewModel)
        {
           
                var indexviewModel = new ProjectFormViewModel
                {
                    Departments = _context.Departments.ToList()
                };

                return View(indexviewModel);
          
           
        }

        //Create Get
        public ActionResult CreateProject()
        {
            var viewModel = new ProjectFormViewModel
            {
                Departments = _context.Departments.ToList()
            };
            return View(viewModel);
        }

        //Create a Project
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProject(ProjectFormViewModel viewModel) {

            var newProject = new Project(viewModel);

            _context.Projects.Add(newProject);
            _context.SaveChanges();
            return RedirectToAction("Index","Projects");
        }

        public ActionResult Index()
        {
            var projects = _context.Projects.ToList();

            return View(projects);
        }

    }
}
