using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace RestaurantMVC.Models
{
    public enum TinhTrangMon
    {
        CON,
        HET
    }

    public class MonAn
    {
        [Key]
        public int MaMon { get; set; }

        [Required, StringLength(100)]
        public string TenMon { get; set; } = string.Empty;

        [StringLength(50)]
        public string? Loai { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Gia { get; set; }

        public TinhTrangMon TinhTrang { get; set; } = TinhTrangMon.CON;

        // Quan hệ N-N thông qua ChiTietOrder
        public ICollection<ChiTietOrder>? ChiTietOrders { get; set; }
    }
}
