using OilTeamProject.Models.Suppliers;
using OilTeamProject.Persistence;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace OilTeamProject.Models.Products
{
    public enum Size
    {
        Small = 1,
        Medium = 2,
        Large = 3
    }

    public enum MaterialType
    {
        Glass = 1,
        Plastic = 2
    }

    public class Package
    {
        public int ID { get; set; }
        public string Name { get; set; }


        [NotMapped]
        public double Volume
        {
            get
            {
                if (Size == Size.Small)
                    return 20;
                else if (Size == Size.Medium)
                    return 60;
                else
                    return 100;
            }
        }

        //[NotMapped]
        //public double Volume { get; set; }

        public MaterialType Material { get; set; }
        public Size Size { get; set; }
        public double Price { get; set; }

        public double Height { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }

        public ICollection<OrderToSupplier> OrderToSuppliers { get; set; }
        public ICollection<Supplier> Suppliers { get; set; }

        public Package()
        { }

        public static void CreatePackageType(Package package)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            db.Packages.Add(package);
            db.SaveChanges();
        }

        public static void EditPackageType(Package package)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            //var packageToUpdate = db.Packages.Find(package.ID);
            db.Entry(package).State = EntityState.Modified;
            db.SaveChanges();
        }

        public static void DeletePackageType(int packageID)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            Package package = db.Packages.Find(packageID);
            db.Packages.Remove(package);
            db.SaveChanges();
        }
        //public enum Size
        //{
        //    Small = 1,
        //    Medium = 2,
        //    Large = 3
        //}

        //public enum MaterialType
        //{
        //    Glass = 1,
        //    Plastic = 2
        //}

        //public class Package
        //{
        //    public int ID { get; set; }
        //    public string Name { get; set; }

        //    [NotMapped]
        //    public double Volume
        //    {
        //        get;
        //        set;
        //    }

        //    public MaterialType Material { get; set; }
        //    public Size Size { get; set; }
        //    public double Price { get; set; }

        //    public double Height { get; set; }
        //    public double Length { get; set; }
        //    public double Width { get; set; }

        //    //public void CreateSmallGlassPackage()
        //    //{
        //    //    new Package
        //    //    {
        //    //        Name = "Small Glass",
        //    //        Height = 218,
        //    //        Length = 46,
        //    //        Width = 46,
        //    //        Material = MaterialType.Glass,
        //    //        Price = 1,
        //    //        Size = Size.Small
        //    //    };
        //    //}

        //    public static Package CreateSmallGlassPackage()
        //    {
        //        return new Package
        //        {
        //            Name = "Small Glass",
        //            Height = 218,
        //            Length = 46,
        //            Width = 46,
        //            Material = MaterialType.Glass,
        //            Price = 1,
        //            Size = Size.Small
        //        };
        //    }

        //    public static Package CreateSmallPlasticPackage()
        //    {
        //        return new Package
        //        {
        //            Name = "Small Plastic",
        //            Height = 218,
        //            Length = 46,
        //            Width = 46,
        //            Material = MaterialType.Plastic,
        //            Price = 1,
        //            Size = Size.Small
        //        };
        //    }
    }

}