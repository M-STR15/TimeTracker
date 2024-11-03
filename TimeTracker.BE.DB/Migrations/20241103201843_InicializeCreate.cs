using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TimeTracker.BE.DB.Migrations
{
    /// <inheritdoc />
    public partial class InicializeCreate : Migration
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
                    Activity_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Activity_ID);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                schema: "dbo",
                columns: table => new
                {
                    Project_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Project_ID);
                });

            migrationBuilder.CreateTable(
                name: "TypeShifts",
                schema: "dbo",
                columns: table => new
                {
                    TypeShift_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: false),
                    IsVisibleInMainWindow = table.Column<bool>(type: "INTEGER", nullable: false),
                    TypeShiftId = table.Column<int>(type: "INTEGER", nullable: true)
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
                    SubModule_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Project_ID = table.Column<int>(type: "INTEGER", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                schema: "dbo",
                columns: table => new
                {
                    Guid_ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Start_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    TypeShift_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    ShiftGuidId = table.Column<Guid>(type: "TEXT", nullable: true)
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
                    Guid_ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Activity_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Project_ID = table.Column<int>(type: "INTEGER", nullable: true),
                    Shift_GuidID = table.Column<Guid>(type: "TEXT", nullable: true),
                    Start_time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SubModule_ID = table.Column<int>(type: "INTEGER", nullable: true),
                    TypeShift_ID = table.Column<int>(type: "INTEGER", nullable: false)
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
                        principalColumn: "TypeShift_ID",
                        onDelete: ReferentialAction.Cascade);
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
                table: "Project",
                columns: new[] { "Project_ID", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "", "Project 1" },
                    { 2, "", "Project 2" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "TypeShifts",
                columns: new[] { "TypeShift_ID", "Color", "IsVisibleInMainWindow", "Name", "TypeShiftId" },
                values: new object[,]
                {
                    { 1, "Orange", true, "Office", null },
                    { 2, "SkyBlue", true, "HomeOffice", null },
                    { 3, "MediumPurple", true, "Others", null },
                    { 4, "LawnGreen", false, "Holiday", null }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Record_activities",
                columns: new[] { "Guid_ID", "Activity_ID", "Description", "Project_ID", "Shift_GuidID", "Start_time", "SubModule_ID", "TypeShift_ID" },
                values: new object[,]
                {
                    { new Guid("077adf3c-0e3b-433f-81f7-567f52ace32c"), 2, "", null, null, new DateTime(2024, 10, 2, 11, 30, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("0b00655e-cf24-479d-9d02-c1597820ed06"), 1, "", null, null, new DateTime(2024, 10, 2, 7, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("18da9692-0c9b-41f4-ac26-bdf09c4cba21"), 1, "", null, null, new DateTime(2024, 10, 3, 7, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("21d38036-bdff-45db-ac05-9a4f6a8e656f"), 3, "", null, null, new DateTime(2024, 10, 4, 15, 10, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { new Guid("22abe4b8-8f9c-4c6f-a24a-2169061cad96"), 2, "", null, null, new DateTime(2024, 10, 3, 11, 30, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("236b4b42-9d64-4302-9ade-3525d7018ca1"), 3, "", null, null, new DateTime(2024, 10, 5, 16, 0, 0, 0, DateTimeKind.Unspecified), null, 3 },
                    { new Guid("2465f517-b5d3-4dea-b45d-d8f26904711f"), 1, "", null, null, new DateTime(2024, 10, 4, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { new Guid("276a60b0-47c9-48c3-a89d-a4a6a8db8599"), 2, "", null, null, new DateTime(2024, 10, 5, 11, 40, 0, 0, DateTimeKind.Unspecified), null, 3 },
                    { new Guid("2893acc4-fcdd-4b10-b48e-36abce20da1e"), 3, "", null, null, new DateTime(2024, 10, 1, 15, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("2e039f71-abe6-4452-950b-42b1cd7237f2"), 3, "", null, null, new DateTime(2024, 10, 2, 15, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("613fd0a1-2f54-4b61-a2e7-c66315b3135b"), 1, "", null, null, new DateTime(2024, 10, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("695f6463-0eda-48ee-b911-3f16225e71e4"), 1, "", null, null, new DateTime(2024, 10, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("9d3136b3-aff1-4290-b920-3bc765719d8f"), 1, "", null, null, new DateTime(2024, 10, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), null, 3 },
                    { new Guid("a43e22c9-960c-4d17-a619-76069ca5c85e"), 2, "", null, null, new DateTime(2024, 10, 1, 11, 30, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("b236b601-f1af-4aeb-849d-0b046f6c195a"), 1, "", null, null, new DateTime(2024, 10, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("b93fb740-b90a-4a2c-bedf-acd82edaa789"), 1, "", null, null, new DateTime(2024, 10, 2, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("c2dd8ac4-989f-47d8-8cc5-4b0573bc56ec"), 2, "", null, null, new DateTime(2024, 10, 4, 11, 40, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { new Guid("c925ab6c-e5df-453e-9ef5-4e468f4f0c7b"), 1, "", null, null, new DateTime(2024, 10, 4, 7, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { new Guid("d16627ba-4542-4308-9003-ee31a0750706"), 3, "", null, null, new DateTime(2024, 10, 3, 15, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("fef8e4a5-208a-4377-b314-4638d78e545c"), 1, "", null, null, new DateTime(2024, 10, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 3 }
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
