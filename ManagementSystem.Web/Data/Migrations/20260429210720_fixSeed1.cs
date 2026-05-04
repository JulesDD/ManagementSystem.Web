using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixSeed1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15605dde-16ce-4c93-832c-ded1c5231fd0",
                column: "ConcurrencyStamp",
                value: "role-emp-001");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52a5d602-2cd4-4a20-aaf5-6c4a8bfbb718",
                column: "ConcurrencyStamp",
                value: "role-admin-001");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5414e681-f604-48c6-b46b-dc170cec5a6b",
                column: "ConcurrencyStamp",
                value: "role-sup-001");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d1b9c8e5-9c3a-4f0e-8b1a-2c3e4f5a6b7c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c1a2b3d4-e5f6-7890-abcd-1234567890ef", "AQAAAAIAAYagAAAAEBqQW8v6VwJz0l5M4Y7n5tYxY0ZzYv3FZp1v8s7zVqzYvP0Zl6F3mX9VxY5m9w==", "b9c1f2d3-4e5f-6789-abcd-1234567890ab" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15605dde-16ce-4c93-832c-ded1c5231fd0",
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52a5d602-2cd4-4a20-aaf5-6c4a8bfbb718",
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5414e681-f604-48c6-b46b-dc170cec5a6b",
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d1b9c8e5-9c3a-4f0e-8b1a-2c3e4f5a6b7c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c2b9a0ea-ab49-4d84-97cc-019b99c24849", "b00155e4d7e99858626896e76f244341215a3fc9", "076bf1de-0342-462c-b1b3-b3baf49696d5" });
        }
    }
}
