using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using OilTeamProject.Persistence;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace OilTeamProject.Models.Products
{
    public class Sector
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public int ID { get; set; }

        [Display(Name = "Sector Name")]
        public string Name { get; set; }

        [Display(Name = "Stored Type")]
        public string StoredType { get; set; }

        [NotMapped]
        public int PackageStockTotal
        {
            get
            {
                return db.PackageStocks.Include(p => p.Sector).Where(p => p.Sector.ID == ID).Count();
            }
        }

        [NotMapped]
        public int ProductStockTotal
        {
            get
            {
                return db.ProductStocks.Include(p => p.Sector).Where(p => p.Sector.ID == ID).Count();
            }
        }

        // FOREIGN KEYS
        public int WarehouseID { get; set; }
        public virtual Warehouse Warehouse { get; set; }


        // what could this be?
        //public void UpdateSector()
        //{

        //}

        //public void TransferPackages(int howMany)
        //{
        //    // pws sindeoume to type of package, packageStockTotal, packageStock gia na einai ola up to date otan
        //    // afairethei kati?
        //    Sector packageSector = CreatePackageSector();
        //    //packageSector.PackageStockTotal = PackageStock.Quantity - howMany;
        //}

        //public void StoreProducts()
        //{

        //}

        //public void StorePackages()
        //{

        //}


    }
}