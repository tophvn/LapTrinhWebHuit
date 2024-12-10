// Models/Sach.cs
using System;

namespace Buoi5.Models
{
    public class Sach
    {
        public int MaSach { get; set; }
        public string TenSach { get; set; }
        public decimal GiaBan { get; set; }
        public string MoTa { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public string AnhBia { get; set; }
        public int? SoLuongTon { get; set; }
        public int? MaChuDe { get; set; }
        public int? MaNXB { get; set; }
        public int? Moi { get; set; }
    }
}
