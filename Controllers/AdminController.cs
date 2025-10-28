using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantMVC.Data;
using RestaurantMVC.Models;

namespace RestaurantMVC.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context) => _context = context;

        public IActionResult Index() => View();

        // =============================
        //       QUẢN LÝ BÀN ĂN
        // =============================

        // Danh sách bàn ăn
        public async Task<IActionResult> ManageTables()
        {
            var tables = await _context.Bans.ToListAsync();
            return PartialView("_ManageTables", tables);
        }

        // GET: /Admin/CreateTable  → hiển thị form thêm bàn
        [HttpGet]
        public IActionResult CreateTable()
        {
            return PartialView("_TableForm", new Ban());
        }

        // POST: /Admin/CreateTable → lưu bàn mới
        [HttpPost]
        public async Task<IActionResult> CreateTable(Ban model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dữ liệu không hợp lệ");

            _context.Bans.Add(model);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // GET: /Admin/EditTable/5 → form sửa
        [HttpGet]
        public async Task<IActionResult> EditTable(int id)
        {
            var ban = await _context.Bans.FindAsync(id);
            if (ban == null)
                return NotFound();
            return PartialView("_TableForm", ban);
        }

        // POST: /Admin/EditTable → lưu chỉnh sửa
        [HttpPost]
        public async Task<IActionResult> EditTable(Ban model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Sai dữ liệu");
            _context.Bans.Update(model);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // POST: /Admin/DeleteTable/5
        [HttpPost]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var ban = await _context.Bans.FindAsync(id);
            if (ban == null)
                return NotFound();
            _context.Bans.Remove(ban);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // ========================================
        //  QUẢN LÝ MÓN ĂN
        // ========================================

        // ✅ GET: /Admin/ManageFoods  → hiển thị danh sách
        public async Task<IActionResult> ManageFoods()
        {
            var foods = await _context.MonAns.ToListAsync();
            return PartialView("_ManageFoods", foods);
        }

        // ✅ GET: /Admin/CreateFood  → hiển thị form trong modal
        [HttpGet]
        public IActionResult CreateFood()
        {
            // Tạo đối tượng trống để Razor bind form
            return PartialView("_FoodForm", new MonAn());
        }

        // ✅ POST: /Admin/CreateFood  → xử lý khi submit form
        [HttpPost]
        public async Task<IActionResult> CreateFood(MonAn model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dữ liệu không hợp lệ");

            _context.MonAns.Add(model);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // ✅ GET: /Admin/EditFood/5  → load form sửa
        [HttpGet]
        public async Task<IActionResult> EditFood(int id)
        {
            var mon = await _context.MonAns.FindAsync(id);
            if (mon == null) return NotFound();
            return PartialView("_FoodForm", mon);
        }

        // ✅ POST: /Admin/EditFood  → xử lý lưu sửa
        [HttpPost]
        public async Task<IActionResult> EditFood(MonAn model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Sai dữ liệu");

            _context.MonAns.Update(model);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // ✅ POST: /Admin/DeleteFood/5  → xóa món ăn
        [HttpPost]
        public async Task<IActionResult> DeleteFood(int id)
        {
            var mon = await _context.MonAns.FindAsync(id);
            if (mon == null) return NotFound();
            _context.MonAns.Remove(mon);
            await _context.SaveChangesAsync();
            return Ok();
        }


        // --- Quản lý nhân viên (mới thêm) ---
        public async Task<IActionResult> ManageEmployees()
        {
            var employees = await _context.NhanViens.ToListAsync();
            return PartialView("_ManageEmployees", employees);
        }

        [HttpGet]
        public IActionResult CreateEmployee() => PartialView("_EmployeeForm", new NhanVien());

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(NhanVien model)
        {
            if (!ModelState.IsValid) return BadRequest("Dữ liệu không hợp lệ");
            _context.NhanViens.Add(model);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> EditEmployee(int id)
        {
            var nv = await _context.NhanViens.FindAsync(id);
            if (nv == null) return NotFound();
            return PartialView("_EmployeeForm", nv);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployee(NhanVien model)
        {
            if (!ModelState.IsValid) return BadRequest("Sai dữ liệu");
            _context.NhanViens.Update(model);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var nv = await _context.NhanViens.FindAsync(id);
            if (nv == null) return NotFound();
            _context.NhanViens.Remove(nv);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // --- Báo cáo thu nhập ---
        public async Task<IActionResult> IncomeReport()
        {
            var now = DateTime.Now;
            var data = await _context.HoaDons
                .Where(h => h.NgayGio.Month == now.Month && h.NgayGio.Year == now.Year)
                .GroupBy(h => h.NgayGio.Date)
                .Select(g => new { Date = g.Key, Total = g.Sum(x => x.TongTien), Count = g.Count() })
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            ViewBag.TotalMonth = data.Sum(x => x.Total);
            return PartialView("_IncomeReport", data);
        }
    }
}
