using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutureFridges.Migrations
{
    /// <inheritdoc />
    public partial class FridgeDB18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Product_UID",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: true);

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
                values: new object[] { new Guid("215fde49-288d-41e8-a768-583b01f2ee9d"), new Guid("c0c1847b-1007-4e1e-820e-86976226c158"), new Guid("4ca4d4bd-c108-472a-8109-d6a46777eab1") });

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
                columns: new[] { "Administrator_UID", "UID" },
                values: new object[] { new Guid("56fada2a-2b97-43d7-99a2-c19179a28c57"), new Guid("741ccde1-56fd-4be1-beb8-e107c0418cf1") });

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpiryDate", "Product_UID" },
                values: new object[] { new DateTime(2023, 1, 26, 20, 36, 53, 628, DateTimeKind.Local).AddTicks(719), new Guid("c0c1847b-1007-4e1e-820e-86976226c158") });

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
                name: "Product_UID",
                table: "Notifications");

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
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("c54ba986-f4d6-456c-8c0e-f58e03059b10") });

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
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpiryDate", "Product_UID" },
                values: new object[] { new DateTime(2023, 1, 26, 20, 15, 42, 909, DateTimeKind.Local).AddTicks(655), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: -1,
                column: "User_UID",
                value: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
