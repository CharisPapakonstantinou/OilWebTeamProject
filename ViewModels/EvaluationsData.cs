using OilTeamProject.Models;
using OilTeamProject.Models.Employees;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Mvc;

namespace OilTeamProject.ViewModels
{
    public class EvaluationsData
    {
        public IEnumerable<Evaluation> Evaluations { get; set; }
        public IEnumerable<Performance> Performances { get; set; }
        public List<Question> Questions { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public Form Form { get; set; }
        public bool CreateNewQuestion { get; set; }
        public Question Question { get; set; }
        public bool CheckedQuestion { get; set; }
        public List<string> CheckedQuestions { get; set; }
        public IList<SelectListItem> SelectedQuestions { get; set; }


        public EvaluationsData()
        {
            Evaluations = new Collection<Evaluation>();
            SelectedQuestions = new List<SelectListItem>();
            Questions = new List<Question>();
            CheckedQuestions = new List<string>();
        }




    }
}