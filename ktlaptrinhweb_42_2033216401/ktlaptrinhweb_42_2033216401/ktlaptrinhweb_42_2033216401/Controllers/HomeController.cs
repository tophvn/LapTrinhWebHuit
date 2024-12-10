using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using ktlaptrinhweb_42_2033216401.Models;
using System.Collections.Generic; 

namespace ktlaptrinhweb_42_2033216401.Controllers
{
    public class HomeController : Controller
    {
        private ktlaptrinhweb_2033216401Entities db = new ktlaptrinhweb_2033216401Entities();

        // GET: /Home/
        public ActionResult Index(int page = 1)
        {
            int pageSize = 20;
            var sach = db.Sach.Include(s => s.ChuDe).Include(s => s.NhaXuatBan);
            int totalBooks = sach.Count();
            var booksOnPage = sach.OrderBy(s => s.MaSach)
                                  .Skip((page - 1) * pageSize)
                                  .Take(pageSize);
            int totalPages = (int)Math.Ceiling((double)totalBooks / pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(booksOnPage.ToList());
        }

        // GET: /Home/Details/5
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

        // GET: /Home/Create
        public ActionResult Create()
        {
            ViewBag.MaChuDe = new SelectList(db.ChuDe, "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBan, "MaNXB", "TenNXB");
            return View();
        }

        // POST: /Home/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSach,TenSach,GiaBan,MoTa,NgayCapNhat,AnhBia,SoLuongTon,MaChuDe,MaNXB,Moi")] Sach sach)
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

        // GET: /Home/Edit/5
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

        // POST: /Home/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSach,TenSach,GiaBan,MoTa,NgayCapNhat,AnhBia,SoLuongTon,MaChuDe,MaNXB,Moi")] Sach sach)
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

        // Thêm sản phẩm vào giỏ hàng
        public ActionResult AddToCart(int maSach)
        {
            var sach = db.Sach.FirstOrDefault(s => s.MaSach == maSach);

            if (sach != null)
            {
                List<Sach> cart = Session["Cart"] as List<Sach>;
                if (cart == null)
                {
                    cart = new List<Sach>();
                }

                cart.Add(sach);
                Session["Cart"] = cart;
            }

            return RedirectToAction("Cart");
        }

        // Xem giỏ hàng
        public ActionResult Cart()
        {
            List<Sach> cart = Session["Cart"] as List<Sach>;
            if (cart == null)
            {
                cart = new List<Sach>();
            }

            return View(cart);
        }
        [HttpPost]
        public ActionResult RemoveFromCart(string maSach)
        {
            var cart = Session["Cart"] as List<string> ?? new List<string>();
            if (cart.Contains(maSach))
            {
                cart.Remove(maSach);
            }
            Session["Cart"] = cart;
            TempData["Message"] = "Sản phẩm đã được xóa khỏi giỏ hàng!";
            return RedirectToAction("Index");
        }
        // GET: /Home/Delete/5
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

        // POST: /Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sach sach = db.Sach.Find(id);
            db.Sach.Remove(sach);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /Home/Login
        public ActionResult Login()
        {
            return View();
        }

        // MD5
        private string MD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        // POST: /Home/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string hashedPassword = MD5Hash(model.Password);

                var user = db.KhachHang.FirstOrDefault(u => u.TaiKhoan == model.Username && u.MatKhau == hashedPassword);
                if (user != null)
                {
                    Session["UserID"] = user.MaKH;
                    Session["Username"] = user.TaiKhoan;
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng.");
            }
            return View(model);
        }

        // GET: /Home/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Home/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(KhachHang model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.KhachHang.SingleOrDefault(k => k.TaiKhoan == model.TaiKhoan);
                if (existingUser != null)
                {
                    ViewBag.ErrorMessage = "Tài khoản đã tồn tại. Vui lòng chọn tài khoản khác.";
                    return View(model);
                }
                model.MatKhau = MD5Hash(model.MatKhau);
                db.KhachHang.Add(model);
                db.SaveChanges();
                Session["User"] = model;
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        // Logout
        public ActionResult Logout()
        {
            Session.Clear();
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

    // LoginViewModel class
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
