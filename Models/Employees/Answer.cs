using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OilTeamProject.Models.Employees;

namespace OilTeamProject.Models.Employees
{
    
    public class Answer
    {
        public enum Rating
        {
            Poor = 1,
            Fair,
            Satisfactory,
            Good,
            Excellent
        }

        [Key]
        [ForeignKey("Question")]
        [Column(Order = 1)]
        public int? QuestionID { get; set; }

       
        [Key]
        [ForeignKey("Performance")]
        [Column(Order = 2)]
        public int PerformanceID { get; set; }

        public string Text { get; set; }

        public Rating? QuestionAnswer { get; set; }

        public virtual Question Question { get; set; }

       
        public virtual Performance Performance { get; set; }


    }
}