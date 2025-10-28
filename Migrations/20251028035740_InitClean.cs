using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantMVC.Migrations
{
    /// <inheritdoc />
    public partial class InitClean : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietOrders_MonAns_MonAnMaMon",
                table: "ChiTietOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietOrders_Orders_OrderMaOrder",
                table: "ChiTietOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Bans_BanMaBan",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_BanMaBan",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietOrders_MonAnMaMon",
                table: "ChiTietOrders");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietOrders_OrderMaOrder",
                table: "ChiTietOrders");

            migrationBuilder.DropColumn(
                name: "BanMaBan",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MaKH",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MaNV",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "ChiTietOrders");

            migrationBuilder.DropColumn(
                name: "MonAnMaMon",
                table: "ChiTietOrders");

            migrationBuilder.DropColumn(
                name: "OrderMaOrder",
                table: "ChiTietOrders");

            migrationBuilder.RenameColumn(
                name: "MaHD",
                table: "HoaDons",
                newName: "MaHoaDon");

            migrationBuilder.AlterColumn<int>(
                name: "MaBan",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GhiChu",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DaCoc",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "SoTienCoc",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaMon",
                table: "ChiTietOrders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "MaOrder",
                table: "ChiTietOrders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.CreateTable(
                name: "NguoiDungs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDangNhap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VaiTro = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDungs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MaBan",
                table: "Orders",
                column: "MaBan");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietOrders_MaMon",
                table: "ChiTietOrders",
                column: "MaMon");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietOrders_MonAns_MaMon",
                table: "ChiTietOrders",
                column: "MaMon",
                principalTable: "MonAns",
                principalColumn: "MaMon",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietOrders_Orders_MaOrder",
                table: "ChiTietOrders",
                column: "MaOrder",
                principalTable: "Orders",
                principalColumn: "MaOrder",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Bans_MaBan",
                table: "Orders",
                column: "MaBan",
                principalTable: "Bans",
                principalColumn: "MaBan",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietOrders_MonAns_MaMon",
                table: "ChiTietOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietOrders_Orders_MaOrder",
                table: "ChiTietOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Bans_MaBan",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "NguoiDungs");

            migrationBuilder.DropIndex(
                name: "IX_Orders_MaBan",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietOrders_MaMon",
                table: "ChiTietOrders");

            migrationBuilder.DropColumn(
                name: "DaCoc",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SoTienCoc",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "MaHoaDon",
                table: "HoaDons",
                newName: "MaHD");

            migrationBuilder.AlterColumn<int>(
                name: "MaBan",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "GhiChu",
                table: "Orders",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "BanMaBan",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaKH",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaNV",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaMon",
                table: "ChiTietOrders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "MaOrder",
                table: "ChiTietOrders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "ChiTietOrders",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MonAnMaMon",
                table: "ChiTietOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderMaOrder",
                table: "ChiTietOrders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BanMaBan",
                table: "Orders",
                column: "BanMaBan");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietOrders_MonAnMaMon",
                table: "ChiTietOrders",
                column: "MonAnMaMon");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietOrders_OrderMaOrder",
                table: "ChiTietOrders",
                column: "OrderMaOrder");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietOrders_MonAns_MonAnMaMon",
                table: "ChiTietOrders",
                column: "MonAnMaMon",
                principalTable: "MonAns",
                principalColumn: "MaMon");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietOrders_Orders_OrderMaOrder",
                table: "ChiTietOrders",
                column: "OrderMaOrder",
                principalTable: "Orders",
                principalColumn: "MaOrder");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Bans_BanMaBan",
                table: "Orders",
                column: "BanMaBan",
                principalTable: "Bans",
                principalColumn: "MaBan");
        }
    }
}
