using OilTeamProject.Models.Employees;
using OilTeamProject.Persistence;
using OilTeamProject.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace OilTeamProject.Repositories
{
    public class DepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Department GetDepartment(WorkDayViewModel viewModel)
        {
            return _context.Departments.Where(d => d.Id == viewModel.DepartmentId).Single();
        }

        public int GetDepartmentId(AssignShiftEmployeesViewModel viewModel)
        {
            return _context.Departments.Where(d => d.Id == viewModel.Department.Id).Single().Id;
        }

        public IEnumerable<Department> GetDepartments()
        {
            return _context.Departments.ToList();
        }
    }
}