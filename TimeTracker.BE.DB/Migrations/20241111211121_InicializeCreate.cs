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
                    End_time = table.Column<DateTime>(type: "TEXT", nullable: true),
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
                    { 3, "Magenta", true, "Others", null },
                    { 4, "LawnGreen", false, "Holiday", null }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Record_activities",
                columns: new[] { "Guid_ID", "Activity_ID", "Description", "End_time", "Project_ID", "Shift_GuidID", "Start_time", "SubModule_ID", "TypeShift_ID" },
                values: new object[,]
                {
                    { new Guid("0342d781-30a9-4328-81f7-7d6e525c647b"), 3, "", null, null, null, new DateTime(2024, 11, 1, 15, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("0e07c845-c37e-4d65-b274-f90bf3f958ed"), 3, "", null, null, null, new DateTime(2024, 11, 2, 15, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("11f290da-514e-428e-b8cf-9ddb2e6df5ce"), 2, "", new DateTime(2024, 11, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 3, 11, 30, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("34095ca1-a950-46e6-89b6-1cf52014e61b"), 3, "", null, null, null, new DateTime(2024, 11, 4, 15, 10, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { new Guid("3d7502ca-8882-4609-b07d-9a446067410a"), 1, "", new DateTime(2024, 11, 3, 15, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("4409696d-3110-483b-8817-050b4f1d6573"), 1, "", new DateTime(2024, 11, 4, 11, 40, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 4, 7, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { new Guid("5bf3f7f5-24af-40dc-a9a4-9be608a9973e"), 1, "", new DateTime(2024, 11, 5, 16, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 3 },
                    { new Guid("5ea88384-31f1-422d-b086-77dedb4de3aa"), 1, "", new DateTime(2024, 11, 1, 11, 30, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("61d52308-c40d-42e5-abdb-dae7b3e04462"), 1, "", new DateTime(2024, 11, 2, 15, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 2, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("7183ca4c-6c37-4d1b-a24e-13437e2fe39f"), 2, "", new DateTime(2024, 11, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 1, 11, 30, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("72604f82-773f-4b97-8016-f1d46c1e856f"), 2, "", new DateTime(2024, 11, 4, 12, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 4, 11, 40, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { new Guid("82dbfb00-7da6-42c3-a943-14876f310779"), 3, "", null, null, null, new DateTime(2024, 11, 5, 16, 0, 0, 0, DateTimeKind.Unspecified), null, 3 },
                    { new Guid("83bbecee-704b-4803-a836-1702c4d6d37a"), 1, "", new DateTime(2024, 11, 3, 11, 30, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 3, 7, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("86ea1e80-8a5c-45b1-b3ca-bc95c9627048"), 1, "", new DateTime(2024, 11, 5, 11, 40, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), null, 3 },
                    { new Guid("8ca245d6-1e58-4848-a8fb-a7e7be622a35"), 1, "", new DateTime(2024, 11, 2, 11, 30, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 2, 7, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("a5a074d6-7b83-4aae-9a0b-18a9f7d001d0"), 1, "", new DateTime(2024, 11, 4, 15, 10, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 4, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { new Guid("aba71af2-2219-43c4-a513-9a7f94710153"), 1, "", new DateTime(2024, 11, 1, 15, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("ae4d84aa-dfdd-406b-bc48-cfa30e054d7d"), 2, "", new DateTime(2024, 11, 2, 12, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 2, 11, 30, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("c4c8e4c3-048d-42dd-b662-292c020fd8b3"), 2, "", new DateTime(2024, 11, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 5, 11, 40, 0, 0, DateTimeKind.Unspecified), null, 3 },
                    { new Guid("fb7d3f47-324d-4c98-9aff-c8e09d9cf7e1"), 3, "", null, null, null, new DateTime(2024, 11, 3, 15, 0, 0, 0, DateTimeKind.Unspecified), null, 1 }
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
