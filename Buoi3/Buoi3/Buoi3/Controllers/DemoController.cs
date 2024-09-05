using Microsoft.AspNetCore.Mvc;
using Buoi3.Models;

namespace Buoi3.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            SinhVien sv = new SinhVien { MSSV = "01", HoTen="Nguyễn Hoàng Hải" };
            ViewBag.thongtin = "Thông tin sinh viên";
            ViewBag.thongtin2 = "Họ & Tên";
            return View(sv);
        }
        public IActionResult GioiThieu()
        {
            return View();
        }
    }
}
