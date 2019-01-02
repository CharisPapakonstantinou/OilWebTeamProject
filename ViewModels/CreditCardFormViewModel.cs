using OilTeamProject.Models.Customers;
using System;
using System.ComponentModel.DataAnnotations;

namespace OilTeamProject.ViewModels
{
    public class CreditCardFormViewModel
    {
        public int CustomerID { get; set; }

        [Required]
        [Display(Name = "Credit Card Number")]
        public int CreditCardNumber { get; set; }

        [Required]
        [Display(Name = "Credit Card Type")]
        public CardType Type { get; set; }

        [Required]
        [Display(Name = "Expire Month")]
        public string ExpMo { get; set; }

        [Required]
        [Display(Name = "Expire Year")]
        public string ExpYr { get; set; }

        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", ExpMo, ExpYr));
        }

    }
}