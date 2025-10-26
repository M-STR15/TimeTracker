using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTracker.BE.DB.Migrations.SQLite
{
    /// <inheritdoc />
    public partial class Update02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Shifts_ShiftGuidId",
                schema: "Shift",
                table: "Shifts");

            migrationBuilder.DropIndex(
                name: "IX_Shifts_ShiftGuidId",
                schema: "Shift",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "ShiftGuidId",
                schema: "Shift",
                table: "Shifts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShiftGuidId",
                schema: "Shift",
                table: "Shifts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_ShiftGuidId",
                schema: "Shift",
                table: "Shifts",
                column: "ShiftGuidId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Shifts_ShiftGuidId",
                schema: "Shift",
                table: "Shifts",
                column: "ShiftGuidId",
                principalSchema: "Shift",
                principalTable: "Shifts",
                principalColumn: "Guid_ID");
        }
    }
}
