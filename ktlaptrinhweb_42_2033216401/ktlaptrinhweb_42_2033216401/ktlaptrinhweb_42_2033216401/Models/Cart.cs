using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ktlaptrinhweb_42_2033216401.Models;  


namespace ktlaptrinhweb_42_2033216401.Models
{
    public class Cart
    {
        // Danh sách các sản phẩm trong giỏ
        public List<CartItem> Items { get; set; }

        // Constructor khởi tạo giỏ hàng rỗng
        public Cart()
        {
            Items = new List<CartItem>();
        }

        // Thêm sản phẩm vào giỏ hàng
        public void AddToCart(int maSach, string tenSach, decimal giaBan, int soLuong)
        {
            var existingItem = Items.FirstOrDefault(x => x.MaSach == maSach);
            if (existingItem != null)
            {
                existingItem.SoLuong += soLuong; // Nếu sách đã có trong giỏ hàng, tăng số lượng
            }
            else
            {
                // Nếu chưa có sách trong giỏ, tạo mới một item và thêm vào giỏ
                Items.Add(new CartItem
                {
                    MaSach = maSach,
                    TenSach = tenSach,
                    GiaBan = giaBan,
                    SoLuong = soLuong
                });
            }
        }

        // Xóa sản phẩm khỏi giỏ
        public void RemoveFromCart(int maSach)
        {
            var item = Items.FirstOrDefault(x => x.MaSach == maSach);
            if (item != null)
            {
                Items.Remove(item); // Xóa item khỏi giỏ
            }
        }

        // Tính tổng tiền của giỏ hàng
        public decimal GetTotal()
        {
            return Items.Sum(x => x.GiaBan * x.SoLuong); // Tính tổng bằng cách nhân số lượng và giá của từng sản phẩm
        }

        // Xóa tất cả sản phẩm trong giỏ
        public void Clear()
        {
            Items.Clear(); // Xóa tất cả các sản phẩm
        }
    }

}