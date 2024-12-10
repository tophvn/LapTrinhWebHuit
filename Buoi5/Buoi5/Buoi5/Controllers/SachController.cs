// Controllers/SachController.cs
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using Buoi5.Models;

namespace Buoi5.Controllers
{
    public class SachController : Controller
    {
        private readonly string _connectionString = "Server=DESKTOP-PGC7MIC\\HOANGHAI;Database=SACH;User Id=sa;Password=123;";

        public async Task<IActionResult> Index()
        {
            var sachList = new List<Sach>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("SELECT * FROM Sach", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            sachList.Add(new Sach
                            {
                                MaSach = reader.GetInt32(0),
                                TenSach = reader.GetString(1),
                                GiaBan = reader.GetDecimal(2),
                                MoTa = reader.IsDBNull(3) ? null : reader.GetString(3),
                                NgayCapNhat = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                                AnhBia = reader.IsDBNull(5) ? null : reader.GetString(5),
                                SoLuongTon = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6),
                                MaChuDe = reader.IsDBNull(7) ? (int?)null : reader.GetInt32(7),
                                MaNXB = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8),
                                Moi = reader.IsDBNull(9) ? (int?)null : reader.GetInt32(9)
                            });
                        }
                    }
                }
            }

            return View(sachList);
        }
    }
}
