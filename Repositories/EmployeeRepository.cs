using OilTeamProject.Models.Employees;
using OilTeamProject.Persistence;
using OilTeamProject.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OilTeamProject.Repositories
{
    public class EmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Employee> AllEmployees()
        {
            return _context.Employees
                .Include(e => e.PersonalDetails)
                .Include(e => e.ContactDetails)
                .Include(e => e.Department)
                .Include(e => e.Role);
        }

        public Employee EmployeeProjects(int? Id)
        {
            return _context.Employees
                .Include(e => e.Assignments)
                .Include(e => e.Role)
                .Include(e => e.Department)
                .Include(e => e.ContactDetails)
                .Include(e => e.PersonalDetails)
                .Single(e => e.Id == Id);
        }

        public Employee EmployeeUpcomingLeaves(int? Id)
        {
            return _context.Employees
                .Include(e => e.Requests)
                .Include(e => e.Role)
                .Include(e => e.Department)
                .Include(e => e.ContactDetails)
                .Include(e => e.PersonalDetails)
                .Single(e => e.Id == Id);
        }

        public void Add(Employee employee)
        {
            _context.Employees.Add(employee);
        }

        public Employee Find(int? id)
        {
            return _context.Employees.Find(id);
        }

        public List<Employee> GetAllEmployees()
        {
            return _context.Employees
                .Include(e => e.PersonalDetails)
                .Include(e => e.ContactDetails)
                .Include(e => e.Department)
                .Include(e => e.Role)
                .ToList();
        }

        public Employee EmployeeUpcomingShifts(int? Id)
        {
            return _context.Employees
                 .Include(e => e.Works)
                 .Include(e => e.Works.Select(w => w.Shift.ShiftType))
                 .Include(e => e.Role)
                 .Include(e => e.Department)
                 .Include(e => e.ContactDetails)
                 .Include(e => e.PersonalDetails)
                 .Single(e => e.Id == Id);
        }

        public IQueryable<Employee> GetEmployeesWithDepartmentId(int departmentId)
        {
            return _context.Employees
                 .Where(e => e.DepartmentId == departmentId);
        }

        public ICollection<Employee> GetEmployeeIdEqualsToShiftId(AssignShiftEmployeesViewModel viewModel)
        {
            return _context.Employees
                    .Where(e => e.DepartmentId == viewModel.Shift.DepartmentId)
                    .ToList();
        }

        public ICollection<Employee> GetEmployeesWhoNotWorking(Shift shift)
        {
            return _context.Employees
                .Where(e => e.DepartmentId == shift.Department.Id &&
                !e.Works.Any(w => w.ShiftId == shift.Id))
                .ToList();
        }

        public ICollection<Employee> GetEmployeesWhoAreWorking(Shift shift)
        {
            return _context.Employees
                .Where(e => e.DepartmentId == shift.Department.Id &&
                e.Works.Any(w => w.ShiftId == shift.Id))
                .ToList();
        }
    }
}