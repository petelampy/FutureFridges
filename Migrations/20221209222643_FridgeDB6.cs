using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutureFridges.Migrations
{
    /// <inheritdoc />
    public partial class FridgeDB6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ManageProduct",
                table: "UserPermissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56fada2a-2b97-43d7-99a2-c19179a28c57",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f298b29b-622b-40a3-b5c0-544927ee7262", "AQAAAAEAACcQAAAAEAEeP3SuVbK10fsAAHwQuob67SCCW1DbrJYdhqz6tk3QSc8kyjsexBgicW+sc+gRPw==", "e0d8b329-f6f0-44f8-af08-b373ecb29d3d" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Product_UID",
                value: new Guid("c0c1847b-1007-4e1e-820e-86976226c158"));

            migrationBuilder.UpdateData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "ManageProduct", "ManageUser", "User_UID" },
                values: new object[] { false, true, new Guid("56fada2a-2b97-43d7-99a2-c19179a28c57") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManageProduct",
                table: "UserPermissions");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56fada2a-2b97-43d7-99a2-c19179a28c57",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "16972f77-6ecf-4135-bc15-14361832d74f", "AQAAAAEAACcQAAAAELuEHZfG82F2dl63L3GR8VFbt9FyiOwhhKcVtI1L52+9+/Cxzk9XPp1z/x5ALUdqBQ==", "69e510ec-fa37-46eb-bbc7-b53a3512f80d" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Product_UID",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "ManageUser", "User_UID" },
                values: new object[] { false, new Guid("00000000-0000-0000-0000-000000000000") });
        }
    }
}
