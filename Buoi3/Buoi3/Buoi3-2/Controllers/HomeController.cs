using Buoi3_2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Buoi3_2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.home = "Trang Chủ";
            return View();
        }
        public IActionResult Login()
        {
            ViewBag.login = "Trang Đăng Nhập";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
