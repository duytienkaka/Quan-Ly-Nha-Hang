using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace RestaurantMVC.Models
{
    public enum TrangThaiBan
    {
        TRONG,
        DAT_TRUOC,
        DANG_PHUC_VU,
        CAN_DON
    }

    public class Ban
    {
        [Key]
        public int MaBan { get; set; }

        [Required, StringLength(50)]
        public string TenBan { get; set; } = string.Empty;

        [StringLength(50)]
        public string? KhuVuc { get; set; }

        [Range(1, int.MaxValue)]
        public int SucChua { get; set; }

        [Required]
        public TrangThaiBan TrangThai { get; set; } = TrangThaiBan.TRONG;

        // Quan hệ 1-N với Order
        public ICollection<Order>? Orders { get; set; }
    }
}
