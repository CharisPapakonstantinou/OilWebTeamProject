using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OilTeamProject.Models.Employees
{
    public class Question
    {
        public int ID { get; set; }
        public string Text { get; set; } //question.Text

        [NotMapped]
        public Answer AnswersForAQuestion { get; set; }



        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Form> Forms { get; set; }

    }
}