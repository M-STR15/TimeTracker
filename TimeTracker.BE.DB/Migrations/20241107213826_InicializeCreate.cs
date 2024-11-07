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
                    { 3, "Magenta", true, "Others", null },
                    { 4, "LawnGreen", false, "Holiday", null }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Record_activities",
                columns: new[] { "Guid_ID", "Activity_ID", "Description", "Project_ID", "Shift_GuidID", "Start_time", "SubModule_ID", "TypeShift_ID" },
                values: new object[,]
                {
                    { new Guid("0047df3a-8f44-4be7-8219-b65a71a64e10"), 1, "", null, null, new DateTime(2024, 10, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("02e27805-bc99-4bef-b240-9660a8ab7ff4"), 2, "", null, null, new DateTime(2024, 10, 1, 11, 30, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("0768222d-92c1-4d46-88a9-3dafb0d7a0dd"), 2, "", null, null, new DateTime(2024, 10, 3, 11, 30, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("0a329565-902b-4e60-9c0f-c6bf3704c222"), 1, "", null, null, new DateTime(2024, 10, 2, 7, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("29710dea-aea5-4478-93a4-46af0c50d03e"), 1, "", null, null, new DateTime(2024, 10, 2, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("30ad5a10-1d16-4528-8298-77c997d95a1d"), 1, "", null, null, new DateTime(2024, 10, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("342fc221-8620-4616-8a3c-955d05e70f5c"), 3, "", null, null, new DateTime(2024, 10, 1, 15, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("54994f49-1959-4413-81c4-c0f8f70182ce"), 1, "", null, null, new DateTime(2024, 10, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 3 },
                    { new Guid("5de33f7b-7709-4cf6-9720-9e4f6af70cd3"), 2, "", null, null, new DateTime(2024, 10, 5, 11, 40, 0, 0, DateTimeKind.Unspecified), null, 3 },
                    { new Guid("61b42096-a2f4-4110-a1c4-9644a80d0e35"), 1, "", null, null, new DateTime(2024, 10, 3, 7, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("6dcc239c-226e-4107-936e-2b8017d03c8a"), 2, "", null, null, new DateTime(2024, 10, 4, 11, 40, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { new Guid("7c57b418-021f-4839-8eb2-f24296a2c973"), 3, "", null, null, new DateTime(2024, 10, 4, 15, 10, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { new Guid("a3dfebe2-c033-4172-8d87-d2915a01d49a"), 3, "", null, null, new DateTime(2024, 10, 5, 16, 0, 0, 0, DateTimeKind.Unspecified), null, 3 },
                    { new Guid("a77d1158-9167-4852-ae58-33377d6c709e"), 3, "", null, null, new DateTime(2024, 10, 3, 15, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("aba18402-302f-4aa0-9e65-7b8ef8b8458f"), 3, "", null, null, new DateTime(2024, 10, 2, 15, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("ad594748-cd36-48aa-9a4f-1f21ec2c29f5"), 1, "", null, null, new DateTime(2024, 10, 4, 7, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { new Guid("aeb0dc69-e3d4-4b76-92ea-3256df6c3e98"), 1, "", null, null, new DateTime(2024, 10, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("bf778a33-99ec-4077-9956-7a061f484eac"), 1, "", null, null, new DateTime(2024, 10, 4, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { new Guid("c8b7e51c-45ee-4ed1-beb0-a4986b068d36"), 2, "", null, null, new DateTime(2024, 10, 2, 11, 30, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("f8298862-01d0-4d8d-b9fe-1ebe89961915"), 1, "", null, null, new DateTime(2024, 10, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), null, 3 }
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
