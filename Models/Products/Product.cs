using OilTeamProject.Persistence;
using OilTeamProject.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilTeamProject.Models.Products
{
    public class Product
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public int ID { get; set; }
        [Required(ErrorMessage = "Please enter a Product Name")]
        [StringLength(255)]
        public string Name { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        [UniqueProductSlug]
        public string Slug { get; set; }
        public bool Featured { get; set; }
        public int Discount { get; set; }

        [NotMapped]
        [DataType(DataType.Currency)]
        public double Price
        {

            get { return GetProductPrice(Category, Package); }
        }

        [NotMapped]
        public double DiscountedPrice
        {
            get { return DiscountedProductPrice(); }
        }

        public string BarCode { get; set; }

        //public string ProductNumber { get; set; }
        [DisplayName("Primary Image")]
        public string Thumbnail { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        [NotMapped]
        public List<HttpPostedFileBase> SecondaryImages { get; set; }

        [DisplayName("Secondary Images")]
        public virtual IEnumerable<Image> Images { get; set; }


        //public int? Stock { get; set; }

        //FOREIGN OBJECTS
        //public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ProductStock> ProductStocks { get; set; }


        [DisplayName("Package")]
        public virtual Package Package { get; set; }
        public virtual Category Category { get; set; }

        //FOREIGN KEYS
        [ForeignKey("Package")]
        [Display(Name = "Package")]
        public int PackageID { get; set; }
        public int CategoryID { get; set; }

        public double GetProductPrice(Category category, Package package)
        {
            category = db.Categories.Find(CategoryID);
            package = db.Packages.Find(PackageID);
            var cost = category.Price + package.Price;
            var price = cost + cost * 0.8;
            price = price + price * 0.24;
            price = Math.Round(price, 2);
            return price;
        }

        public double MinimumPrice()
        {
            var category = db.Categories.Find(CategoryID);
            var package = db.Packages.Find(PackageID);
            var cost = package.Price + category.Price;
            var minimumPrice = cost + cost * 0.2;
            minimumPrice = minimumPrice + minimumPrice * 0.24;
            minimumPrice = Math.Round(minimumPrice, 2);
            return minimumPrice;
        }

        public double DiscountedProductPrice()
        {
            double discountedPrice = Price * Discount / 100;
            discountedPrice = Price - discountedPrice;
            discountedPrice = Math.Round(discountedPrice, 2);
            if (Discount == 0)
            {
                discountedPrice = 0;
            }
            else if (discountedPrice < MinimumPrice())
            {
                discountedPrice = MinimumPrice();
            }

            return discountedPrice;
        }

        public bool CheckIfSlugExists(string slug, int? id)
        {
            var product = db.Products
                .Where(p => p.Slug == slug)
                .Where(p => p.ID != id)
                .SingleOrDefault();

            if (product != null)
                return true;

            return false;
        }

        public bool CheckIfProductExists(int? id)
        {
            if (id == null)
                return false;

            var product = db.Products.Find(id);

            if (product != null)
                return true;

            return false;
        }


    }
}