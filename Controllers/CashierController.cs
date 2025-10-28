using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantMVC.Data;
using RestaurantMVC.Models;

namespace RestaurantMVC.Controllers
{
    [Authorize(Roles = "THU_NGAN")]
    public class CashierController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CashierController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Trang chính
        public IActionResult Index() => View();
        public async Task<IActionResult> Deposits()
        {
            var orders = await _context.Orders
                .Include(o => o.Ban)
                .Where(o => o.TrangThai == TrangThaiOrder.DA_COC)
                .ToListAsync();
            return View(orders);
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmDeposit(int orderId)
        {
            var order = await _context.Orders.Include(o => o.Ban).FirstOrDefaultAsync(o => o.MaOrder == orderId);
            if (order == null) return BadRequest();

            order.TrangThai = TrangThaiOrder.MO; // chuyển sang "đặt thành công"
            order.Ban.TrangThai = TrangThaiBan.DAT_TRUOC;
            await _context.SaveChangesAsync();
            return RedirectToAction("Deposits");
        }


        // ✅ Lấy danh sách bàn
        public async Task<IActionResult> TableList()
        {
            var tables = await _context.Bans
                .OrderBy(b => b.MaBan)
                .ToListAsync();
            return PartialView("_TableList", tables);
        }

        // ✅ Xem order của bàn
        public async Task<IActionResult> OrderDetail(int tableId)
        {
            var order = await _context.Orders
                .Include(o => o.ChiTietOrders)
                .ThenInclude(ct => ct.MonAn)
                .FirstOrDefaultAsync(o => o.MaBan == tableId && o.TrangThai == TrangThaiOrder.DANG_PHUC_VU);

            ViewBag.TableId = tableId;
            return PartialView("_OrderDetail", order);
        }

        [HttpPost]
        public async Task<IActionResult> Pay(int tableId)
        {
            var order = await _context.Orders
         .Include(o => o.Ban)
         .FirstOrDefaultAsync(o => o.MaBan == tableId && o.TrangThai == TrangThaiOrder.DANG_PHUC_VU);

            if (order == null)
                return BadRequest("Không tìm thấy đơn hàng đang phục vụ.");

            // ✅ Tính tổng tiền
            var total = await _context.ChiTietOrders
                .Where(c => c.MaOrder == order.MaOrder)
                .SumAsync(c => c.SoLuong * c.DonGia);

            // ✅ Tạo hóa đơn
            var bill = new HoaDon
            {
                MaOrder = order.MaOrder,
                NgayGio = DateTime.Now,
                TongTien = total,
                TrangThai = TrangThaiHoaDon.DA_THANH_TOAN,
                PhuongThuc = PhuongThucThanhToan.TIEN_MAT
            };
            _context.HoaDons.Add(bill);

            // ✅ Cập nhật trạng thái order & bàn
            order.TrangThai = TrangThaiOrder.DA_CHOT;
            order.Ban.TrangThai = TrangThaiBan.TRONG;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult PayModal(int tableId)
        {
            ViewBag.TableId = tableId;
            return PartialView("_PaymentModal");
        }

    }
}
