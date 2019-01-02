using OilTeamProject.ViewModels;
using System;
using System.Collections.Generic;

namespace OilTeamProject.Models.Employees
{
    public class Project
    {
        public int Id { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? StartingDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<Assignment> Assignments { get; set; }

        public Project()
        {}

        public Project(ProjectFormViewModel viewModel)
        {
            DueDate = viewModel.DueDate;
            StartingDate = DateTime.Now;
            Description = viewModel.Description.ToString();
            DepartmentId = viewModel.DepartmentId;
            EndDate = viewModel.EndDate;
        }
    }
}