using OilTeamProject.Models.Employees;
using System.Collections.Generic;

namespace OilTeamProject.ViewModels
{
    public class EmployeesAssignToShiftViewModel
    {
        public Shift Shift { get; set; }

        public IEnumerable<Employee> Employees { get; set; }
    }
}