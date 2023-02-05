using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutureFridges.Migrations
{
    /// <inheritdoc />
    public partial class FridgeDB17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinimumStockLevel",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserUID = table.Column<Guid>(name: "User_UID", type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdministratorUID = table.Column<Guid>(name: "Administrator_UID", type: "uniqueidentifier", nullable: false),
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56fada2a-2b97-43d7-99a2-c19179a28c57",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8aad5cdd-dcd0-4829-b8c3-d465302700a2", "AQAAAAEAACcQAAAAEP2Epf9n8sQ41VG5hXozEyuX0eGtPNP8ova+FhdecJd1k/k2/uFQjScEX43Lw6Zh6w==", "51bb1705-abb2-405a-931b-e73a4d498521" });

            migrationBuilder.UpdateData(
                table: "AuditLogs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EventTime", "UID" },
                values: new object[] { new DateTime(2023, 1, 26, 20, 15, 42, 909, DateTimeKind.Local).AddTicks(831), new Guid("f1294480-7b47-4486-aaf2-4ee38497dfe1") });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Order_UID", "Product_UID", "UID" },
                values: new object[] { new Guid("215fde49-288d-41e8-a768-583b01f2ee9d"), new Guid("c0c1847b-1007-4e1e-820e-86976226c158"), new Guid("c54ba986-f4d6-456c-8c0e-f58e03059b10") });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MinimumStockLevel", "Supplier_UID" },
                values: new object[] { 0, new Guid("a27ccab3-cdf9-4888-a2fa-d16a3462ad9a") });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Administrator_UID", "UID" },
                values: new object[] { 1, new Guid("56fada2a-2b97-43d7-99a2-c19179a28c57"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpiryDate", "Product_UID" },
                values: new object[] { new DateTime(2023, 1, 26, 20, 15, 42, 909, DateTimeKind.Local).AddTicks(655), new Guid("c0c1847b-1007-4e1e-820e-86976226c158") });

            migrationBuilder.UpdateData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: -1,
                column: "User_UID",
                value: new Guid("56fada2a-2b97-43d7-99a2-c19179a28c57"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropColumn(
                name: "MinimumStockLevel",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56fada2a-2b97-43d7-99a2-c19179a28c57",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b0a0c03-59d0-4208-b20d-a23589b2bd8f", "AQAAAAEAACcQAAAAEIQZSnhee3GBym2N2NpK4jid1mHlA+ti38OKJPBGamP2tmkl5EBG83a1D+CdDktBGQ==", "68c95820-399c-45f7-81bd-25c382bcb464" });

            migrationBuilder.UpdateData(
                table: "AuditLogs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EventTime", "UID" },
                values: new object[] { new DateTime(2023, 1, 26, 14, 46, 12, 41, DateTimeKind.Local).AddTicks(980), new Guid("3b1a53a3-825f-4a03-b638-52b352580ea9") });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Order_UID", "Product_UID", "UID" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("63827b62-84ca-466d-982d-294170fcec13") });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Supplier_UID",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpiryDate", "Product_UID" },
                values: new object[] { new DateTime(2023, 1, 26, 14, 46, 12, 41, DateTimeKind.Local).AddTicks(693), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: -1,
                column: "User_UID",
                value: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
