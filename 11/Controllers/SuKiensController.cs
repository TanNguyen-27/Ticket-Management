using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _11.Models;

namespace _11.Controllers
{
    public class SuKiensController : Controller
    {
        private QL_SuKienAmNhacEntities1 db = new QL_SuKienAmNhacEntities1();

        // GET: SuKiens
        public ActionResult Index()
        {
            return View(db.SuKiens.ToList());
        }

        // GET: SuKiens/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuKien suKien = db.SuKiens.Find(id);
            if (suKien == null)
            {
                return HttpNotFound();
            }
            return View(suKien);
        }

        // GET: SuKiens/Create
        public ActionResult Create()
        {
            SuKien modal = new SuKien();
            return View(modal);
        }

        // POST: SuKiens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSuKien,TenSuKien,NgayToChuc,DiaDiem")] SuKien suKien)
        {
            if (ModelState.IsValid)
            {
                db.SuKiens.Add(suKien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(suKien);
        }

        // GET: SuKiens/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuKien suKien = db.SuKiens.Find(id);
            if (suKien == null)
            {
                return HttpNotFound();
            }
            return View(suKien);
        }

        // POST: SuKiens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSuKien,TenSuKien,NgayToChuc,DiaDiem")] SuKien suKien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suKien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(suKien);
        }

        // GET: SuKiens/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuKien suKien = db.SuKiens.Find(id);
            if (suKien == null)
            {
                return HttpNotFound();
            }
            return View(suKien);
        }

        // POST: SuKiens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SuKien suKien = db.SuKiens.Find(id);
            db.SuKiens.Remove(suKien);
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
