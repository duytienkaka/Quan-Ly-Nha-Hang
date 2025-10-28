using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantMVC.Models
{
    public enum TrangThaiHoaDon
    {
        CHUA_THANH_TOAN,
        DA_THANH_TOAN
    }

    public enum PhuongThucThanhToan
    {
        TIEN_MAT,
        CHUYEN_KHOAN
    }


    public class HoaDon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaHoaDon { get; set; }

        [ForeignKey("Order")]
        public int MaOrder { get; set; }
        public Order Order { get; set; }

        public DateTime NgayGio { get; set; }
        public decimal TongTien { get; set; }

        public TrangThaiHoaDon TrangThai { get; set; }
        public PhuongThucThanhToan PhuongThuc { get; set; }
    }
}
