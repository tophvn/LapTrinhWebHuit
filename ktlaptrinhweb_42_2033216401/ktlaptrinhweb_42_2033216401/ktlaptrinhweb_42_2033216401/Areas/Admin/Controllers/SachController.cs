using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ktlaptrinhweb_42_2033216401.Models;

namespace ktlaptrinhweb_42_2033216401.Areas.Admin.Controllers
{
    public class SachController : Controller
    {
        private ktlaptrinhweb_2033216401Entities db = new ktlaptrinhweb_2033216401Entities();

        // GET: /Admin/Sach/
        public ActionResult Index()
        {
            var sach = db.Sach.Include(s => s.ChuDe).Include(s => s.NhaXuatBan);
            return View(sach.ToList());
        }

        // GET: /Admin/Sach/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Sach.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        // GET: /Admin/Sach/Create
        public ActionResult Create()
        {
            ViewBag.MaChuDe = new SelectList(db.ChuDe, "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBan, "MaNXB", "TenNXB");
            return View();
        }

        // POST: /Admin/Sach/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MaSach,TenSach,GiaBan,MoTa,NgayCapNhat,AnhBia,SoLuongTon,MaChuDe,MaNXB,Moi")] Sach sach)
        {
            if (ModelState.IsValid)
            {
                db.Sach.Add(sach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaChuDe = new SelectList(db.ChuDe, "MaChuDe", "TenChuDe", sach.MaChuDe);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBan, "MaNXB", "TenNXB", sach.MaNXB);
            return View(sach);
        }

        // GET: /Admin/Sach/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Sach.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaChuDe = new SelectList(db.ChuDe, "MaChuDe", "TenChuDe", sach.MaChuDe);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBan, "MaNXB", "TenNXB", sach.MaNXB);
            return View(sach);
        }

        // POST: /Admin/Sach/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MaSach,TenSach,GiaBan,MoTa,NgayCapNhat,AnhBia,SoLuongTon,MaChuDe,MaNXB,Moi")] Sach sach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaChuDe = new SelectList(db.ChuDe, "MaChuDe", "TenChuDe", sach.MaChuDe);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBan, "MaNXB", "TenNXB", sach.MaNXB);
            return View(sach);
        }

        // GET: /Admin/Sach/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Sach.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        // POST: /Admin/Sach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sach sach = db.Sach.Find(id);
            db.Sach.Remove(sach);
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
