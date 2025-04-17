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
    public class DanhGiasController : Controller
    {
        private QL_SuKienAmNhacEntities1 db = new QL_SuKienAmNhacEntities1();

        // GET: DanhGias
        public ActionResult Index()
        {
            var danhGias = db.DanhGias.Include(d => d.KhachHang).Include(d => d.SuKien);
            return View(danhGias.ToList());
        }

        // GET: DanhGias/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhGia danhGia = db.DanhGias.Find(id);
            if (danhGia == null)
            {
                return HttpNotFound();
            }
            return View(danhGia);
        }

        // GET: DanhGias/Create
        public ActionResult Create()
        {
            ViewBag.MaKhachHang = new SelectList(db.KhachHangs, "MaKhachHang", "TenDangNhap");
            ViewBag.MaSuKien = new SelectList(db.SuKiens, "MaSuKien", "TenSuKien");
            return View();
        }

        // POST: DanhGias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDanhGia,MaSuKien,MaKhachHang,SoSao,NoiDung,NgayDanhGia")] DanhGia danhGia)
        {
            if (ModelState.IsValid)
            {
                db.DanhGias.Add(danhGia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKhachHang = new SelectList(db.KhachHangs, "MaKhachHang", "TenDangNhap", danhGia.MaKhachHang);
            ViewBag.MaSuKien = new SelectList(db.SuKiens, "MaSuKien", "TenSuKien", danhGia.MaSuKien);
            return View(danhGia);
        }

        // GET: DanhGias/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhGia danhGia = db.DanhGias.Find(id);
            if (danhGia == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKhachHang = new SelectList(db.KhachHangs, "MaKhachHang", "TenDangNhap", danhGia.MaKhachHang);
            ViewBag.MaSuKien = new SelectList(db.SuKiens, "MaSuKien", "TenSuKien", danhGia.MaSuKien);
            return View(danhGia);
        }

        // POST: DanhGias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDanhGia,MaSuKien,MaKhachHang,SoSao,NoiDung,NgayDanhGia")] DanhGia danhGia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(danhGia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKhachHang = new SelectList(db.KhachHangs, "MaKhachHang", "TenDangNhap", danhGia.MaKhachHang);
            ViewBag.MaSuKien = new SelectList(db.SuKiens, "MaSuKien", "TenSuKien", danhGia.MaSuKien);
            return View(danhGia);
        }

        // GET: DanhGias/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhGia danhGia = db.DanhGias.Find(id);
            if (danhGia == null)
            {
                return HttpNotFound();
            }
            return View(danhGia);
        }

        // POST: DanhGias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DanhGia danhGia = db.DanhGias.Find(id);
            db.DanhGias.Remove(danhGia);
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
