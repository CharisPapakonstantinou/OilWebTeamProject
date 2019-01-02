using OilTeamProject.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OilTeamProject.Models.Suppliers
{
    public class PackageOrderToSupplier
    {
        [Key]
        [Column(Order = 1)]
        public int OrderToSupplierId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int PackageId { get; set; }

        public OrderToSupplier OrderToSupplier { get; set; }

        public Package Package { get; set; }
    }
}