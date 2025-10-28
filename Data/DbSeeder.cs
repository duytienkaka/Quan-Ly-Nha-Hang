using RestaurantMVC.Models;

namespace RestaurantMVC.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext db)
        {
            // Nếu chưa có nhân viên nào thì thêm mẫu
            if (!db.NhanViens.Any())
            {
                db.NhanViens.AddRange(
                    new NhanVien
                    {
                        HoTen = "Quản trị viên",
                        TenDangNhap = "admin",
                        MatKhauBam = "123", // mật khẩu test (chưa hash)
                        VaiTro = VaiTro.ADMIN
                    },
                    new NhanVien
                    {
                        HoTen = "Thu ngân A",
                        TenDangNhap = "cashier",
                        MatKhauBam = "123",
                        VaiTro = VaiTro.THU_NGAN
                    },
                    new NhanVien
                    {
                        HoTen = "Phục vụ B",
                        TenDangNhap = "staff",
                        MatKhauBam = "123",
                        VaiTro = VaiTro.PHUC_VU
                    }
                );
                db.SaveChanges();
            }

            // Nếu chưa có khách hàng nào
            if (!db.AppUsers.Any())
            {
                db.AppUsers.Add(
                    new AppUser
                    {
                        Username = "user1",
                        PasswordHash = "123", // mật khẩu test
                        FullName = "Khách hàng demo",
                        Email = "user1@example.com",
                        Phone = "0901234567"
                    }
                );
                db.SaveChanges();
            }
        }
    }
}
