using OilTeamProject.Models.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OilTeamProject.Models.Products
{
    public class RawMaterial
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        //FOREIGN OBJECTS
        public virtual ICollection<Category> Categories { get; set; }
        public ICollection<Supplier> Suppliers { get; set; }

        public ICollection<OrderToSupplier> OrderToSuppliers { get; set; }

    }
}