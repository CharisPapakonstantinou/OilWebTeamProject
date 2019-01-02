using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OilTeamProject.Models;

namespace OilTeamProject.Models.Employees
{
    public class Performance
    {
        public int ID { get; set; }

        public int EmployeeID { get; set; }     
        public int EvaluationID { get; set; }
        public int FormID { get; set; }
        public decimal OveralRating { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateEvaluated { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Evaluation Evaluation { get; set; }
      
        public virtual  Form Form{ get; set; }
        public virtual ICollection<Answer> Answers { get; set; }


    }
}