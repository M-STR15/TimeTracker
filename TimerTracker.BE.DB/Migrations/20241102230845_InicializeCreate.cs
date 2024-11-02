using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TimerTracker.BE.DB.Migrations
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
                    { new Guid("04bd8683-4063-4b1b-9396-9441aee0d0f6"), 1, "", null, null, new DateTime(2024, 10, 4, 7, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { new Guid("2e8aed50-a704-4b13-bd2d-2c8eb526ec61"), 1, "", null, null, new DateTime(2024, 10, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), null, 3 },
                    { new Guid("433b4608-b315-473b-a6a6-ecd63c216361"), 1, "", null, null, new DateTime(2024, 10, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 3 },
                    { new Guid("4af68c55-abf0-4b6e-a4ab-65b10281d36f"), 1, "", null, null, new DateTime(2024, 10, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("65ee1ccb-a136-484d-8aef-d3a7f983c21a"), 1, "", null, null, new DateTime(2024, 10, 2, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("6c612a12-b523-4a7a-b59c-c59ea9f306c0"), 2, "", null, null, new DateTime(2024, 10, 2, 11, 30, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("7b6c8911-843b-4b06-bc01-21ef80b6aa2f"), 2, "", null, null, new DateTime(2024, 10, 5, 11, 40, 0, 0, DateTimeKind.Unspecified), null, 3 },
                    { new Guid("7ef303ad-b6b5-442c-9742-d4903da5db2f"), 1, "", null, null, new DateTime(2024, 10, 3, 7, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("82cc4892-3682-48a3-9773-cbfa11d38310"), 3, "", null, null, new DateTime(2024, 10, 1, 15, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("9143c640-ff6a-4acb-a908-ca7d8d209a57"), 3, "", null, null, new DateTime(2024, 10, 5, 16, 0, 0, 0, DateTimeKind.Unspecified), null, 3 },
                    { new Guid("936c2b4d-3382-45f0-9c79-487080e28a08"), 1, "", null, null, new DateTime(2024, 10, 2, 7, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("a03ccd74-125d-4282-9b9e-3b2f425d75c6"), 1, "", null, null, new DateTime(2024, 10, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("a302ef5d-8f28-4407-afe6-7fefcc194463"), 2, "", null, null, new DateTime(2024, 10, 1, 11, 30, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("ad53fd53-79b0-41a2-b196-1bb3364c82f0"), 2, "", null, null, new DateTime(2024, 10, 3, 11, 30, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("ad9df005-80bb-4cb3-8bb8-fe0b54c7e096"), 1, "", null, null, new DateTime(2024, 10, 4, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { new Guid("afc35f51-5e63-402d-ac7a-fcc86ffb2199"), 3, "", null, null, new DateTime(2024, 10, 3, 15, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("c007224f-0b77-455d-8392-8c823e1ed707"), 1, "", null, null, new DateTime(2024, 10, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("c0fb140b-e16b-47bf-a849-12b4c6a37487"), 3, "", null, null, new DateTime(2024, 10, 2, 15, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("c374fe89-5171-401e-88e1-93a151d4fcfd"), 2, "", null, null, new DateTime(2024, 10, 4, 11, 40, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { new Guid("e995c47c-4ce1-4759-91b3-bd86a3167337"), 3, "", null, null, new DateTime(2024, 10, 4, 15, 10, 0, 0, DateTimeKind.Unspecified), null, 2 }
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
