using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Foolproof;
using OilTeamProject.Models.Customers;
using OilTeamProject.Models.Factories;
using OilTeamProject.Persistence;

namespace OilTeamProject.Models.Products
{
    public class ProductStock
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        private string productStockID;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ProductStockID
        {
            get
            {
                return ProductID.ToString() + BottlingID.ToString();
            }
            set
            {
                productStockID = value;
            }
        }

        public const int MinimumStock = 1000;
        public const int MaximumStock = 5000;
        // pote xreiazetai na ginei ananewsi paketwn
        public const int ReorderingLevel = 2500;


        [NotMapped]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Expiration Date")]
        public DateTime ExpirationDate { get; set; }

        [Range(0, MaximumStock)]
        [Display(Name = "Available Quantity")]
        public int AvailableQuantity { get; set; }

        [Range(0, MaximumStock)]
        [GreaterThan("AvailableQuantity")]
        [Display(Name = "Actual Quantity")]
        public int ActualQuantity { get; set; }

        // FOREIGN KEYS
        //[Required]
        // 1ProductStock is created by 1Bottling
        public virtual Sector Sector { get; set; }
        public int SectorID { get; set; }
        public virtual Bottling Bottling { get; set; }
        public int BottlingID { get; set; }

        public virtual Product Product { get; set; }
        public int ProductID { get; set; }

        public virtual ICollection<OrderProducts> OrderProducts { get; set; }

        public DateTime CalculateExpirationDate()
        {
            ExpirationDate = Bottling.BottlingDate.AddYears(1);

            return ExpirationDate;
        }

        public ProductStock SendToCustomerView(int id)
        {
            var stock = db.ProductStocks
                .OrderByDescending(p => p.AvailableQuantity)
                .First(p => p.ProductID == id);

            return stock;
        }

        public ProductStock SendProductStock(int orderAmount, string id)
        {
            ProductStock productStock = db.ProductStocks.Single(p => p.ProductStockID == id);


            if (orderAmount >= productStock.AvailableQuantity)
            {
                productStock.ActualQuantity -= productStock.AvailableQuantity;

                orderAmount = productStock.AvailableQuantity;
                //// the whole available quantity is being sent if the order amount is larger
                productStock.AvailableQuantity = 0;

                // update the stock because it is low
                //UpdateStock(productStock);

                // return this for warehouse View
                return productStock;
            }
            else
            {

                productStock.AvailableQuantity = productStock.AvailableQuantity - orderAmount;
                productStock.ActualQuantity = productStock.ActualQuantity - orderAmount;

                //if (productStock.AvailableQuantity <= MinimumStock)
                //{
                //    UpdateStock(productStock);
                //}
                return productStock;
            }
        }
    }
}