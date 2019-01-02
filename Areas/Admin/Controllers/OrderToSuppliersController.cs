using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OilTeamProject.Models.Suppliers;
using OilTeamProject.Persistence;
using OilTeamProject.ViewModels;

namespace OilTeamProject.Areas.Admin.Controllers
{
    public class OrderToSuppliersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/OrderToSuppliers
        public ActionResult Index()
        {
            var orderToSuppliers = db.OrderToSuppliers.Include(o => o.RawMaterial).Include(o => o.Supplier);

            return View(orderToSuppliers.ToList());
        }

        // GET: Admin/OrderToSuppliers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.ShowPackageName = false;

            var orderToSupplier = db.OrderToSuppliers
                        .Include(o => o.Packages)
                        .Include(o => o.Supplier)
                        .Include(o => o.RawMaterial)
                        .Single(o => o.Id == id);

            PackageOrderToSupplier packageOrderSupplier = db.PackageOrderToSuppliers.SingleOrDefault(p => p.OrderToSupplierId == orderToSupplier.Id);
            
            if (packageOrderSupplier != null)
            {
                ViewBag.PackageName = db.Packages.SingleOrDefault(o => o.ID == packageOrderSupplier.PackageId).Name;
                ViewBag.ShowPackageName = true;

            }


            if (orderToSupplier == null)
            {
                return HttpNotFound();
            }
            return View(orderToSupplier);
        }

        // GET: Admin/OrderToSuppliers/Create
        public ActionResult Create()
        {
            ViewBag.RawMaterialId = new SelectList(db.RawMaterials, "ID", "Name");
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name");
            ViewBag.PackageId = new SelectList(db.Packages, "Id", "Name");

            return View();
        }

        // POST: Admin/OrderToSuppliers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SupplierId,Description,RawMaterialId,Quantity")] OrderToSupplier orderToSupplier, int? PackageId)
        {
            orderToSupplier.DateTime = DateTime.Now;

            if (PackageId != null)
            {
                var packageOrderToSupplier = new PackageOrderToSupplier()
                {
                    OrderToSupplierId = orderToSupplier.Id,
                    PackageId = (int)PackageId
                };
                db.PackageOrderToSuppliers.Add(packageOrderToSupplier);
            }

            if (ModelState.IsValid)
            {
                db.OrderToSuppliers.Add(orderToSupplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RawMaterialId = new SelectList(db.RawMaterials, "ID", "Name", orderToSupplier.RawMaterialId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", orderToSupplier.SupplierId);
            ViewBag.PackageId = new SelectList(db.Packages, "Id", "Name");

            return View(orderToSupplier);
        }

        // GET: Admin/OrderToSuppliers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderToSupplier orderToSupplier = db.OrderToSuppliers
                .Include(o => o.RawMaterial)
                .Include(p => p.Packages)
                .SingleOrDefault(o => o.Id == id);

            Supplier supplier = db.Suppliers.Find(orderToSupplier.SupplierId);

            ViewBag.PackageMaterial = false;

            if (supplier.SupplyingMatetrial == SupplyingMaterial.Package)
            {
                ViewBag.PackageMaterial = true;
            }


            if (orderToSupplier == null)
            {
                return HttpNotFound();
            }
            ViewBag.RawMaterialId = new SelectList(db.RawMaterials, "ID", "Name", orderToSupplier.RawMaterialId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", orderToSupplier.SupplierId);
            ViewBag.PackageId = new SelectList(db.Packages, "Id", "Name");

            return View(orderToSupplier);
        }

        // POST: Admin/OrderToSuppliers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderToSupplier orderToSupplier, int? PackageId)
        {

            var packageOrderToSupplier = db.PackageOrderToSuppliers.SingleOrDefault(o => o.OrderToSupplierId == orderToSupplier.Id);

            if (packageOrderToSupplier != null)
            {
                db.PackageOrderToSuppliers.Remove(packageOrderToSupplier);
                db.SaveChanges();

                var newPackageOrderToSupplier = new PackageOrderToSupplier
                {
                    OrderToSupplierId = orderToSupplier.Id,
                    PackageId = (int) PackageId
                };

                db.PackageOrderToSuppliers.Add(newPackageOrderToSupplier);

            }


            var orderToSupplierFromDb = db.OrderToSuppliers.Find(orderToSupplier.Id);

            orderToSupplierFromDb.SupplierId = orderToSupplier.SupplierId;
            orderToSupplierFromDb.PackageId = orderToSupplier.PackageId;
            orderToSupplierFromDb.RawMaterialId = orderToSupplier.RawMaterialId;
            orderToSupplierFromDb.OrderHasBeenReceived = orderToSupplier.OrderHasBeenReceived;
            orderToSupplierFromDb.Quantity = orderToSupplier.Quantity;
            orderToSupplierFromDb.Description = orderToSupplier.Description;



            if (ModelState.IsValid)
            {

                //db.Entry(orderToSupplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RawMaterialId = new SelectList(db.RawMaterials, "ID", "Name", orderToSupplier.RawMaterialId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", orderToSupplier.SupplierId);
            ViewBag.PackageId = new SelectList(db.Packages, "Id", "Name");

            return View(orderToSupplier);
        }

        // GET: Admin/OrderToSuppliers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderToSupplier orderToSupplier = db.OrderToSuppliers.Find(id);
            if (orderToSupplier == null)
            {
                return HttpNotFound();
            }
            return View(orderToSupplier);
        }

        // POST: Admin/OrderToSuppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderToSupplier orderToSupplier = db.OrderToSuppliers.Find(id);
            db.OrderToSuppliers.Remove(orderToSupplier);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
