using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutureFridges.Migrations
{
    /// <inheritdoc />
    public partial class FridgeDB16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogType = table.Column<int>(type: "int", nullable: false),
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserSupplierName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56fada2a-2b97-43d7-99a2-c19179a28c57",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b0a0c03-59d0-4208-b20d-a23589b2bd8f", "AQAAAAEAACcQAAAAEIQZSnhee3GBym2N2NpK4jid1mHlA+ti38OKJPBGamP2tmkl5EBG83a1D+CdDktBGQ==", "68c95820-399c-45f7-81bd-25c382bcb464" });

            migrationBuilder.InsertData(
                table: "AuditLogs",
                columns: new[] { "Id", "Description", "EventTime", "LogType", "UID", "UserSupplierName" },
                values: new object[] { 1, "This is an example log entry", new DateTime(2023, 1, 26, 14, 46, 12, 41, DateTimeKind.Local).AddTicks(980), 1, new Guid("3b1a53a3-825f-4a03-b638-52b352580ea9"), "SampleUser" });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Order_UID", "Product_UID", "UID" },
                values: new object[] { new Guid("215fde49-288d-41e8-a768-583b01f2ee9d"), new Guid("c0c1847b-1007-4e1e-820e-86976226c158"), new Guid("63827b62-84ca-466d-982d-294170fcec13") });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Supplier_UID",
                value: new Guid("a27ccab3-cdf9-4888-a2fa-d16a3462ad9a"));

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpiryDate", "Product_UID" },
                values: new object[] { new DateTime(2023, 1, 26, 14, 46, 12, 41, DateTimeKind.Local).AddTicks(693), new Guid("c0c1847b-1007-4e1e-820e-86976226c158") });

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
                name: "AuditLogs");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56fada2a-2b97-43d7-99a2-c19179a28c57",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "098222c5-c053-48ca-9f9f-3872b042c6f9", "AQAAAAEAACcQAAAAEGxG9jfoJ1bnhMFD5tyW3B/mHLgXY4LhGP2GUUIRWnwoVDzam/A42s+B/O79Lg5mPw==", "885d4052-08bd-40c3-a5db-ee7981a16482" });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Order_UID", "Product_UID", "UID" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("a70388d9-fd1f-413c-aa83-938b0230162f") });

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
                values: new object[] { new DateTime(2023, 1, 20, 9, 40, 1, 541, DateTimeKind.Local).AddTicks(2467), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: -1,
                column: "User_UID",
                value: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
