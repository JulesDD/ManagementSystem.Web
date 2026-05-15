using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d1b9c8e5-9c3a-4f0e-8b1a-2c3e4f5a6b7c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9a860b31-b0cf-44ec-9c98-883d6988e084", "AQAAAAIAAYagAAAAEBqQW8v6VwJz0l5M4Y7n5tYxY0ZzYv3FZp1v8s7zVqzYvP0Zl6F3mX9VxY5m9w==", "b9c1f2d3-4e5f-6789-abcd-1234567890ab" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d1b9c8e5-9c3a-4f0e-8b1a-2c3e4f5a6b7c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b6903a53-635e-4407-8af5-eff0eda3507b", "AQAAAAIAAYagAAAAELaI/5WoOeiBVRIMrb2A3gh3SNwx4gE7V2edOrHU/1bjTLSGLiXXUYlVyZvZgKP77A==", "dd1d9b5e-ebf0-492e-af85-5a4fd722ced2" });
        }
    }
}
