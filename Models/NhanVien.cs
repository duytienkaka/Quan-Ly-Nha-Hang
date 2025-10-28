using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace RestaurantMVC.Models
{
    public enum VaiTro
    {
        ADMIN,
        THU_NGAN,
        PHUC_VU
    }

    public class NhanVien
    {
        [Key]
        public int MaNV { get; set; }

        [Required, StringLength(100)]
        public string HoTen { get; set; } = string.Empty;

        [StringLength(50)]
        public string? ChucVu { get; set; }

        [Required, StringLength(50)]
        public string TenDangNhap { get; set; } = string.Empty;

        [Required, StringLength(255)]
        public string MatKhauBam { get; set; } = string.Empty; // password hash

        public VaiTro VaiTro { get; set; } = VaiTro.PHUC_VU;

        // Quan hệ 1-N với Order
        public ICollection<Order>? Orders { get; set; }
    }
}
