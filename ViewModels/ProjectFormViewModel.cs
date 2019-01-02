using OilTeamProject.Models.Employees;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OilTeamProject.ViewModels
{
    public class ProjectFormViewModel
    {
        public int Id { get; set; }


        [DataType(DataType.Date)]
        [FutureDate]
        public DateTime DueDate { get; set; }

        [DataType(DataType.Date)]
        [FutureDate]
        public DateTime? StartingDate { get; set; }


        [DataType(DataType.Date)]
        [FutureDate]
        public DateTime? EndDate { get; set; }

        public string Description { get; set; }

        public int DepartmentId { get; set; }
        public IEnumerable<Department> Departments { get; set; }


    }
}