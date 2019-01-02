using OilTeamProject.Models.Employees;
using OilTeamProject.Persistence;
using OilTeamProject.Repositories;
using OilTeamProject.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace OilTeamProject.Areas.Admin.Controllers
{
    public class ShiftsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly ShiftRepository _shiftRepository;

        private readonly EmployeeRepository _employeeRepository;

        private readonly DepartmentRepository _departmentRepository;

        private readonly WorkRepository _workRepository;

        private readonly ShiftTypeRepository _shiftTypeRepository;

        public ShiftsController()
        {
            _context = new ApplicationDbContext();
            _shiftRepository = new ShiftRepository(_context);
            _employeeRepository = new EmployeeRepository(_context);
            _departmentRepository = new DepartmentRepository(_context);
            _workRepository = new WorkRepository(_context);
            _shiftTypeRepository = new ShiftTypeRepository(_context);
        }

        //Action To find Shift for New Work
        public ActionResult FindShiftForNewWork(AssignShiftEmployeesViewModel viewModel)
        {
            var datetime = viewModel.Datetime;
            var departmentId = _departmentRepository.GetDepartmentId(viewModel);
            var shiftTypeId = viewModel.ShiftTypeId;

            var shiftId = _shiftRepository.GetShiftIdByDayTimeAndDepartmentId(datetime, departmentId, shiftTypeId);

            TempData["ID"] = shiftId;
            //TempData["DepartmentID"] = departmentId;
            return RedirectToAction("NewWork");
        }

        //get
        public ActionResult NewWork(int? id)
        {
            var newId = Convert.ToInt32(TempData["ID"]);
            if (newId != 0)
            {
                id = newId;
            }

            //var depapartmentID = Convert.ToInt32(TempData["DepartmentID"]);
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var shift = _shiftRepository.GetShift(id);

            if (shift == null)
            {
                return HttpNotFound();
            }

            var employees = _employeeRepository.GetEmployeesWhoNotWorking(shift);

            var workingEmployees = _employeeRepository.GetEmployeesWhoAreWorking(shift);

            var viewModel = new AssignShiftEmployeesViewModel
            {
                Shift = shift,
                Employees = employees,
                WorkingEmployees = workingEmployees,
                Department = shift.Department,
            };

            return View("NewWork", viewModel);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewWork(AssignShiftEmployeesViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.Employees = _employeeRepository.GetEmployeeIdEqualsToShiftId(viewModel);
                viewModel.Shift = _shiftRepository.GetShift();

                return View("NewWork", viewModel);
            }

            var employee = viewModel.EmployeeId;

            if (employee.Equals(0))
                return RedirectToAction("NewWork");

            var shift = viewModel.Shift.Id;

            var exist = _workRepository.GetWork(employee, shift);

            if (exist)
            {
                return RedirectToAction("NewWork");
            }

            var work = new Work(viewModel.EmployeeId, viewModel.Shift.Id);

            _context.Works.Add(work);
            _context.SaveChanges();

            var shiftId = viewModel.Shift.Id;
            TempData["ID"] = shiftId;
            return RedirectToAction("NewWork", shiftId);
        }

        //Get
        public ActionResult AddAWorkWeek()
        {
            var viewModel = new WorkDayViewModel
            {
                Departments = _context.Departments
            };

            return View(viewModel);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAWorkWeek(WorkDayViewModel viewModel)
        {
            var shift = _shiftRepository.GetShiftExist(viewModel);

            viewModel.Departments = _departmentRepository.GetDepartments();

            if (viewModel.DepartmentId == 0 || viewModel.NumberOfWorkDays == 0 || viewModel.NumbersOfShifts == 0 || viewModel.WorkDate == null)
            {
                var returnViewModel = new WorkDayViewModel
                {
                    Departments = viewModel.Departments,
                    WorkDate = viewModel.WorkDate,
                    NumberOfWorkDays = viewModel.NumberOfWorkDays,
                    NumbersOfShifts = viewModel.NumbersOfShifts
                };
                return RedirectToAction("AddAWorkWeek", returnViewModel);
            }

            if (!shift)
            {
                for (int j = 0; j < viewModel.NumberOfWorkDays; j++)
                {
                    for (byte i = 1; i <= viewModel.NumbersOfShifts; i++)
                    {
                        var newShift = new Shift(viewModel.WorkDate.AddDays(j), i, viewModel.DepartmentId);
                        _context.Shifts.Add(newShift);
                    }
                }
                _context.SaveChanges();

                viewModel.Department = _departmentRepository.GetDepartment(viewModel);
                var assigneViewModel = new AssignShiftEmployeesViewModel
                {
                    Department = viewModel.Department,
                    ShiftTypes = _shiftTypeRepository.GetShiftTypes()
                };

                return View("ChooseDateAssigment", assigneViewModel);
            }

            return View(viewModel);
        }

        public ActionResult Index(DateTime? dateTime, int? departmentId)
        {
            var departments = new GetWorkDayViewModel
            {
                Departments = _departmentRepository.GetDepartments()
            };
            if (dateTime == null && departmentId == null)
            {
                return View(departments);
            }
            else
            {
                var shiftIds = _shiftRepository.GetShiftTypesFromShifts(dateTime, departmentId);
                var shifttypes = _shiftTypeRepository.GetListFromShiftTypes(shiftIds);
                return RedirectToAction("GetWorkDay", new
                {
                    currentDatetime = dateTime,
                    currnetDpertmentId = departmentId,
                    departmentsToList = departments,
                    shiftToList = shifttypes
                });
            }
        }

        //Index (works)
        public ActionResult GetWorkDay(DateTime dateTime, int departmentId)
        {
            if (departmentId == 0)
            {
                return RedirectToAction("Index", null, null);
            }
            var shifts = _shiftRepository.GetShiftsWithWorkSelectedEmployeeWithDateTimeAndDepartment(dateTime, departmentId);
            if (shifts.Count == 0)
                return RedirectToAction("Index", null, null);

            var ShiftResults = _shiftRepository.GetFirstShift(shifts);

            var shiftIds = _shiftRepository.GetShiftTypesFromShifts(dateTime, departmentId);
            var shifttypes = _shiftTypeRepository.GetListFromShiftTypes(shiftIds);

            var employees = _employeeRepository.GetEmployeesWithDepartmentId(departmentId);

            var workDay = new GetWorkDayViewModel
            {
                Datetime = ShiftResults.DateTime,
                DepartmentId = departmentId,
                DepartemtName = ShiftResults.Department.Name,
                Shifts = shifts,
                Departments = _context.Departments.ToList(),
                ShiftTypes = shifttypes,
                Employees = employees,
            };

            return View("GetWorkDay", workDay);
        }

        //Create Get (works)
        public ActionResult Create()
        {
            var viewModel = new ShiftFormViewModel
            {
                Departments = _departmentRepository.GetDepartments(),
                ShiftTypes = _shiftTypeRepository.GetShiftTypes(),
            };

            return View(viewModel);
        }
    }
}