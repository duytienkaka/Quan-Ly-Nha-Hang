using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantMVC.Data;
using RestaurantMVC.Models;

namespace RestaurantMVC.Controllers
{
    [Authorize(Roles = "PHUC_VU")]
    public class StaffController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StaffController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View();

        // ✅ Danh sách bàn
        public async Task<IActionResult> TableList()
        {
            var tables = await _context.Bans.OrderBy(b => b.MaBan).ToListAsync();
            return PartialView("_TableList", tables);
        }

        // ✅ Danh sách món ăn
        public async Task<IActionResult> FoodList(int tableId)
        {
            ViewBag.TableId = tableId;
            var foods = await _context.MonAns
                .Where(m => m.TinhTrang == TinhTrangMon.CON)
                .ToListAsync();
            return PartialView("_FoodList", foods);
        }

        // ✅ Hiển thị order hiện tại của bàn
        public async Task<IActionResult> CurrentOrder(int tableId)
        {
            var order = await _context.Orders
                .Include(o => o.ChiTietOrders)
                .ThenInclude(ct => ct.MonAn)
                .FirstOrDefaultAsync(o => o.MaBan == tableId && o.TrangThai == TrangThaiOrder.DANG_PHUC_VU);

            return PartialView("_CurrentOrder", order);
        }

        // ✅ Thêm món vào order
        [HttpPost]
        public async Task<IActionResult> AddItem(int tableId, int foodId, int quantity)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.MaBan == tableId && o.TrangThai == TrangThaiOrder.DANG_PHUC_VU);

            if (order == null)
            {
                // ✅ Tạo order mới nếu chưa có
                order = new Order
                {
                    MaBan = tableId,
                    TrangThai = TrangThaiOrder.DANG_PHUC_VU,
                    ThoiGian = DateTime.Now
                };
                _context.Orders.Add(order);

                // ✅ Cập nhật trạng thái bàn
                var table = await _context.Bans.FindAsync(tableId);
                if (table != null)
                {
                    table.TrangThai = TrangThaiBan.DANG_PHUC_VU;
                }
            }

            // ✅ Thêm chi tiết món ăn
            var detail = await _context.ChiTietOrders
                .FirstOrDefaultAsync(c => c.MaOrder == order.MaOrder && c.MaMon == foodId);
            if (detail != null)
                detail.SoLuong += quantity;
            else
                _context.ChiTietOrders.Add(new ChiTietOrder
                {
                    MaOrder = order.MaOrder,
                    MaMon = foodId,
                    SoLuong = quantity,
                    DonGia = (await _context.MonAns.FindAsync(foodId)).Gia
                });

            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
