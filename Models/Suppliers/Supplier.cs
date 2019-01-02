using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OilTeamProject.Models.Products;

namespace OilTeamProject.Models.Suppliers
{
    public enum SupplierType
    {
        Company,
        Private_Person
    }

    public enum SupplyingMaterial
    {
        Package,
        Olive
    }

    public class Supplier
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public SupplierType Type { get; set; }

        public SupplyingMaterial SupplyingMatetrial { get; set; }

        // ? Company Company { get; set;}

        public RawMaterial OliveSupplying { get; set; }
        //public int RawMaterialId { get; set; }

        public ICollection<Package> PackageSupplying { get; set; }
        //public int PackageId { get; set; }
         
        public ICollection<OrderToSupplier> OrderToSuppliers { get; set; }
    }
}