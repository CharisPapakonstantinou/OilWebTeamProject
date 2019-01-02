using OilTeamProject.Models.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OilTeamProject.ViewModels
{
    public class OrderFormViewModel
    {

        public int OrderId { get; set; }



        public List<string> ProductStockIds { get; set; }
        public List<string> ProductNames { get; set; }
        public List<double> Prices { get; set; }
        public List<int> Quantities { get; set; }
        public List<bool> AddToCarts { get; set; }
        public List<string> Descriptions { get; set; }
        public PaymentType PaymentType { get; set; }
        public int CustomerId { get; set; }
        public int Count { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalCost { get; set; }
        public string OrderNumber { get; set; }


        public bool RedeemBonus { get; set; }

        public bool IfBonusIsApplicable { get; set; }

        public OrderFormViewModel()
        {
            ProductStockIds = new List<string>();
            ProductNames = new List<string>();
            Prices = new List<double>();
            Quantities = new List<int>();
            AddToCarts = new List<bool>();
            Descriptions = new List<string>();
        }
    }
}