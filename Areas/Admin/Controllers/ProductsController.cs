using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OilTeamProject.Models.Products;
using OilTeamProject.Persistence;

namespace OilTeamProject.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Package);
            return View(products.ToList());
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            var images = db.Images.ToList().Where(p => p.ProductID == product.ID);
            ViewBag.Images = images;
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            ViewBag.PackageID = new SelectList(db.Packages, "ID", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, List<HttpPostedFileBase> PicFile)
        {

            product.Thumbnail = Path.GetFileName(product.ImageFile.FileName);
            string fileName = Path.Combine(Server.MapPath("~/Image/"), product.Thumbnail);
            product.ImageFile.SaveAs(fileName);

            product.SecondaryImages = PicFile;
            var picsToDB = new List<Image>();
            foreach (var secondaryPic in product.SecondaryImages)
            {
                string secFileName = Path.GetFileName(secondaryPic.FileName);

                string otherFileName = Path.Combine(Server.MapPath("~/Image/"), secFileName);
                secondaryPic.SaveAs(otherFileName);

                var image = new Image();
                image.Title = secFileName;
                image.ProductID = product.ID;
                picsToDB.Add(image);
            }


            if (ModelState.IsValid)
            {
                foreach (var pic in picsToDB)
                {
                    db.Images.Add(pic);
                }
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", product.CategoryID);
            ViewBag.PackageID = new SelectList(db.Packages, "ID", "Name", product.PackageID);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var images = db.Images.ToList().Where(p => p.ProductID == product.ID);
            product.Images = images;
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", product.CategoryID);
            ViewBag.PackageID = new SelectList(db.Packages, "ID", "Name", product.PackageID);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, List<HttpPostedFileBase> PicFile)
        {
            // peritos kwdikas?
            if (product.ImageFile != null)
            {
                product.Thumbnail = Path.GetFileName(product.ImageFile.FileName);
                string fileName = Path.Combine(Server.MapPath("~/Image/"), product.Thumbnail);
                product.ImageFile.SaveAs(fileName);
            }
            /////////////////////////////////////////

            //var picsToDB = new List<Image>();
            //if (PicFile != null)
            //{
            //    product.SecondaryImages = PicFile;
            //    foreach (var secondaryPic in product.SecondaryImages)
            //    {
            //        string secFileName = Path.GetFileName(secondaryPic.FileName);

            //        string otherFileName = Path.Combine(Server.MapPath("~/Image/"), secFileName);
            //        secondaryPic.SaveAs(otherFileName);

            //        var image = new Image();
            //        image.Title = secFileName;
            //        image.ProductID = product.ID;
            //        picsToDB.Add(image);
            //    }
            //}


            if (ModelState.IsValid)
            {
                //foreach (var pic in picsToDB)
                //{
                //    db.Images.Add(pic);
                //}

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", product.CategoryID);
            ViewBag.PackageID = new SelectList(db.Packages, "ID", "Name", product.PackageID);
            return View(product);
        }


        // POST: Admin/Products/DeleteProduct/5
        public void DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //GET Product/Submit Discount
        public ActionResult SubmitDiscount(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = db.Products
                   .Include(p => p.Category)
                   .Include(p => p.Package)
                   .Where(p => p.ID == id)
                   .SingleOrDefault();

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        //Post/Submit Discount
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitDiscount(int? id, int discount)
        {
            var product = db.Products.Find(id);
            product.Discount = discount;

            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public void DeleteGalleryImage(int id)
        {
            var image = db.Images.Where(i => i.ID == id);
            db.Images.RemoveRange(image);
            db.SaveChanges();
        }
    }
}