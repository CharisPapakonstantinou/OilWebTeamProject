using OilTeamProject.Controllers;
using OilTeamProject.Models;
using OilTeamProject.Models.Customers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace OilTeamProject.ViewModels
{

    public class OrderDetailsFormViewModel
    {
        public List<string> ProductStockIds { get; set; }
        public List<string> ProductNames { get; set; }
        public List<double> Prices { get; set; }
        public List<int> Quantities { get; set; }
        public List<bool> AddToCarts { get; set; }

        public PaymentType PaymentType { get; set; }


        public List<double> SubTotal { get; set; }

        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public double TotalCost { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int Count { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(30, ErrorMessage = "'First Name' must be between 3-30 characters long", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(30, ErrorMessage = "'Last Name' must be between 3-30 characters long", MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "'Country' must be between 3-30 characters long", MinimumLength = 3)]
        public string Country { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "'City' must be between 3-30 characters long", MinimumLength = 3)]
        public string City { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "'Address' must be between 3-30 characters long", MinimumLength = 3)]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        [DataType(DataType.PostalCode)]
        public int PostalCode { get; set; }

        [Required]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }


        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool RedeemBonus { get; set; }
        public bool IfBonusIsApplicable { get; set; }
        public bool CartIsEmpty { get; set; }
        public double NewTotalCost { get; set; }


        public OrderDetailsFormViewModel()
        {
            ProductStockIds = new List<string>();
            ProductNames = new List<string>();
            Prices = new List<double>();
            Quantities = new List<int>();
            AddToCarts = new List<bool>();
            SubTotal = new List<double>();

        }

        public string Action
        {
            get
            {

                Expression<Func<OrdersController, ActionResult>> payOff = (c => c.PayOff(this));

                // Expression : ekfras pou adiprosopeuete apo delegate pou adiprosopeuei methodous 
                Expression<Func<OrdersController, ActionResult>> confirm = (c => c.ConfirmOrder(this));


                // expresion x +y = 2
                var action = (OrderId == 0) ? confirm : payOff;
                var actionName = (action.Body as MethodCallExpression).Method.Name;
                // pare to body pernei to c  => c.Body
                return actionName;
                // by defailt int is 0
            }

        }
    }
}