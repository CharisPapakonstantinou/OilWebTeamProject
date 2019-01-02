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
    public class RawMaterialStocksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RawMaterialStocks
        public ActionResult Index()
        {
            var rawMaterialStocks = db.RawMaterialStocks.Include(r => r.RawMaterial).Include(r => r.Sector);
            return View(rawMaterialStocks.ToList());
        }

        // GET: RawMaterialStocks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterialStock rawMaterialStock = db.RawMaterialStocks.Find(id);
            if (rawMaterialStock == null)
            {
                return HttpNotFound();
            }
            return View(rawMaterialStock);
        }

        // GET: RawMaterialStocks/Create
        public ActionResult Create()
        {
            ViewBag.RawMaterialID = new SelectList(db.RawMaterials, "ID", "Description");
            ViewBag.SectorID = new SelectList(db.Sectors, "ID", "Name");
            return View();
        }

        // POST: RawMaterialStocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Quantity,RawMaterialID,SectorID")] RawMaterialStock rawMaterialStock)
        {
            if (ModelState.IsValid)
            {
                db.RawMaterialStocks.Add(rawMaterialStock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RawMaterialID = new SelectList(db.RawMaterials, "ID", "Description", rawMaterialStock.RawMaterialID);
            ViewBag.SectorID = new SelectList(db.Sectors, "ID", "Name", rawMaterialStock.SectorID);
            return View(rawMaterialStock);
        }

        // GET: RawMaterialStocks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterialStock rawMaterialStock = db.RawMaterialStocks.Find(id);
            if (rawMaterialStock == null)
            {
                return HttpNotFound();
            }
            ViewBag.RawMaterialID = new SelectList(db.RawMaterials, "ID", "Description", rawMaterialStock.RawMaterialID);
            ViewBag.SectorID = new SelectList(db.Sectors, "ID", "Name", rawMaterialStock.SectorID);
            return View(rawMaterialStock);
        }

        // POST: RawMaterialStocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Quantity,RawMaterialID,SectorID")] RawMaterialStock rawMaterialStock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rawMaterialStock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RawMaterialID = new SelectList(db.RawMaterials, "ID", "Description", rawMaterialStock.RawMaterialID);
            ViewBag.SectorID = new SelectList(db.Sectors, "ID", "Name", rawMaterialStock.SectorID);
            return View(rawMaterialStock);
        }

        // GET: RawMaterialStocks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterialStock rawMaterialStock = db.RawMaterialStocks.Find(id);
            if (rawMaterialStock == null)
            {
                return HttpNotFound();
            }
            return View(rawMaterialStock);
        }

        // POST: RawMaterialStocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RawMaterialStock rawMaterialStock = db.RawMaterialStocks.Find(id);
            db.RawMaterialStocks.Remove(rawMaterialStock);
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





        // GET: RawMaterialStocks/SubmitOrder
        public ActionResult SubmitOrder(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterialStock rawMaterialStock = db.RawMaterialStocks.Find(id);
            if (rawMaterialStock == null)
            {
                return HttpNotFound();
            }
            ViewBag.RawMaterialID = new SelectList(db.RawMaterials, "ID", "Description", rawMaterialStock.RawMaterialID);
            return View(rawMaterialStock);
        }


        [HttpPost, ActionName("SubmitOrder")]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitOrder(int productionOrderAmount, int id)
        {
            var newStock = db.RawMaterialStocks.Find(id);
            newStock.Quantity = newStock.SendToProduction(productionOrderAmount, id);

            if (ModelState.IsValid)
            {
                
                db.Entry(newStock).State = EntityState.Modified; 
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RawMaterialID = new SelectList(db.RawMaterials, "ID", "Description", newStock.RawMaterialID);
            return View(newStock);
        }
    }
}
