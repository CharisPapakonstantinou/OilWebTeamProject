using OilTeamProject.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OilTeamProject.Models.Employees
{
    public class Form
    {
        public int ID { get; set; }

        public string Theme { get; set; }

        public virtual ICollection<Performance> Performances  { get; set; }

        public virtual ICollection<Question> Questions { get; set; }


    }
}