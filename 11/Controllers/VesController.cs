using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _11.Models;

namespace _11.Controllers
{
    public class VesController : Controller
    {
        private QL_SuKienAmNhacEntities1 db = new QL_SuKienAmNhacEntities1();

        // GET: Ves
        public ActionResult Index()
        {
            var ves = db.Ves.Include(v => v.SuKien);
            return View(ves.ToList());
        }

        // GET: Ves/Details/5
        public ActionResult Details(string id)


        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ve ve = db.Ves.Find(id);
            if (ve == null)
            {
                return HttpNotFound();
            }
            return View(ve);
        }

        // GET: Ves/Create
        public ActionResult Create()
        {
            ViewBag.MaSuKien = new SelectList(db.SuKiens, "MaSuKien", "TenSuKien");
            Ve model = new Ve();
            return View(model);
        }



        // POST: Ves/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "MaVe,MaSuKien,LoaiVe,GiaVe,SoLuong,AnhVe")] Ve ve, HttpPostedFileBase file)
    {
            Debug.WriteLine("thuc hien tao ve");
            Debug.WriteLine(ve.MaVe);
            Debug.WriteLine(ve.LoaiVe);
            Debug.WriteLine(ve.GiaVe);
            Debug.WriteLine(ve.SoLuong);
            Debug.WriteLine(ve.AnhVe);
            ve.MaSuKien = "SK202408";
        if (ve == null)
        {
            Debug.WriteLine("Error here");
            throw new ArgumentNullException(nameof(ve));
        }

        if (ModelState.IsValid)
        {
            Debug.WriteLine("set image for model");
            string fileName = "";

            // Check if the file is provided
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    // Tạo đường dẫn thư mục "Images" nếu chưa tồn tại
                    string folderPath = Server.MapPath("~/Images");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // Đặt tên file ngẫu nhiên để tránh trùng
                    fileName = Path.GetFileName(file.FileName); 
                    string filePath = Path.Combine(folderPath, fileName);

                    // Save the file to the Images folder
                    file.SaveAs(filePath);

                    // Save the relative file path to the database (not the full physical path)
                    ve.AnhVe = "Images/" + fileName;
                }
                catch (Exception ex)
                {
                        Debug.WriteLine("no value for Error here !!! ");
                        ModelState.AddModelError("", "Error uploading the file: " + ex.Message);
                    return View(ve);
                }
            }
            else
            {
                  Debug.WriteLine("no value for image ");
                  ve.AnhVe = null;
            }

                Debug.WriteLine("let's go ");
                // Save the ve object to the database
                db.Ves.Add(ve);
            db.SaveChanges();

            // Redirect to Index action after successful save
            return RedirectToAction("Index");
        }
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Debug.WriteLine(error.ErrorMessage);
                }
                return View(ve);
            }
            Debug.WriteLine("het cuu");
        // In case of invalid model state, return the current view with validation errors
        ViewBag.MaSuKien = new SelectList(db.SuKiens, "MaSuKien", "TenSuKien", ve.MaSuKien);
        return View(ve);
    }


        // POST: Ves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        // PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaVe,MaSuKien,LoaiVe,GiaVe,SoLuong,AnhVe")] Ve ve,
            HttpPostedFileBase file)
        {
            Debug.WriteLine($"Received Edit request for MaVe: {ve.MaVe}"); if (ve == null)
               
            {
                throw new ArgumentNullException(nameof(ve));
            }

            if (ModelState.IsValid)
            {
                var existingVe = db.Ves.Find(ve.MaVe); // Lấy vé cũ từ DB
                if (existingVe == null)
                {
                    return HttpNotFound();
                }

                string fileName = existingVe.AnhVe; // Giữ nguyên ảnh cũ nếu không upload mới
                Debug.WriteLine(fileName);
                if (file != null && file.ContentLength > 0)
                {

                    string folderPath = Server.MapPath("~/Images");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    fileName = Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(folderPath, fileName);
                    file.SaveAs(filePath);
                    ve.AnhVe = "Images/" + fileName;
                }
                else
                {
                    ve.AnhVe = existingVe.AnhVe; // Nếu không chọn ảnh mới, giữ ảnh cũ
                }

                // Cập nhật thông tin vé
                existingVe.MaSuKien = ve.MaSuKien;
                existingVe.LoaiVe = ve.LoaiVe;
                existingVe.GiaVe = ve.GiaVe;
                existingVe.SoLuong = ve.SoLuong;
                existingVe.AnhVe = ve.AnhVe;

                db.Entry(existingVe).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.MaSuKien = new SelectList(db.SuKiens, "MaSuKien", "TenSuKien", ve.MaSuKien);
            return View(ve);
        }

        // GET: Ves/Edit/5
        public ActionResult Edit(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ve ve = db.Ves.Find(id);
            if (ve == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaSuKien = new SelectList(db.SuKiens, "MaSuKien", "TenSuKien", ve.MaSuKien);
            return View(ve);
        }





        // GET: Ves/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ve ve = db.Ves.Find(id);
            if (ve == null)
            {
                return HttpNotFound();
            }
            return View(ve);
        }

        // POST: Ves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Ve ve = db.Ves.Find(id);
            db.Ves.Remove(ve);
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
