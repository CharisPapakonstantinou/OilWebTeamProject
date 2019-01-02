using OilTeamProject.Persistence;
using OilTeamProject.Validation;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilTeamProject.Models.Products
{


    public class Category
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        [Display(Name = "Κατηγορια")]
        public string Name { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        [UniqueCategorySlug]
        public string Slug { get; set; }

        public double Price { get; set; }

        //FOREIGN OBJECTS
        public virtual RawMaterial RawMaterial { get; set; }

        public string Thumbnail { get; set; }
        [DisplayName("Upload File")]
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        //FOREIGN KEYS
        public int RawMaterialID { get; set; }

        public bool CheckIfSlugExists(string slug)
        {
            var category = db.Categories
                .Where(c => c.Slug == slug)
                .SingleOrDefault();

            if (category != null)
                return true;

            return false;
        }
    }
}