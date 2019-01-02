using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OilTeamProject.Models.Products;
using OilTeamProject.Persistence;

namespace OilTeamProject.Areas.Admin.Controllers
{
    public class PackageStocksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PackageStocks
        public ActionResult Index()
        {
            var packageStocks = db.PackageStocks.Include(p => p.Package).Include(p => p.Sector);
            return View(packageStocks.ToList());
        }

        // GET: PackageStocks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackageStock packageStock = db.PackageStocks.Find(id);
            if (packageStock == null)
            {
                return HttpNotFound();
            }
            return View(packageStock);
        }

        // GET: PackageStocks/Create
        public ActionResult Create()
        {
            ViewBag.PackageID = new SelectList(db.Packages, "ID", "Name");
            ViewBag.SectorID = new SelectList(db.Sectors, "ID", "Name");
            return View();
        }

        // POST: PackageStocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Quantity,PackageID,SectorID")] PackageStock packageStock)
        {
            if (ModelState.IsValid)
            {
                db.PackageStocks.Add(packageStock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PackageID = new SelectList(db.Packages, "ID", "Name", packageStock.PackageID);
            ViewBag.SectorID = new SelectList(db.Sectors, "ID", "Name", packageStock.SectorID);
            return View(packageStock);
        }

        // GET: PackageStocks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackageStock packageStock = db.PackageStocks.Find(id);
            if (packageStock == null)
            {
                return HttpNotFound();
            }
            ViewBag.PackageID = new SelectList(db.Packages, "ID", "Name", packageStock.PackageID);
            ViewBag.SectorID = new SelectList(db.Sectors, "ID", "Name", packageStock.SectorID);
            return View(packageStock);
        }

        // POST: PackageStocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Quantity,PackageID,SectorID")] PackageStock packageStock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(packageStock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PackageID = new SelectList(db.Packages, "ID", "Name", packageStock.PackageID);
            ViewBag.SectorID = new SelectList(db.Sectors, "ID", "Name", packageStock.SectorID);
            return View(packageStock);
        }

        // GET: PackageStocks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackageStock packageStock = db.PackageStocks.Find(id);
            if (packageStock == null)
            {
                return HttpNotFound();
            }
            return View(packageStock);
        }

        // POST: PackageStocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PackageStock packageStock = db.PackageStocks.Find(id);
            db.PackageStocks.Remove(packageStock);
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



        // GET: PackageStocksStocks/SubmitOrder
        public ActionResult SubmitOrder(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackageStock packageStock = db.PackageStocks.Find(id);
            if (packageStock == null)
            {
                return HttpNotFound();
            }
            ViewBag.PackageID = new SelectList(db.Packages, "ID", "Name", packageStock.PackageID);
            return View(packageStock);
        }


        [HttpPost, ActionName("SubmitOrder")]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitOrder(int orderAmount, int id)
        {
            var newStock = db.PackageStocks.Find(id);
            newStock.Quantity = newStock.SendToProduction(orderAmount, id);

            if (ModelState.IsValid)
            {
                db.Entry(newStock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PackageID = new SelectList(db.Packages, "ID", "Name", newStock.PackageID);
            return View(newStock);
        }
    }
}
