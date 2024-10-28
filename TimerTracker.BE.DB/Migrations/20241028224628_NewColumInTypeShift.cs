using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimerTracker.BE.DB.Migrations
{
    /// <inheritdoc />
    public partial class NewColumInTypeShift : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVisibleInMainWindow",
                schema: "dbo",
                table: "TypeShifts",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "TypeShifts",
                keyColumn: "TypeShift_ID",
                keyValue: 1,
                column: "IsVisibleInMainWindow",
                value: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "TypeShifts",
                keyColumn: "TypeShift_ID",
                keyValue: 2,
                column: "IsVisibleInMainWindow",
                value: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "TypeShifts",
                keyColumn: "TypeShift_ID",
                keyValue: 3,
                column: "IsVisibleInMainWindow",
                value: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "TypeShifts",
                keyColumn: "TypeShift_ID",
                keyValue: 4,
                column: "IsVisibleInMainWindow",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVisibleInMainWindow",
                schema: "dbo",
                table: "TypeShifts");
        }
    }
}
