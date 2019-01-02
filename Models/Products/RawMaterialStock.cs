using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using OilTeamProject.Persistence;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace OilTeamProject.Models.Products
{
    public class RawMaterialStock
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public int ID { get; set; }

        [NotMapped]
        private static int reservation;

        public const int MinimumStock = 1000;
        public const int MaximumStock = 5000;
        public const int ReorderingLevel = 4000;

        [Display(Name = "Available Quantity")]
        public int Quantity { get; set; }

        // FOREIGN KEYS
        public virtual RawMaterial RawMaterial { get; set; }
        [Index(IsUnique = true)]
        public int RawMaterialID { get; set; }

        public virtual Sector Sector { get; set; }
        public int SectorID { get; set; }

        public int SendToProduction(int productionOrderAmount, int? id)
        {

            if (productionOrderAmount >= Quantity)
            {
                if (Quantity == 0)
                {
                    UpdateStock(id);

                    Quantity -= productionOrderAmount;
                }
                else
                {
                    reservation = productionOrderAmount - Quantity;
                    Quantity = 0;
                    if (reservation > 0)
                    {
                        UpdateStock(id);
                        Quantity -= reservation;
                    }
                }
                return Quantity;
            }
            else
            {
                int i = Quantity - productionOrderAmount;
                if (i <= MinimumStock)
                {
                    UpdateStock(id);
                }
                return i;
            }
        }

        public void UpdateStock(int? id)
        {
            Quantity = Quantity + ReorderingLevel;
        }
    }
}