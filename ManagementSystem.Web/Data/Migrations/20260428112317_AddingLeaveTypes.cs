using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingLeaveTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "leaveTypeId",
                table: "LeaveTypes",
                newName: "LeaveTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LeaveTypeId",
                table: "LeaveTypes",
                newName: "leaveTypeId");
        }
    }
}
