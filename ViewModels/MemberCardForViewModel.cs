using OilTeamProject.Models.Customers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OilTeamProject.ViewModels
{

    //public enum MembershipType
    //{
    //    Basic,
    //    Silver,
    //    Gold
    //}

    public class MemberCardForViewModel
    {
        [Required]
        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }

        [Required]
        public MembershipType Type { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Display(Name = "MemberCard Code")]
        //public Guid MemberCardCode { get; set; }

        [Required]
        public int Credits { get; set; }

        public IEnumerable<MembershipType> Types { get; set; }

        public DateTime GetDate()
        {
            return DateTime.Parse(string.Format("{0}", CreationDate));
        }
    }

}