using OilTeamProject.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OilTeamProject.ViewModels
{
    public class ProjectDetailsViewModel
    {
        public Project Project { get; set; }
        public IEnumerable<Assignment> ProjectsEmployees { get; set; }
    }
}