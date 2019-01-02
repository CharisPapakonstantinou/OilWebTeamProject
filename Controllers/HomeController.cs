using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OilTeamProject.Models.Products;
using OilTeamProject.Persistence;

namespace OilTeamProject.Controllers
{

        public class HomeController : Controller
        {
            private ApplicationDbContext db = new ApplicationDbContext();
            public ActionResult Index()
            {
                var categories = db.Categories.ToList();
                var products = db.Products.ToList().Where(p => p.Featured == true);

                ViewBag.Products = products;
                ViewBag.Categories = categories;
                return View();
            }

            public ActionResult About()
            {
                ViewBag.Message = "Your application description page.";

                return View();
            }

            public ActionResult Contact()
            {
                ViewBag.Message = "Your contact page.";

                return View();
            }

            // GET: /home/Product/slug
            // SOS Check if slug is unique on create
            public ActionResult SingleProduct(string slug)
            {
                if (slug == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Product product = db.Products.Where(p => p.Slug == slug).SingleOrDefault();
                Category category = db.Categories.Find(product.CategoryID);
                Package package = db.Packages.Find(product.PackageID);
                var images = db.Images.ToList().Where(p => p.ProductID == product.ID);
                ViewBag.Category = category;
                ViewBag.Package = package;
                ViewBag.Images = images;
                if (product == null)
                {
                    return HttpNotFound();
                }
                if (product.Discount != 0)
                {
                    return View("ProductWithDiscount", product);
                }
                return View("Product", product);
            }

            // GET: /home/category/slug
            // SOS Check if slug is unique on create
            public ActionResult SingleCategory(string slug)
            {
                if (slug == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Category category = db.Categories.Where(c => c.Slug == slug).SingleOrDefault();
                ViewBag.Products = db.Products.ToList().Where(p => p.CategoryID == category.CategoryID);
                if (category == null)
                {
                    return HttpNotFound();
                }
                return View("Category", category);
            }
        }
    }
