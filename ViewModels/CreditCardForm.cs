using System;
using System.ComponentModel.DataAnnotations;
using OilTeamProject.Models.Customers;

namespace OilTeamProject.ViewModels
{
    public class CreditCardForm
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