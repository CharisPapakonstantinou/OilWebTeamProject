
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OilTeamProject.Models.Employees
{
    public class Evaluation
    {
        

        public int ID { get; set; }

       

        [DataType(DataType.Date)]
        public DateTime StartEvaluationDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndEvaluationDate { get; set; }

        public string EvaluationDate
        {
            get
            {
                return "Start  " + StartEvaluationDate.ToShortDateString() + " ,  End  " + EndEvaluationDate.ToShortDateString();
            }
        }


        //connection 1-N between Employee and Evaluation
        public virtual ICollection<Performance> Performances { get; set; }

        

    }
}