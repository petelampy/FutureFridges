using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutureFridges.Migrations
{
    /// <inheritdoc />
    public partial class FridgeDB3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserUID = table.Column<Guid>(name: "User_UID", type: "uniqueidentifier", nullable: false),
                    AddStock = table.Column<bool>(type: "bit", nullable: false),
                    ManageHealthAndSafetyReport = table.Column<bool>(type: "bit", nullable: false),
                    ManageUser = table.Column<bool>(type: "bit", nullable: false),
                    RemoveStock = table.Column<bool>(type: "bit", nullable: false),
                    ViewStock = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "873d6c80-2c60-4ad6-97bd-a79e576d76c3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a6895cf5-38ec-47ab-ad5d-0bd1c9c2029e", "AQAAAAEAACcQAAAAELBSTw0PNb4nl/d/IOinWwB+b+8ilpVHpjNU7zNTqCd+ArDCSnUGW50R/UrH6cd0YQ==", "3397867f-7fb0-474b-bae9-78093a1f1745" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "873d6c80-2c60-4ad6-97bd-a79e576d76c3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "90914176-8a63-4231-b87a-bbfb05f1088c", "AQAAAAEAACcQAAAAELpzbDVGCCAPk4g27Q+fY8cfbQ/EfhNYGhq/4XKbMlUPvTJvIfQfJcNRG94oE6ITzQ==", "514b435f-3e97-476f-97ec-277c9a38218a" });
        }
    }
}
