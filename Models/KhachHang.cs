using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace RestaurantMVC.Models
{
    public class KhachHang
    {
        [Key]
        public int MaKH { get; set; }

        [StringLength(100)]
        public string? HoTen { get; set; }

        [StringLength(15)]
        public string? SDT { get; set; }

        [StringLength(255)]
        public string? GhiChu { get; set; }

        // Quan hệ 1-N với Order
        public ICollection<Order>? Orders { get; set; }
    }
}
