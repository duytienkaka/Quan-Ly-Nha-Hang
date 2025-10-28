using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantMVC.Models
{
    public class ChiTietOrder
    {
        [Key, Column(Order = 0)]
        public int MaOrder { get; set; }

        [Key, Column(Order = 1)]
        public int MaMon { get; set; }

        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }

        [ForeignKey("MaOrder")]
        public Order Order { get; set; }

        [ForeignKey("MaMon")]
        public MonAn MonAn { get; set; }
    }
}
