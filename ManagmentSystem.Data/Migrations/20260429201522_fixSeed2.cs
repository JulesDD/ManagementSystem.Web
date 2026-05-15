using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixSeed2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d1b9c8e5-9c3a-4f0e-8b1a-2c3e4f5a6b7c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c2b9a0ea-ab49-4d84-97cc-019b99c24849", "b00155e4d7e99858626896e76f244341215a3fc9", "076bf1de-0342-462c-b1b3-b3baf49696d5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d1b9c8e5-9c3a-4f0e-8b1a-2c3e4f5a6b7c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9a860b31-b0cf-44ec-9c98-883d6988e084", "AQAAAAIAAYagAAAAEBqQW8v6VwJz0l5M4Y7n5tYxY0ZzYv3FZp1v8s7zVqzYvP0Zl6F3mX9VxY5m9w==", "b9c1f2d3-4e5f-6789-abcd-1234567890ab" });
        }
    }
}
