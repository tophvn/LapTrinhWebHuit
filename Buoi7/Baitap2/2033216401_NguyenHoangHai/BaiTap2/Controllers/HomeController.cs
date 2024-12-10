using BaiTap2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

public class HomeController : Controller
{
    // Trang Index
    public ActionResult Index(string searchString, int? categoryId, int? publisherId)
    {
        var model = new BookViewModel
        {
            Books = GetBooks(searchString, categoryId, publisherId),
            Categories = GetCategories(),
            Publishers = GetPublishers()
        };
        return View(model);
    }

    // Tìm sách theo tiêu chí tìm kiếm
    private List<BookAdmin> GetBooks(string searchString, int? categoryId, int? publisherId)
    {
        var books = new List<BookAdmin>();
        using (var connection = DatabaseHelper.GetConnection())
        {
            connection.Open();
            string query = @"
                SELECT s.MaSach, s.TenSach, s.GiaBan, s.MoTa, s.NgayCapNhat, s.AnhBia
                FROM Sach s
                LEFT JOIN ThamGia tg ON s.MaSach = tg.MaSach
                LEFT JOIN TacGia tg2 ON tg.MaTacGia = tg2.MaTacGia
                WHERE 
                    (@SearchString IS NULL OR s.TenSach LIKE '%' + @SearchString + '%' OR tg2.TenTacGia LIKE '%' + @SearchString + '%') AND
                    (@CategoryId IS NULL OR s.MaChuDe = @CategoryId) AND
                    (@PublisherId IS NULL OR s.MaNXB = @PublisherId)
                GROUP BY s.MaSach, s.TenSach, s.GiaBan, s.MoTa, s.NgayCapNhat, s.AnhBia";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@SearchString", (object)searchString ?? DBNull.Value);
                command.Parameters.AddWithValue("@CategoryId", (object)categoryId ?? DBNull.Value);
                command.Parameters.AddWithValue("@PublisherId", (object)publisherId ?? DBNull.Value);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        books.Add(new BookAdmin
                        {
                            MaSach = reader.GetInt32(0),
                            TenSach = reader.GetString(1),
                            GiaBan = reader.GetDecimal(2),
                            MoTa = reader.GetString(3),
                            NgayCapNhat = reader.GetDateTime(4),
                            AnhBia = reader.GetString(5)
                        });
                    }
                }
            }
        }
        return books;
    }

    // Lấy danh sách danh mục
    private List<Category> GetCategories()
    {
        var categories = new List<Category>();
        using (var connection = DatabaseHelper.GetConnection())
        {
            connection.Open();
            string query = "SELECT * FROM ChuDe";

            using (var command = new SqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new Category
                        {
                            MaChuDe = reader.GetInt32(0),
                            TenChuDe = reader.GetString(1)
                        });
                    }
                }
            }
        }
        return categories;
    }

    // Lấy danh sách nhà xuất bản
    private List<Publisher> GetPublishers()
    {
        var publishers = new List<Publisher>();
        using (var connection = DatabaseHelper.GetConnection())
        {
            connection.Open();
            string query = "SELECT * FROM NhaXuatBan";

            using (var command = new SqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        publishers.Add(new Publisher
                        {
                            MaNXB = reader.GetInt32(0),
                            TenNXB = reader.GetString(1)
                        });
                    }
                }
            }
        }
        return publishers;
    }

    // Trang chi tiết sách
    public ActionResult Detail(int id)
    {
        var book = GetBookById(id);
        return View(book);
    }

    // Lấy thông tin chi tiết của sách
    private BookAdmin GetBookById(int id)
    {
        BookAdmin book = null;
        using (var connection = DatabaseHelper.GetConnection())
        {
            connection.Open();
            string query = "SELECT * FROM Sach WHERE MaSach = @Id";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        book = new BookAdmin
                        {
                            MaSach = reader.GetInt32(0),
                            TenSach = reader.GetString(1),
                            GiaBan = reader.GetDecimal(2),
                            MoTa = reader.GetString(3),
                            NgayCapNhat = reader.GetDateTime(4),
                            AnhBia = reader.GetString(5)
                        };
                    }
                }
            }
        }
        return book;
    }

    // Đăng nhập
    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        if (IsValidUser(username, password, out string fullName))
        {
            ViewData["UserName"] = fullName; // Lưu họ tên vào ViewData
            TempData["SuccessMessage"] = "Đăng nhập thành công!";
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError("", "Tên tài khoản hoặc mật khẩu không đúng.");
            return View();
        }
    }

    public IActionResult Login()
    {
        return View();
    }

    // Đăng ký
    [HttpPost]
    public IActionResult Register(string username, string password, string email, string fullName, DateTime dateOfBirth, string gender, string phone, string address)
    {
        using (var connection = DatabaseHelper.GetConnection())
        {
            connection.Open();
            string query = @"
                INSERT INTO KhachHang (TaiKhoan, MatKhau, Email, HoTen, NgaySinh, GioiTinh, DienThoai, DiaChi)
                VALUES (@TaiKhoan, @MatKhau, @Email, @HoTen, @NgaySinh, @GioiTinh, @DienThoai, @DiaChi)";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@TaiKhoan", username);
                command.Parameters.AddWithValue("@MatKhau", password);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@HoTen", fullName);
                command.Parameters.AddWithValue("@NgaySinh", dateOfBirth);
                command.Parameters.AddWithValue("@GioiTinh", gender);
                command.Parameters.AddWithValue("@DienThoai", phone);
                command.Parameters.AddWithValue("@DiaChi", address);

                command.ExecuteNonQuery();
            }
        }
        TempData["SuccessMessage"] = "Đăng ký thành công!";
        return RedirectToAction("Login");
    }

    // Xác thực người dùng
    private bool IsValidUser(string username, string password, out string fullName)
    {
        fullName = null;
        using (var connection = DatabaseHelper.GetConnection())
        {
            connection.Open();
            string query = "SELECT HoTen FROM KhachHang WHERE TaiKhoan = @TaiKhoan AND MatKhau = @MatKhau";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@TaiKhoan", username);
                command.Parameters.AddWithValue("@MatKhau", password);

                var result = command.ExecuteScalar();
                if (result != null)
                {
                    fullName = result.ToString();
                    return true;
                }
            }
        }
        return false;
    }

    // Đăng xuất
    [HttpPost]
    public IActionResult Logout()
    {
        ViewData["UserName"] = null; // Xóa họ tên khỏi ViewData
        TempData["SuccessMessage"] = "Đăng xuất thành công!";
        return RedirectToAction("Index");
    }
}
