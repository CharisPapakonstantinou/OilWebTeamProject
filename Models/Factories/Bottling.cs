using OilTeamProject.Models.Products;
using System;
using System.Collections.Generic;

namespace OilTeamProject.Models.Factories
{
    public class Bottling
    {
        public int BottlingID { get; set; }
        public DateTime BottlingDate { get; set; }
        public string ProductCode { get; set; }
        public int BottlingLotNumber { get; set; }
        public Tanks Tank { get; set; }
        public double Quantity { get; set; }

        //public Factory Factory { get; set; }

        public int? FactoryID { get; set; }
        public virtual Factory Factory { get; set; }

        public ICollection<Factory> Factories { get; set; }

        public ICollection<ProductStock> ProductStocks { get; set; }

    }
}