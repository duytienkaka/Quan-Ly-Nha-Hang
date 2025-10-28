using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantMVC.Data;
using RestaurantMVC.Models;

namespace RestaurantMVC.Controllers
{
    [Authorize(Roles = "USER")]  // role cho khách hàng
    public class CustomerHomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerHomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View();

        // ✅ Lấy danh sách bàn còn trống để đặt
        public async Task<IActionResult> BookingForm()
        {
            var tables = await _context.Bans
                .Where(b => b.TrangThai == TrangThaiBan.TRONG)
                .ToListAsync();
            return PartialView("_BookingForm", tables);
        }

        // ✅ Xử lý đặt bàn
        [HttpPost]
        public async Task<IActionResult> Book(int tableId, string hoTen, string soDienThoai, int soNguoi, string ghiChu)
        {
            var table = await _context.Bans.FindAsync(tableId);
            if (table == null || table.TrangThai != TrangThaiBan.TRONG)
                return BadRequest("Bàn không khả dụng.");

            // tạo đơn hàng ở trạng thái "chờ cọc"
            var order = new Order
            {
                MaBan = tableId,
                TrangThai = TrangThaiOrder.CHO_THANH_TOAN_COC,
                ThoiGian = DateTime.Now,
                GhiChu = $"Khách: {hoTen} - {soDienThoai} - {ghiChu}"
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // chuyển bàn sang trạng thái "ĐẶT_TRƯỚC"
            table.TrangThai = TrangThaiBan.DAT_TRUOC;
            await _context.SaveChangesAsync();

            return Ok(new { orderId = order.MaOrder });
        }


        // ✅ Lịch sử đặt bàn
        public async Task<IActionResult> BookingHistory()
        {
            var orders = await _context.Orders
                .Include(o => o.Ban)
                .OrderByDescending(o => o.ThoiGian)
                .Take(10)
                .ToListAsync();
            return PartialView("_BookingHistory", orders);
        }

        // ✅ Hủy đặt bàn
        [HttpPost]
        public async Task<IActionResult> CancelBooking(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.Ban)
                .FirstOrDefaultAsync(o => o.MaOrder == orderId && o.TrangThai == TrangThaiOrder.MO);

            if (order == null) return BadRequest("Không thể hủy.");

            order.Ban.TrangThai = TrangThaiBan.TRONG;
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> PayDeposit(int orderId, decimal amount)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return BadRequest();

            order.DaCoc = true;
            order.SoTienCoc = amount;
            order.TrangThai = TrangThaiOrder.DA_COC;
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
