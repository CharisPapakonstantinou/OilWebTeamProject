using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using OilTeamProject.Persistence;

namespace OilTeamProject.Models.Products
{
    public class PackageStock
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public int ID { get; set; }

        [NotMapped]
        private static int reservation;

        public const int MinimumStock = 1000;
        public const int MaximumStock = 5000;
        public const int ReorderingLevel = 4000;

        public int Quantity { get; set; }

        // FOREIGN KEYS
        public virtual Package Package { get; set; }
        [Index(IsUnique = true)]
        public int PackageID { get; set; }

        public virtual Sector Sector { get; set; }
        public int SectorID { get; set; }

        public int SendToProduction(int orderAmount, int? id)
        {
            if (orderAmount >= Quantity)
            {
                if (Quantity == 0)
                {
                    UpdateStock(id);

                    Quantity -= orderAmount;
                }
                else
                {
                    reservation = orderAmount - Quantity;
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
                int remainingAmount = Quantity - orderAmount;
                if (remainingAmount <= MinimumStock)
                {
                    UpdateStock(id);
                }
                return remainingAmount;
            }
        }

        public void UpdateStock(int? id)
        {
            Quantity = Quantity + ReorderingLevel;
        }
    }
}