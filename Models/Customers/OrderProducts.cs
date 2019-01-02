using OilTeamProject.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OilTeamProject.Models.Customers
{
    public class OrderProducts
    {
        public int OrderProductsID { get; set; }
        public int OrderID { get; set; }
        public string ProductStockID { get; set; }

        [Range(0, 999999)]
        public int Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual ProductStock ProductStock { get; set; }
    }
}