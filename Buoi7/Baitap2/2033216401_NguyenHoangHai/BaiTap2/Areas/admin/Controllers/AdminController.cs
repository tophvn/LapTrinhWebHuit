using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using BaiTap2.Models;

namespace BaiTap2.Areas.admin.Controllers
{
    [Area("admin")]
    public class AdminController : Controller
    {
        private SqlConnection GetDatabaseConnection()
        {
            return DatabaseAdmin.GetConnection();
        }

        public IActionResult Index()
        {
            List<Category1> categories = new List<Category1>();
            try
            {
                using (var conn = GetDatabaseConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT * FROM ChuDe", conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(new Category1
                            {
                                MaChuDe = (int)reader["MaChuDe"],
                                TenChuDe = reader["TenChuDe"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error loading categories: " + ex.Message);
            }
            return View("~/Areas/admin/Views/Admin/Index.cshtml", categories);
        }

        [HttpPost]
        public IActionResult AddCategory(string tenChuDe)
        {
            if (string.IsNullOrEmpty(tenChuDe))
            {
                return BadRequest("Tên chủ đề không được để trống.");
            }

            try
            {
                using (var conn = GetDatabaseConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand("INSERT INTO ChuDe (TenChuDe) VALUES (@TenChuDe)", conn);
                    cmd.Parameters.AddWithValue("@TenChuDe", tenChuDe);
                    cmd.ExecuteNonQuery();
                }
                return Ok("Thêm chủ đề thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest("Có lỗi xảy ra: " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult UpdateCategory(int maChuDe, string tenChuDe)
        {
            if (string.IsNullOrEmpty(tenChuDe))
            {
                return BadRequest("Tên chủ đề không được để trống.");
            }

            try
            {
                using (var conn = GetDatabaseConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand("UPDATE ChuDe SET TenChuDe = @TenChuDe WHERE MaChuDe = @MaChuDe", conn);
                    cmd.Parameters.AddWithValue("@TenChuDe", tenChuDe);
                    cmd.Parameters.AddWithValue("@MaChuDe", maChuDe);
                    cmd.ExecuteNonQuery();
                }
                return Ok("Cập nhật chủ đề thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest("Có lỗi xảy ra: " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                using (var conn = GetDatabaseConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand("DELETE FROM ChuDe WHERE MaChuDe = @MaChuDe", conn);
                    cmd.Parameters.AddWithValue("@MaChuDe", id);
                    cmd.ExecuteNonQuery();
                }
                return Ok("Xóa chủ đề thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest("Có lỗi xảy ra: " + ex.Message);
            }
        }
    }
}
