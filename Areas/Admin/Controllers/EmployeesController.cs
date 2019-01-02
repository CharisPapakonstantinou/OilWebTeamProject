using OilTeamProject.Models.Employees;
using OilTeamProject.Persistence;
using OilTeamProject.Repositories;
using OilTeamProject.ViewModels;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace OilTeamProject.Areas.Admin.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationDbContext _context;

        private readonly EmployeeRepository _employeeRepository;

        private readonly RoleRepository _roleRepository;

        private readonly DepartmentRepository _departmentRepository;

        public EmployeesController()
        {
            _context = new ApplicationDbContext();
            _employeeRepository = new EmployeeRepository(_context);
            _roleRepository = new RoleRepository(_context);
            _departmentRepository = new DepartmentRepository(_context);
        }



        [HttpPost]
        public ActionResult Search(EmployeeFormViewModel viewModel)
        {
            return RedirectToAction("Index", "Employees", new { query = viewModel.SearchTerm });//stelnw ston controller tou home to searchTerm
        }

        public ActionResult MyProjects(int? Id)
        {
            if (Id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var employee = _employeeRepository.EmployeeProjects(Id);

            if (employee == null)
                return HttpNotFound();

            return View(employee);
        }

        public ActionResult MyLeaves(int? Id)
        {
            if (Id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var employee = _employeeRepository.EmployeeUpcomingLeaves(Id);

            if (employee == null)
                return HttpNotFound();

            return View(employee);
        }

        public ActionResult MyShifts(int? Id)
        {
            if (Id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var employee = _employeeRepository.EmployeeUpcomingShifts(Id);

            if (employee == null)
                return HttpNotFound();

            return View(employee);
        }

        public ActionResult RequestLeave()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequestLeave([Bind(Include = "StartDateOfLeave,EndDateOfLeave,Type,Description")] Leave leave, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _employeeRepository.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                var request = employee.MakeARequestForLeave(leave);

                _context.Leaves.Add(leave);
                _context.Requests.Add(request);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(leave);
        }

        // GET: Employees
        public ActionResult Index(string query)
        {
            var employees = _employeeRepository.AllEmployees();

            if (!string.IsNullOrEmpty(query))
            {
                employees = employees
                    .Where(g =>
                        g.LastName.Contains(query) ||
                        g.Department.Name.Contains(query) ||
                        g.Role.Name.Contains(query))
                        ;
            }

            var viewModel = new EmployeeFormViewModel
            {
                Employees = employees,
                SearchTerm = query
            };

            return View("Index", viewModel);
        }

        public ActionResult Create()
        {
            var viewModel = new EmployeeFormViewModel
            {
                Departments = _departmentRepository.GetDepartments(),
                Roles = _roleRepository.GetRoles(),
                Heading = "Add Employee"
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Departments = _departmentRepository.GetDepartments();
                viewModel.Roles = _roleRepository.GetRoles();
                return View("EmployeeForm", viewModel);
            }

            var employee = new Employee(viewModel);

            _employeeRepository.Add(employee);
            _context.SaveChanges();

            return RedirectToAction("Index", "Employees");
        }

        public ActionResult Edit(int Id)
        {
            var employee = _employeeRepository.EmployeeUpcomingShifts(Id);

            var viewModel = new EmployeeFormViewModel(employee);

            viewModel.Departments = _departmentRepository.GetDepartments();
            viewModel.Roles = _roleRepository.GetRoles();

            viewModel.NumberOfShifts = _employeeRepository.EmployeeUpcomingShifts(Id).Works.Count;
            viewModel.NumberOfProjects = _employeeRepository.EmployeeProjects(Id).Assignments.Count;

            return View("EmployeeForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(EmployeeFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Roles = _roleRepository.GetRoles();
                viewModel.Departments = _departmentRepository.GetDepartments();
                return View("EmployeeForm", viewModel);
            }

            var employee = _employeeRepository.EmployeeUpcomingShifts(viewModel.Id);

            employee.EmployeeModify(viewModel);

            _context.SaveChanges();

            return RedirectToAction("Index", "Employees");
        }
    }
}