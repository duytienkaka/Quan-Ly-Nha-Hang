using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantMVC.Models
{
    public enum TrangThaiOrder
    {
        MO,                  // Mới đặt
        CHO_THANH_TOAN_COC,  // Chờ khách thanh toán cọc
        DA_COC,              // Đã thanh toán cọc
        DANG_PHUC_VU,
        DA_CHOT,
        HUY_DAT
    }


    public class Order
    {
        [Key] // 👈 khóa chính
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaOrder { get; set; }
        [ForeignKey("Ban")]
        public int MaBan { get; set; }
        public TrangThaiOrder TrangThai { get; set; }
        public DateTime ThoiGian { get; set; }
        public string GhiChu { get; set; }
        public bool DaCoc { get; set; } = false;
        public decimal? SoTienCoc { get; set; }

        // Quan hệ
        public Ban Ban { get; set; }
        public ICollection<ChiTietOrder> ChiTietOrders { get; set; }

        // ✅ Thêm thuộc tính này để EF nhận biết quan hệ 1–1
        public HoaDon HoaDon { get; set; }
    }

}
