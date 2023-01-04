using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutureFridges.Migrations
{
    /// <inheritdoc />
    public partial class FridgeDB10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItem",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "OrderItem",
                newName: "OrderItems");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56fada2a-2b97-43d7-99a2-c19179a28c57",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a61372d0-74e7-48e8-99f7-7b54cac14fe9", "AQAAAAEAACcQAAAAEOvmOIlXAmZh/5AD9f9YZRYXIhI2pYFz112R7lLqQb/0bsesttBNMxT0E22uehqobw==", "731e0e35-700c-44c1-9061-c6df2d3527f8" });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Order_UID", "Product_UID", "UID" },
                values: new object[] { new Guid("215fde49-288d-41e8-a768-583b01f2ee9d"), new Guid("c0c1847b-1007-4e1e-820e-86976226c158"), new Guid("4937ed71-1ec5-4f3d-aefd-e5b1d749a3ae") });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Product_UID",
                value: new Guid("c0c1847b-1007-4e1e-820e-86976226c158"));

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpiryDate", "Product_UID" },
                values: new object[] { new DateTime(2023, 1, 4, 12, 25, 20, 780, DateTimeKind.Local).AddTicks(3786), new Guid("c0c1847b-1007-4e1e-820e-86976226c158") });

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
            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "OrderItems",
                newName: "OrderItem");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItem",
                table: "OrderItem",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56fada2a-2b97-43d7-99a2-c19179a28c57",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f6ee64b1-f09d-46be-bdab-c87a63571273", "AQAAAAEAACcQAAAAEJQRlgX/diq+fVVnEj3cxMEJUOuR5OaADp27ohNvbhBYmlhMEEo/whvXAdiwNIp1dg==", "57542b2c-082c-46ab-a767-71a491ed5824" });

            migrationBuilder.UpdateData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Order_UID", "Product_UID", "UID" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("74bf178c-3eff-45b5-aa59-fc818eba137a") });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Product_UID",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpiryDate", "Product_UID" },
                values: new object[] { new DateTime(2023, 1, 3, 21, 47, 25, 508, DateTimeKind.Local).AddTicks(244), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: -1,
                column: "User_UID",
                value: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
