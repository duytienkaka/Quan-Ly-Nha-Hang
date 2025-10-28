using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RestaurantMVC.Data;
using System.Security.Claims;

namespace RestaurantMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = _context.NguoiDungs.FirstOrDefault(u =>
                u.TenDangNhap == username && u.MatKhau == password);

            if (user == null)
            {
                ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu!";
                return View();
            }

            // ✅ Tạo danh tính đăng nhập
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.TenDangNhap),
                new Claim(ClaimTypes.Role, user.VaiTro)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // ✅ Chuyển hướng theo vai trò
            return user.VaiTro switch
            {
                "ADMIN" => RedirectToAction("Index", "Admin"),
                "THU_NGAN" => RedirectToAction("Index", "Cashier"),
                "PHUC_VU" => RedirectToAction("Index", "Staff"),
                "USER" => RedirectToAction("Index", "CustomerHome"),
                _ => RedirectToAction("Login")
            };
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied() => View();
    }
}
