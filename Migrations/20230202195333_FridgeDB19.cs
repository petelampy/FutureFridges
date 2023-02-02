using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutureFridges.Migrations
{
    /// <inheritdoc />
    public partial class FridgeDB19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ManageOrders",
                table: "UserPermissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ManageSuppliers",
                table: "UserPermissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ViewAuditLog",
                table: "UserPermissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "StockItem_UID",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56fada2a-2b97-43d7-99a2-c19179a28c57",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d89f74fa-b692-4b66-bb7f-5844950dc7f8", "AQAAAAEAACcQAAAAEES/DwV6QxWB1Ck0/KEJbWvIjuJ16hxcH80jyksoy6Y7KkYpsoaAM93BpM6LJUUXVA==", "eda9a3a4-8cbe-42da-8d33-bd6167199f6b" });

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
                columns: new[] { "Order_UID", "Product_UID", "UID" },
                values: new object[] { new Guid("215fde49-288d-41e8-a768-583b01f2ee9d"), new Guid("c0c1847b-1007-4e1e-820e-86976226c158"), new Guid("35c8c206-cc43-461b-8b8a-5d0be93fa6a3") });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MinimumStockLevel", "Supplier_UID" },
                values: new object[] { 2, new Guid("a27ccab3-cdf9-4888-a2fa-d16a3462ad9a") });

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Administrator_UID", "UID" },
                values: new object[] { new Guid("56fada2a-2b97-43d7-99a2-c19179a28c57"), new Guid("2202dcd0-3e82-49a0-964c-c96ee5de2467") });

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpiryDate", "Product_UID" },
                values: new object[] { new DateTime(2023, 2, 2, 19, 53, 33, 267, DateTimeKind.Local).AddTicks(5862), new Guid("c0c1847b-1007-4e1e-820e-86976226c158") });

            migrationBuilder.UpdateData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "ManageOrders", "ManageSuppliers", "User_UID", "ViewAuditLog" },
                values: new object[] { true, true, new Guid("56fada2a-2b97-43d7-99a2-c19179a28c57"), true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManageOrders",
                table: "UserPermissions");

            migrationBuilder.DropColumn(
                name: "ManageSuppliers",
                table: "UserPermissions");

            migrationBuilder.DropColumn(
                name: "ViewAuditLog",
                table: "UserPermissions");

            migrationBuilder.DropColumn(
                name: "StockItem_UID",
                table: "Notifications");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56fada2a-2b97-43d7-99a2-c19179a28c57",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "29a37d25-f01b-465a-9eb3-63948bd6ba49", "AQAAAAEAACcQAAAAECD9IcPemvdDFlfGb1NrK2Yo3X6x/U9R6coM5fiDRAiDbGvCQV3AhhN++nbq1Zqjgw==", "723d0802-8a53-4d1d-aea4-3acc33f6a75f" });

            migrationBuilder.UpdateData(
                table: "AuditLogs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EventTime", "UID" },
                values: new object[] { new DateTime(2023, 1, 26, 20, 36, 53, 628, DateTimeKind.Local).AddTicks(856), new Guid("4a5f2ec0-2a0b-4901-a339-d4312cfb7ecf") });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Order_UID", "Product_UID", "UID" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("4ca4d4bd-c108-472a-8109-d6a46777eab1") });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MinimumStockLevel", "Supplier_UID" },
                values: new object[] { 0, new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Administrator_UID", "UID" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("741ccde1-56fd-4be1-beb8-e107c0418cf1") });

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpiryDate", "Product_UID" },
                values: new object[] { new DateTime(2023, 1, 26, 20, 36, 53, 628, DateTimeKind.Local).AddTicks(719), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: -1,
                column: "User_UID",
                value: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
