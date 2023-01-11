using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutureFridges.Migrations
{
    /// <inheritdoc />
    public partial class FridgeDB9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfItems = table.Column<int>(type: "int", nullable: false),
                    PinCode = table.Column<int>(type: "int", nullable: false),
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderUID = table.Column<Guid>(name: "Order_UID", type: "uniqueidentifier", nullable: false),
                    ProductUID = table.Column<Guid>(name: "Product_UID", type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56fada2a-2b97-43d7-99a2-c19179a28c57",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f6ee64b1-f09d-46be-bdab-c87a63571273", "AQAAAAEAACcQAAAAEJQRlgX/diq+fVVnEj3cxMEJUOuR5OaADp27ohNvbhBYmlhMEEo/whvXAdiwNIp1dg==", "57542b2c-082c-46ab-a767-71a491ed5824" });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "NumberOfItems", "PinCode", "UID" },
                values: new object[] { 1, 3, 1001, new Guid("215fde49-288d-41e8-a768-583b01f2ee9d") });

            migrationBuilder.InsertData(
                table: "OrderItem",
                columns: new[] { "Id", "Order_UID", "Product_UID", "Quantity", "UID" },
                values: new object[] { 1, new Guid("215fde49-288d-41e8-a768-583b01f2ee9d"), new Guid("c0c1847b-1007-4e1e-820e-86976226c158"), 3, new Guid("74bf178c-3eff-45b5-aa59-fc818eba137a") });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ImageName", "Product_UID" },
                values: new object[] { "cheese.jpeg", new Guid("c0c1847b-1007-4e1e-820e-86976226c158") });

            migrationBuilder.InsertData(
                table: "StockItems",
                columns: new[] { "Id", "ExpiryDate", "Item_UID", "Product_UID" },
                values: new object[] { 1, new DateTime(2023, 1, 3, 21, 47, 25, 508, DateTimeKind.Local).AddTicks(244), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("c0c1847b-1007-4e1e-820e-86976226c158") });

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
                name: "Order");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DeleteData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56fada2a-2b97-43d7-99a2-c19179a28c57",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d96add34-bc2c-4c65-bedf-85134ce16f6f", "AQAAAAEAACcQAAAAEL2D8iQGAMjaKiy2/TD8AJZddz0DEB83fJIr4ElcAWzWwqJH+9BpjTX6ao0N3gyuWg==", "0ae69642-3efd-4c3d-97b7-aaa5598b72eb" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ImageName", "Product_UID" },
                values: new object[] { null, new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: -1,
                column: "User_UID",
                value: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
