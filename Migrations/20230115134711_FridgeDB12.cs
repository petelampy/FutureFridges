using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutureFridges.Migrations
{
    /// <inheritdoc />
    public partial class FridgeDB12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CreateOrder",
                table: "UserPermissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56fada2a-2b97-43d7-99a2-c19179a28c57",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6c3280df-3f0f-45e7-a260-8cff254e7c92", "AQAAAAEAACcQAAAAEB6pXvCyB09h9zY93F8TMV/w6+zRQNqR2F+g2eiW9EpBGLBQT96WDttnAmzMkC/kHQ==", "2a4f1b35-40a4-45fd-a9f4-810d717b7a26" });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Order_UID", "Product_UID", "UID" },
                values: new object[] { new Guid("215fde49-288d-41e8-a768-583b01f2ee9d"), new Guid("c0c1847b-1007-4e1e-820e-86976226c158"), new Guid("ccff74e0-7262-411a-97f6-ea060cb2eaf2") });

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpiryDate", "Product_UID" },
                values: new object[] { new DateTime(2023, 1, 15, 13, 47, 11, 355, DateTimeKind.Local).AddTicks(5145), new Guid("c0c1847b-1007-4e1e-820e-86976226c158") });

            migrationBuilder.UpdateData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreateOrder", "ManageProduct", "User_UID" },
                values: new object[] { true, true, new Guid("56fada2a-2b97-43d7-99a2-c19179a28c57") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateOrder",
                table: "UserPermissions");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56fada2a-2b97-43d7-99a2-c19179a28c57",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5a6dbef8-907c-43d0-a567-9fa9ec6b7a73", "AQAAAAEAACcQAAAAEHoML0RxsN+IL0cZECCICIzgWvkMDgFOmOGA0uQ0G/JlE8y5iHubryd7EKu2A59CwQ==", "7e53287c-eda4-411a-9b66-78ef1a0e94ac" });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Order_UID", "Product_UID", "UID" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("dd93e858-4e4a-46b5-af78-5b74ea20bfc6") });

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpiryDate", "Product_UID" },
                values: new object[] { new DateTime(2023, 1, 10, 18, 6, 14, 314, DateTimeKind.Local).AddTicks(7103), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "ManageProduct", "User_UID" },
                values: new object[] { false, new Guid("00000000-0000-0000-0000-000000000000") });
        }
    }
}
