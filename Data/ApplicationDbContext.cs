using Microsoft.EntityFrameworkCore;
using RestaurantMVC.Models;

namespace RestaurantMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ban> Bans { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<NguoiDung> NguoiDungs { get; set; }

        public DbSet<MonAn> MonAns { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ChiTietOrder> ChiTietOrders { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 Order – ChiTietOrder: 1 - n
            modelBuilder.Entity<ChiTietOrder>()
                .HasKey(c => new { c.MaOrder, c.MaMon });

            modelBuilder.Entity<ChiTietOrder>()
                .HasOne(c => c.Order)
                .WithMany(o => o.ChiTietOrders)
                .HasForeignKey(c => c.MaOrder);

            // 🔹 Order – HoaDon: 1 - 1
            modelBuilder.Entity<Order>()
                .HasOne(o => o.HoaDon)
                .WithOne(h => h.Order)
                .HasForeignKey<HoaDon>(h => h.MaOrder);

            // 🔹 Enum conversion cho HoaDon
            modelBuilder.Entity<HoaDon>()
                .Property(h => h.TrangThai)
                .HasConversion<string>();

            modelBuilder.Entity<HoaDon>()
                .Property(h => h.PhuongThuc)
                .HasConversion<string>();

            // Enum được lưu dưới dạng chuỗi (string)
            modelBuilder.Entity<Ban>().Property(b => b.TrangThai).HasConversion<string>();
            modelBuilder.Entity<NhanVien>().Property(nv => nv.VaiTro).HasConversion<string>();
            modelBuilder.Entity<MonAn>().Property(m => m.TinhTrang).HasConversion<string>();
            modelBuilder.Entity<Order>().Property(o => o.TrangThai).HasConversion<string>();
            modelBuilder.Entity<HoaDon>().Property(h => h.PhuongThuc).HasConversion<string>();
            modelBuilder.Entity<HoaDon>().Property(h => h.TrangThai).HasConversion<string>();
        }
    }
}
