using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimerTracker.BE.DB.Migrations
{
    /// <inheritdoc />
    public partial class AddItemTypeShift : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "dbo",
                table: "TypeShifts",
                columns: new[] { "TypeShift_ID", "Color", "Name", "TypeShiftId" },
                values: new object[] { 4, "LawnGreen", "Holiday", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "TypeShifts",
                keyColumn: "TypeShift_ID",
                keyValue: 4);
        }
    }
}
