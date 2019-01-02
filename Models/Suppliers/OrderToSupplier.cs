using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OilTeamProject.Models.Products;

namespace OilTeamProject.Models.Suppliers
{

    [Table("OrderToSuppliers")]
    public class OrderToSupplier
    {
		
        public int Id { get; set; }

        [Display(Name = "Supplier Name")]
        public int SupplierId { get; set; }

        // Nav property
        [Display(Name = "Supplier Name")]
        public Supplier Supplier { get; set; }
        [Display(Name = "Order Datetime")]
        public DateTime DateTime { get; set; }


        public string Description { get; set; }
        
        public RawMaterial RawMaterial { get; set; }

        [Display (Name = "RawMaterial")]
        public int? RawMaterialId { get; set; }

        public ICollection<Package> Packages { get; set; }

        [Display(Name = "Package")]
        public int? PackageId { get; set; }

        public int Quantity { get; set; }

        [Display(Name = "Order has been received")]
        public bool OrderHasBeenReceived { get; set; }

    }
}