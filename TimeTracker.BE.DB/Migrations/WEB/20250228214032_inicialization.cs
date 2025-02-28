using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TimeTracker.BE.DB.Migrations.WEB
{
    /// <inheritdoc />
    public partial class inicialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Activities",
                schema: "dbo",
                columns: table => new
                {
                    Activity_ID = table.Column<int>(type: "int", nullable: false, comment: "Primární klíč aktivity.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Název aktivity.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Activity_ID);
                },
                comment: "Primární klíč aktivity.");

            migrationBuilder.CreateTable(
                name: "Project",
                schema: "dbo",
                columns: table => new
                {
                    Project_ID = table.Column<int>(type: "int", nullable: false, comment: "Primární klíč projektu.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Popis projektu."),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Název projektu.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Project_ID);
                },
                comment: "Tabulka projektů.");

            migrationBuilder.CreateTable(
                name: "TypeShifts",
                schema: "dbo",
                columns: table => new
                {
                    TypeShift_ID = table.Column<int>(type: "int", nullable: false, comment: "Primární klíč typu směny.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Barva směny."),
                    IsVisibleInMainWindow = table.Column<bool>(type: "bit", nullable: false, comment: "Viditelnost směny v hlavním okně."),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Název typu směny."),
                    TypeShiftId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeShifts", x => x.TypeShift_ID);
                    table.ForeignKey(
                        name: "FK_TypeShifts_TypeShifts_TypeShiftId",
                        column: x => x.TypeShiftId,
                        principalSchema: "dbo",
                        principalTable: "TypeShifts",
                        principalColumn: "TypeShift_ID");
                },
                comment: "Tabulka všech možných směn.");

            migrationBuilder.CreateTable(
                name: "SubModule",
                schema: "dbo",
                columns: table => new
                {
                    SubModule_ID = table.Column<int>(type: "int", nullable: false, comment: "Primární klíč podmodulu.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Popis podmodulu."),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Název podmodulu."),
                    Project_ID = table.Column<int>(type: "int", nullable: false, comment: "ID projektu, ke kterému podmodul patří.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubModule", x => x.SubModule_ID);
                    table.ForeignKey(
                        name: "FK_SubModule_Project_Project_ID",
                        column: x => x.Project_ID,
                        principalSchema: "dbo",
                        principalTable: "Project",
                        principalColumn: "Project_ID",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Tabulka podmodulů.");

            migrationBuilder.CreateTable(
                name: "Shifts",
                schema: "dbo",
                columns: table => new
                {
                    Guid_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeShift_ID = table.Column<int>(type: "int", nullable: false),
                    ShiftGuidId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Stamp_DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Guid_ID);
                    table.ForeignKey(
                        name: "FK_Shifts_Shifts_ShiftGuidId",
                        column: x => x.ShiftGuidId,
                        principalSchema: "dbo",
                        principalTable: "Shifts",
                        principalColumn: "Guid_ID");
                    table.ForeignKey(
                        name: "FK_Shifts_TypeShifts_TypeShift_ID",
                        column: x => x.TypeShift_ID,
                        principalSchema: "dbo",
                        principalTable: "TypeShifts",
                        principalColumn: "TypeShift_ID",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Tabulka slouží k naplánování směny.");

            migrationBuilder.CreateTable(
                name: "Record_activities",
                schema: "dbo",
                columns: table => new
                {
                    Guid_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Activity_ID = table.Column<int>(type: "int", nullable: false, comment: "Primární klíč aktivity."),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Popis aktivity."),
                    End_DateTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Datum a čas ukončení aktivity."),
                    Project_ID = table.Column<int>(type: "int", nullable: true),
                    Shift_GuidID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Start_DateTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Datum a čas zahájení aktivity."),
                    SubModule_ID = table.Column<int>(type: "int", nullable: true),
                    TypeShift_ID = table.Column<int>(type: "int", nullable: true),
                    Stamp_DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Record_activities", x => x.Guid_ID);
                    table.ForeignKey(
                        name: "FK_Record_activities_Activities_Activity_ID",
                        column: x => x.Activity_ID,
                        principalSchema: "dbo",
                        principalTable: "Activities",
                        principalColumn: "Activity_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Record_activities_Project_Project_ID",
                        column: x => x.Project_ID,
                        principalSchema: "dbo",
                        principalTable: "Project",
                        principalColumn: "Project_ID");
                    table.ForeignKey(
                        name: "FK_Record_activities_Shifts_Shift_GuidID",
                        column: x => x.Shift_GuidID,
                        principalSchema: "dbo",
                        principalTable: "Shifts",
                        principalColumn: "Guid_ID");
                    table.ForeignKey(
                        name: "FK_Record_activities_SubModule_SubModule_ID",
                        column: x => x.SubModule_ID,
                        principalSchema: "dbo",
                        principalTable: "SubModule",
                        principalColumn: "SubModule_ID");
                    table.ForeignKey(
                        name: "FK_Record_activities_TypeShifts_TypeShift_ID",
                        column: x => x.TypeShift_ID,
                        principalSchema: "dbo",
                        principalTable: "TypeShifts",
                        principalColumn: "TypeShift_ID");
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Activities",
                columns: new[] { "Activity_ID", "Name" },
                values: new object[,]
                {
                    { 1, "Start" },
                    { 2, "Pause" },
                    { 3, "Stop" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "TypeShifts",
                columns: new[] { "TypeShift_ID", "Color", "IsVisibleInMainWindow", "Name", "TypeShiftId" },
                values: new object[,]
                {
                    { 1, "Orange", true, "Office", null },
                    { 2, "SkyBlue", true, "HomeOffice", null },
                    { 3, "Magenta", true, "Others", null },
                    { 4, "LawnGreen", false, "Holiday", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_Name",
                schema: "dbo",
                table: "Activities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_Name",
                schema: "dbo",
                table: "Project",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Record_activities_Activity_ID",
                schema: "dbo",
                table: "Record_activities",
                column: "Activity_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Record_activities_Project_ID",
                schema: "dbo",
                table: "Record_activities",
                column: "Project_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Record_activities_Shift_GuidID",
                schema: "dbo",
                table: "Record_activities",
                column: "Shift_GuidID");

            migrationBuilder.CreateIndex(
                name: "IX_Record_activities_SubModule_ID",
                schema: "dbo",
                table: "Record_activities",
                column: "SubModule_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Record_activities_TypeShift_ID",
                schema: "dbo",
                table: "Record_activities",
                column: "TypeShift_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_ShiftGuidId",
                schema: "dbo",
                table: "Shifts",
                column: "ShiftGuidId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_Start_date",
                schema: "dbo",
                table: "Shifts",
                column: "Start_date",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_TypeShift_ID",
                schema: "dbo",
                table: "Shifts",
                column: "TypeShift_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SubModule_Project_ID_Name",
                schema: "dbo",
                table: "SubModule",
                columns: new[] { "Project_ID", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypeShifts_TypeShiftId",
                schema: "dbo",
                table: "TypeShifts",
                column: "TypeShiftId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Record_activities",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Activities",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Shifts",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SubModule",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TypeShifts",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Project",
                schema: "dbo");
        }
    }
}
