using OilTeamProject.Models.Customers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OilTeamProject.ViewModels
{
    public class MemberCardFormViewModel
    {
        [Required]
        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }

        [Required]
        public MembershipType Type { get; set; }

        [Required]
        public int Credits { get; set; }

        public IEnumerable<MembershipType> Types { get; set; }

        public DateTime GetDate()
        {
            return DateTime.Parse(string.Format("{0}", CreationDate));
        }
    }

}