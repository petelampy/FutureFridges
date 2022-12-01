using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutureFridges.Migrations
{
    /// <inheritdoc />
    public partial class FridgeDB2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "873d6c80-2c60-4ad6-97bd-a79e576d76c3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserType" },
                values: new object[] { "90914176-8a63-4231-b87a-bbfb05f1088c", "AQAAAAEAACcQAAAAELpzbDVGCCAPk4g27Q+fY8cfbQ/EfhNYGhq/4XKbMlUPvTJvIfQfJcNRG94oE6ITzQ==", "514b435f-3e97-476f-97ec-277c9a38218a", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "873d6c80-2c60-4ad6-97bd-a79e576d76c3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1022e29e-fa23-4779-a525-c2842f3f13d2", "AQAAAAEAACcQAAAAEPkIZd3G41GGgzlcMoe//j2pbcgBxP6+dWNGbGAHAvHiVXaPfkRYYKdX6VnhZ9F9ag==", "250d30c5-e758-4096-adaa-f72df505f469" });
        }
    }
}
