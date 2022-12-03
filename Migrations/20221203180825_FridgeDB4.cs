using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutureFridges.Migrations
{
    /// <inheritdoc />
    public partial class FridgeDB4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "873d6c80-2c60-4ad6-97bd-a79e576d76c3");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "56fada2a-2b97-43d7-99a2-c19179a28c57", 0, "c563b560-13af-4324-ad70-7193a0868201", "admin@fridges.com", true, false, null, "ADMIN@FRIDGES.COM", "ADMIN", "AQAAAAEAACcQAAAAEFCHo4r72AaNrV672kQ2ZYJE68fJezOg04Bi+qRrEe1WijLOiBYNP4yYi9kuE2EX/A==", null, false, "7a697472-98cd-4e22-915e-92a25d372543", false, "Admin", 0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Name", "Product_UID" },
                values: new object[] { 1, 2, "CHEESE", new Guid("c0c1847b-1007-4e1e-820e-86976226c158") });

            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "Id", "AddStock", "ManageHealthAndSafetyReport", "ManageUser", "RemoveStock", "User_UID", "ViewStock" },
                values: new object[] { -1, true, true, false, true, new Guid("56fada2a-2b97-43d7-99a2-c19179a28c57"), true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "56fada2a-2b97-43d7-99a2-c19179a28c57");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "873d6c80-2c60-4ad6-97bd-a79e576d76c3", 0, "a6895cf5-38ec-47ab-ad5d-0bd1c9c2029e", "admin@fridges.com", true, false, null, "ADMIN@FRIDGES.COM", "ADMIN", "AQAAAAEAACcQAAAAELBSTw0PNb4nl/d/IOinWwB+b+8ilpVHpjNU7zNTqCd+ArDCSnUGW50R/UrH6cd0YQ==", null, false, "3397867f-7fb0-474b-bae9-78093a1f1745", false, "Admin", 0 });
        }
    }
}
