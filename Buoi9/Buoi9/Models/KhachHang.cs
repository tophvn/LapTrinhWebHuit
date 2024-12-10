using System.ComponentModel.DataAnnotations;

namespace Buoi9.Models
{
    public class KhachHang
    {
        [Key] 
        public int MaKH { get; set; }
        public string HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string DienThoai { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
    }

}
