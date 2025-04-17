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
    public class DatVesController : Controller
    {
        private QL_SuKienAmNhacEntities1 db = new QL_SuKienAmNhacEntities1();

        // GET: DatVes
        public ActionResult Index()
        {
            var datVes = db.DatVes.Include(d => d.KhachHang).Include(d => d.Ve);
            return View(datVes.ToList());
        }

        // GET: DatVes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatVe datVe = db.DatVes.Find(id);
            if (datVe == null)
            {
                return HttpNotFound();
            }
            return View(datVe);
        }

        // GET: DatVes/Create
        public ActionResult Create()
        {
            ViewBag.MaKhachHang = new SelectList(db.KhachHangs, "MaKhachHang", "TenDangNhap");
            ViewBag.MaVe = new SelectList(db.Ves, "MaVe", "MaSuKien");
            DatVe modal = new DatVe();
            return View(modal);
        }

        // POST: DatVes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDatVe,MaKhachHang,MaVe,SoLuong,TongTien,NgayDat")] DatVe datVe)
        {
            if (ModelState.IsValid)
            {
                db.DatVes.Add(datVe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKhachHang = new SelectList(db.KhachHangs, "MaKhachHang", "TenDangNhap", datVe.MaKhachHang);
            ViewBag.MaVe = new SelectList(db.Ves, "MaVe", "MaSuKien", datVe.MaVe);
            return View(datVe);
        }

        // GET: DatVes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatVe datVe = db.DatVes.Find(id);
            if (datVe == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKhachHang = new SelectList(db.KhachHangs, "MaKhachHang", "TenDangNhap", datVe.MaKhachHang);
            ViewBag.MaVe = new SelectList(db.Ves, "MaVe", "MaSuKien", datVe.MaVe);
            return View(datVe);
        }

        // POST: DatVes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDatVe,MaKhachHang,MaVe,SoLuong,TongTien,NgayDat")] DatVe datVe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(datVe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKhachHang = new SelectList(db.KhachHangs, "MaKhachHang", "TenDangNhap", datVe.MaKhachHang);
            ViewBag.MaVe = new SelectList(db.Ves, "MaVe", "MaSuKien", datVe.MaVe);
            return View(datVe);
        }

        // GET: DatVes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatVe datVe = db.DatVes.Find(id);
            if (datVe == null)
            {
                return HttpNotFound();
            }
            return View(datVe);
        }

        // POST: DatVes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DatVe datVe = db.DatVes.Find(id);
            db.DatVes.Remove(datVe);
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
