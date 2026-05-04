using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDefaultRolesAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "15605dde-16ce-4c93-832c-ded1c5231fd0", null, "Employee", "EMPLOYEE" },
                    { "52a5d602-2cd4-4a20-aaf5-6c4a8bfbb718", null, "Administrator", "ADMINISTRATOR" },
                    { "5414e681-f604-48c6-b46b-dc170cec5a6b", null, "Supervisor", "SUPERVISOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d1b9c8e5-9c3a-4f0e-8b1a-2c3e4f5a6b7c", 0, "b6903a53-635e-4407-8af5-eff0eda3507b", "admin@localhost", true, false, null, "ADMIN@LOCALHOST", "ADMIN@LOCALHOST", "AQAAAAIAAYagAAAAELaI/5WoOeiBVRIMrb2A3gh3SNwx4gE7V2edOrHU/1bjTLSGLiXXUYlVyZvZgKP77A==", null, false, "dd1d9b5e-ebf0-492e-af85-5a4fd722ced2", false, "admin@localhost" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "52a5d602-2cd4-4a20-aaf5-6c4a8bfbb718", "d1b9c8e5-9c3a-4f0e-8b1a-2c3e4f5a6b7c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15605dde-16ce-4c93-832c-ded1c5231fd0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5414e681-f604-48c6-b46b-dc170cec5a6b");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "52a5d602-2cd4-4a20-aaf5-6c4a8bfbb718", "d1b9c8e5-9c3a-4f0e-8b1a-2c3e4f5a6b7c" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52a5d602-2cd4-4a20-aaf5-6c4a8bfbb718");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d1b9c8e5-9c3a-4f0e-8b1a-2c3e4f5a6b7c");
        }
    }
}
