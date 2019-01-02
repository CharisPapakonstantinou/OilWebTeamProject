using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OilTeamProject.ViewModels
{
    public class CartViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total { get { return Quantity * Price; } }
        public double GrandTotal { get; set; }
        public string Image { get; set; }
    }
}