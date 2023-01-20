using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutureFridges.Migrations
{
    /// <inheritdoc />
    public partial class FridgeDB14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Supplier_UID",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Supplier_UID",
                table: "OrderItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56fada2a-2b97-43d7-99a2-c19179a28c57",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "27f8ae23-222a-4195-8024-e4dc534839d2", "AQAAAAEAACcQAAAAEK4QfWIK1oMBF3CBtGbbwDeuih6MgsbEUt1yiODK9nOMqF772Fv1My9791yVXCfdeA==", "4c7274be-fe88-4376-b5e3-8d5c9a8e8d23" });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Order_UID", "Product_UID", "Supplier_UID", "UID" },
                values: new object[] { new Guid("215fde49-288d-41e8-a768-583b01f2ee9d"), new Guid("c0c1847b-1007-4e1e-820e-86976226c158"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("2089b65b-2443-4894-b5cc-c7704ae1c062") });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "Supplier_UID",
                value: null);

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
                values: new object[] { new DateTime(2023, 1, 19, 12, 46, 42, 63, DateTimeKind.Local).AddTicks(8513), new Guid("c0c1847b-1007-4e1e-820e-86976226c158") });

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
            migrationBuilder.DropColumn(
                name: "Supplier_UID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Supplier_UID",
                table: "OrderItems");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56fada2a-2b97-43d7-99a2-c19179a28c57",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ab38440a-1e70-4f5c-80f5-fa631fdc5e5c", "AQAAAAEAACcQAAAAEPKpCAoRQ21rz1f/zKjzVkO8nc3u1clYc0Z7yvb7kjyjEMcVTvJasdX3/YHkV8V/Dg==", "14154dbc-8cfa-4a44-a69e-8204c3d156e6" });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Order_UID", "Product_UID", "UID" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("521c4461-6fbd-404a-97b4-20a937c13069") });

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
                values: new object[] { new DateTime(2023, 1, 19, 10, 59, 22, 114, DateTimeKind.Local).AddTicks(2102), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: -1,
                column: "User_UID",
                value: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
