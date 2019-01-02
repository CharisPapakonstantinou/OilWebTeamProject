using OilTeamProject.Models.Employees;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace OilTeamProject.ViewModels
{
    public class AssignShiftEmployeesViewModel
    {
        public Shift Shift { get; set; }

        [DataType(DataType.Date)]
        [FutureDate]
        public DateTime Datetime { get; set; }

        public int EmployeeId { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public ICollection<Employee> WorkingEmployees { get; set; }

        public Department Department { get; set; }
        

        public IEnumerable<ShiftType> ShiftTypes { get; set; }
        public int ShiftTypeId { get; set; }
    }
}