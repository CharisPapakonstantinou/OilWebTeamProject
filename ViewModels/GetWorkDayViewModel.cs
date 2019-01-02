using OilTeamProject.Models.Employees;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OilTeamProject.ViewModels
{
    public class GetWorkDayViewModel
    {
        [DataType(DataType.Date)]
        [FutureDate]
        public DateTime Datetime { get; set; }

        public int DepartmentId { get; set; }
        public IEnumerable<Department> Departments { get; set; }
        public string DepartemtName { get; set; }

        public IEnumerable<Shift> Shifts { get; set; }
        //public ILookup<int,Work> Works { get; set; }

        public int ShiftTypeId { get; set; }
        public IEnumerable<ShiftType> ShiftTypes { get; set; }

        public int EmployeeId { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }
}