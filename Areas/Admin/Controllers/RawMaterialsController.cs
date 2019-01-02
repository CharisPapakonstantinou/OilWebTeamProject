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
    public class RawMaterialsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/RawMaterials
        public ActionResult Index()
        {
            return View(db.RawMaterials.ToList());
        }

        // GET: Admin/RawMaterials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterial rawMaterial = db.RawMaterials.Find(id);
            if (rawMaterial == null)
            {
                return HttpNotFound();
            }
            return View(rawMaterial);
        }

        // GET: Admin/RawMaterials/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/RawMaterials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Price")] RawMaterial rawMaterial)
        {
            if (ModelState.IsValid)
            {
                db.RawMaterials.Add(rawMaterial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rawMaterial);
        }

        // GET: Admin/RawMaterials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterial rawMaterial = db.RawMaterials.Find(id);
            if (rawMaterial == null)
            {
                return HttpNotFound();
            }
            return View(rawMaterial);
        }

        // POST: Admin/RawMaterials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Price")] RawMaterial rawMaterial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rawMaterial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rawMaterial);
        }

        // GET: Admin/RawMaterials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterial rawMaterial = db.RawMaterials.Find(id);
            if (rawMaterial == null)
            {
                return HttpNotFound();
            }
            return View(rawMaterial);
        }

        // POST: Admin/RawMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RawMaterial rawMaterial = db.RawMaterials.Find(id);
            db.RawMaterials.Remove(rawMaterial);
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
