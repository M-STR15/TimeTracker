using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTracker.BE.DB.Migrations.SQLite
{
    /// <inheritdoc />
    public partial class Update01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Record");

            migrationBuilder.EnsureSchema(
                name: "Project");

            migrationBuilder.EnsureSchema(
                name: "Shift");

            migrationBuilder.RenameTable(
                name: "TypeShifts",
                schema: "dbo",
                newName: "TypeShifts",
                newSchema: "Shift");

            migrationBuilder.RenameTable(
                name: "SubModule",
                schema: "dbo",
                newName: "SubModule",
                newSchema: "Project");

            migrationBuilder.RenameTable(
                name: "Shifts",
                schema: "dbo",
                newName: "Shifts",
                newSchema: "Shift");

            migrationBuilder.RenameTable(
                name: "Record_activities",
                schema: "dbo",
                newName: "Record_activities",
                newSchema: "Record");

            migrationBuilder.RenameTable(
                name: "Project",
                schema: "dbo",
                newName: "Project",
                newSchema: "Project");

            migrationBuilder.RenameTable(
                name: "Activities",
                schema: "dbo",
                newName: "Activities",
                newSchema: "Record");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "TypeShifts",
                schema: "Shift",
                newName: "TypeShifts",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "SubModule",
                schema: "Project",
                newName: "SubModule",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Shifts",
                schema: "Shift",
                newName: "Shifts",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Record_activities",
                schema: "Record",
                newName: "Record_activities",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Project",
                schema: "Project",
                newName: "Project",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Activities",
                schema: "Record",
                newName: "Activities",
                newSchema: "dbo");
        }
    }
}
