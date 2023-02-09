using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutureFridges.Migrations
{
    /// <inheritdoc />
    public partial class FridgeDB20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NotifyAllHeadChefs",
                table: "Settings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56fada2a-2b97-43d7-99a2-c19179a28c57",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserType" },
                values: new object[] { "afa1d5d6-0120-4854-afe5-61a197a951cd", "AQAAAAEAACcQAAAAEKWR4cRiArRJOJAiOJ9+V7lR5PgspHXlrtSIA+5FsaXPI1Bimvm8Ifg4x6OMK2dEKw==", "a393609c-cc85-44c4-a4de-99dd14c7ce36", 1 });

            migrationBuilder.UpdateData(
                table: "AuditLogs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EventTime", "UID" },
                values: new object[] { new DateTime(2023, 2, 9, 14, 52, 3, 89, DateTimeKind.Local).AddTicks(3508), new Guid("6f692169-c649-4a0a-b70f-f38c8a9dca73") });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Order_UID", "Product_UID", "Supplier_UID", "UID" },
                values: new object[] { new Guid("215fde49-288d-41e8-a768-583b01f2ee9d"), new Guid("c0c1847b-1007-4e1e-820e-86976226c158"), new Guid("a27ccab3-cdf9-4888-a2fa-d16a3462ad9a"), new Guid("13d17b23-778b-4c41-ab11-6bace3502274") });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "Supplier_UID",
                value: new Guid("a27ccab3-cdf9-4888-a2fa-d16a3462ad9a"));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Supplier_UID",
                value: new Guid("a27ccab3-cdf9-4888-a2fa-d16a3462ad9a"));

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Administrator_UID", "NotifyAllHeadChefs", "UID" },
                values: new object[] { new Guid("56fada2a-2b97-43d7-99a2-c19179a28c57"), false, new Guid("89aea9fd-a242-455a-87ec-6b12c568453b") });

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpiryDate", "Product_UID" },
                values: new object[] { new DateTime(2023, 2, 9, 14, 52, 3, 89, DateTimeKind.Local).AddTicks(3394), new Guid("c0c1847b-1007-4e1e-820e-86976226c158") });

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
                name: "NotifyAllHeadChefs",
                table: "Settings");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56fada2a-2b97-43d7-99a2-c19179a28c57",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserType" },
                values: new object[] { "d89f74fa-b692-4b66-bb7f-5844950dc7f8", "AQAAAAEAACcQAAAAEES/DwV6QxWB1Ck0/KEJbWvIjuJ16hxcH80jyksoy6Y7KkYpsoaAM93BpM6LJUUXVA==", "eda9a3a4-8cbe-42da-8d33-bd6167199f6b", 0 });

            migrationBuilder.UpdateData(
                table: "AuditLogs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EventTime", "UID" },
                values: new object[] { new DateTime(2023, 2, 2, 19, 53, 33, 267, DateTimeKind.Local).AddTicks(6008), new Guid("c3f5ff9b-451a-4023-8554-ddefdca45273") });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Order_UID", "Product_UID", "Supplier_UID", "UID" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("35c8c206-cc43-461b-8b8a-5d0be93fa6a3") });

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
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Administrator_UID", "UID" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("2202dcd0-3e82-49a0-964c-c96ee5de2467") });

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpiryDate", "Product_UID" },
                values: new object[] { new DateTime(2023, 2, 2, 19, 53, 33, 267, DateTimeKind.Local).AddTicks(5862), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: -1,
                column: "User_UID",
                value: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
