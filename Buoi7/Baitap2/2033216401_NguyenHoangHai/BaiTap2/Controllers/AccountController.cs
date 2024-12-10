using Microsoft.AspNetCore.Mvc;

namespace BaiTap2.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (IsValidUser(username, password))
            {
                string userRole = GetUserRole(username);

                if (userRole == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Tên tài khoản hoặc mật khẩu không đúng.");
            return View();
        }

        [HttpPost]
        public IActionResult Register()
        {
            return RedirectToAction("Login"); 
        }

        // Phương thức kiểm tra tính hợp lệ của thông tin đăng nhập
        private bool IsValidUser(string username, string password)
        {
            // Logic để kiểm tra tên tài khoản và mật khẩu
            return true; // Trả về true nếu hợp lệ
        }

        // Phương thức lấy vai trò người dùng từ cơ sở dữ liệu
        private string GetUserRole(string username)
        {
            // Logic để lấy vai trò từ cơ sở dữ liệu
            return "User"; // Trả về "Admin" hoặc "User" tùy theo người dùng
        }
    }
}
