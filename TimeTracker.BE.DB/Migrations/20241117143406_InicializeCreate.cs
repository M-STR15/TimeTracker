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
                    End_DateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Project_ID = table.Column<int>(type: "INTEGER", nullable: true),
                    Shift_GuidID = table.Column<Guid>(type: "TEXT", nullable: true),
                    Start_DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SubModule_ID = table.Column<int>(type: "INTEGER", nullable: true),
                    TypeShift_ID = table.Column<int>(type: "INTEGER", nullable: true)
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
                columns: new[] { "Guid_ID", "Activity_ID", "Description", "End_DateTime", "Project_ID", "Shift_GuidID", "Start_DateTime", "SubModule_ID", "TypeShift_ID" },
                values: new object[,]
                {
                    { new Guid("1ddf6371-a203-4fcf-8939-3d19df766ea2"), 1, "", new DateTime(2024, 11, 1, 15, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("1e12e58c-ab12-4944-94af-4cb4f9164d55"), 3, "", null, null, null, new DateTime(2024, 11, 5, 16, 0, 0, 0, DateTimeKind.Unspecified), null, 3 },
                    { new Guid("237c6e29-3ae6-4189-9779-badece04410e"), 3, "", null, null, null, new DateTime(2024, 11, 3, 15, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("307e0d2d-abc2-40b3-9663-cd236f5c53db"), 1, "", new DateTime(2024, 11, 1, 11, 30, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("38eb2448-95c2-4843-93c9-e33f2bea0a9c"), 1, "", new DateTime(2024, 11, 2, 11, 30, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 2, 7, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("3c8f8cbb-4c10-4db3-a237-a68dafbcb4c7"), 2, "", new DateTime(2024, 11, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 3, 11, 30, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("3de6e8bf-c83c-482d-aa01-ca9588c4e256"), 3, "", null, null, null, new DateTime(2024, 11, 4, 15, 10, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { new Guid("3fddf2c2-54d2-4fd5-9294-ab7c58b6ddef"), 3, "", null, null, null, new DateTime(2024, 11, 1, 15, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("57422b84-6a3e-4742-88fe-661f1a8ea6e7"), 1, "", new DateTime(2024, 11, 4, 15, 10, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 4, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { new Guid("5e98e2d5-93b1-4f35-8eb9-d789660de4f8"), 2, "", new DateTime(2024, 11, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 1, 11, 30, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("6aca049f-10b4-4a18-96fa-d6a4859b7274"), 1, "", new DateTime(2024, 11, 3, 11, 30, 0, 0, DateTimeKind.Unspecified), 2, null, new DateTime(2024, 11, 3, 7, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("7854a46a-f9c7-48d6-a9d8-19a35ed407aa"), 2, "", new DateTime(2024, 11, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 5, 11, 40, 0, 0, DateTimeKind.Unspecified), null, 3 },
                    { new Guid("7b1e2806-fe98-4f9e-82f3-44b07681f43b"), 1, "", new DateTime(2024, 11, 3, 15, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("a90c2641-92f5-4986-a329-bf0fae490fa0"), 1, "", new DateTime(2024, 11, 2, 15, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 2, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("b3340d72-36d7-4638-b4e7-82c7ba3d05d1"), 2, "", new DateTime(2024, 11, 4, 12, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 4, 11, 40, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { new Guid("c3adb288-7f5c-46dd-8eba-54e972946586"), 1, "", new DateTime(2024, 11, 5, 16, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 3 },
                    { new Guid("cca64427-625d-4d11-9c1c-5143f2379776"), 3, "", null, null, null, new DateTime(2024, 11, 2, 15, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("e02c5199-6ceb-4678-9e53-af0a5b5a7e22"), 2, "", new DateTime(2024, 11, 2, 12, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 2, 11, 30, 0, 0, DateTimeKind.Unspecified), null, 1 }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Shifts",
                columns: new[] { "Guid_ID", "Description", "ShiftGuidId", "Start_date", "TypeShift_ID" },
                values: new object[,]
                {
                    { new Guid("31402227-1064-4455-872f-df218a85aca3"), null, null, new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { new Guid("68dbc51a-9546-4450-bf46-e614397021e4"), null, null, new DateTime(2024, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("865370c2-1115-4b23-b4c9-f7cdebbbe86d"), null, null, new DateTime(2024, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { new Guid("9ad8ec6d-6bd8-4cf5-b681-c46c86c508f3"), null, null, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { new Guid("a634ce0f-ab0c-4061-babb-70f656277fa1"), null, null, new DateTime(2024, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { new Guid("a9f6f060-c255-43e5-b5d5-e9d7860ab14d"), null, null, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("d10f4bf4-c1a4-404b-9213-803ad4cee509"), null, null, new DateTime(2024, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { new Guid("d434a034-f93d-4f68-a69a-60243a16d21d"), null, null, new DateTime(2024, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { new Guid("d917a220-5a2c-401c-90cc-c746aaada412"), null, null, new DateTime(2024, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { new Guid("fa1db3cb-f2c4-4efd-aee9-cd3487366229"), null, null, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "SubModule",
                columns: new[] { "SubModule_ID", "Description", "Name", "Project_ID" },
                values: new object[,]
                {
                    { 1, null, "SubModule 1", 1 },
                    { 2, null, "SubModule 2", 1 }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Record_activities",
                columns: new[] { "Guid_ID", "Activity_ID", "Description", "End_DateTime", "Project_ID", "Shift_GuidID", "Start_DateTime", "SubModule_ID", "TypeShift_ID" },
                values: new object[,]
                {
                    { new Guid("b1aeed02-a169-4d75-9cbf-17b15b810a12"), 1, "Test poznamky", new DateTime(2024, 11, 5, 11, 40, 0, 0, DateTimeKind.Unspecified), 1, new Guid("d434a034-f93d-4f68-a69a-60243a16d21d"), new DateTime(2024, 11, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), null, 3 },
                    { new Guid("d666ef9e-3635-45a9-b8f1-6011ef77a25c"), 1, "", new DateTime(2024, 11, 4, 11, 40, 0, 0, DateTimeKind.Unspecified), 1, null, new DateTime(2024, 11, 4, 7, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 }
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
