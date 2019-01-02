using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace OilTeamProject.Models.Products
{
    public class Image
    {
        public int ID { get; set; }
        public string Title { get; set; }
        [DisplayName("Upload File")]
        [NotMapped]
        public string ImagePath { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        //public virtual Category Category { get; set; }
        //public int CategoryID { get; set; }
        public Product Product { get; set; }
        [Required]
        public int ProductID { get; set; }




    }
}