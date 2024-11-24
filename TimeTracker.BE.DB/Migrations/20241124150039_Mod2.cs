using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TimeTracker.BE.DB.Migrations
{
    /// <inheritdoc />
    public partial class Mod2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Record_activities",
                keyColumn: "Guid_ID",
                keyValue: new Guid("1ddf6371-a203-4fcf-8939-3d19df766ea2"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Record_activities",
                keyColumn: "Guid_ID",
                keyValue: new Guid("1e12e58c-ab12-4944-94af-4cb4f9164d55"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Record_activities",
                keyColumn: "Guid_ID",
                keyValue: new Guid("237c6e29-3ae6-4189-9779-badece04410e"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Record_activities",
                keyColumn: "Guid_ID",
                keyValue: new Guid("307e0d2d-abc2-40b3-9663-cd236f5c53db"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Record_activities",
                keyColumn: "Guid_ID",
                keyValue: new Guid("38eb2448-95c2-4843-93c9-e33f2bea0a9c"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Record_activities",
                keyColumn: "Guid_ID",
                keyValue: new Guid("3c8f8cbb-4c10-4db3-a237-a68dafbcb4c7"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Record_activities",
                keyColumn: "Guid_ID",
                keyValue: new Guid("3de6e8bf-c83c-482d-aa01-ca9588c4e256"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Record_activities",
                keyColumn: "Guid_ID",
                keyValue: new Guid("3fddf2c2-54d2-4fd5-9294-ab7c58b6ddef"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Record_activities",
                keyColumn: "Guid_ID",
                keyValue: new Guid("57422b84-6a3e-4742-88fe-661f1a8ea6e7"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Record_activities",
                keyColumn: "Guid_ID",
                keyValue: new Guid("5e98e2d5-93b1-4f35-8eb9-d789660de4f8"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Record_activities",
                keyColumn: "Guid_ID",
                keyValue: new Guid("6aca049f-10b4-4a18-96fa-d6a4859b7274"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Record_activities",
                keyColumn: "Guid_ID",
                keyValue: new Guid("7854a46a-f9c7-48d6-a9d8-19a35ed407aa"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Record_activities",
                keyColumn: "Guid_ID",
                keyValue: new Guid("7b1e2806-fe98-4f9e-82f3-44b07681f43b"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Record_activities",
                keyColumn: "Guid_ID",
                keyValue: new Guid("a90c2641-92f5-4986-a329-bf0fae490fa0"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Record_activities",
                keyColumn: "Guid_ID",
                keyValue: new Guid("b1aeed02-a169-4d75-9cbf-17b15b810a12"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Record_activities",
                keyColumn: "Guid_ID",
                keyValue: new Guid("b3340d72-36d7-4638-b4e7-82c7ba3d05d1"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Record_activities",
                keyColumn: "Guid_ID",
                keyValue: new Guid("c3adb288-7f5c-46dd-8eba-54e972946586"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Record_activities",
                keyColumn: "Guid_ID",
                keyValue: new Guid("cca64427-625d-4d11-9c1c-5143f2379776"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Record_activities",
                keyColumn: "Guid_ID",
                keyValue: new Guid("d666ef9e-3635-45a9-b8f1-6011ef77a25c"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Record_activities",
                keyColumn: "Guid_ID",
                keyValue: new Guid("e02c5199-6ceb-4678-9e53-af0a5b5a7e22"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                    { new Guid("b1aeed02-a169-4d75-9cbf-17b15b810a12"), 1, "Test poznamky", new DateTime(2024, 11, 5, 11, 40, 0, 0, DateTimeKind.Unspecified), 1, new Guid("d434a034-f93d-4f68-a69a-60243a16d21d"), new DateTime(2024, 11, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), null, 3 },
                    { new Guid("b3340d72-36d7-4638-b4e7-82c7ba3d05d1"), 2, "", new DateTime(2024, 11, 4, 12, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 4, 11, 40, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { new Guid("c3adb288-7f5c-46dd-8eba-54e972946586"), 1, "", new DateTime(2024, 11, 5, 16, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 3 },
                    { new Guid("cca64427-625d-4d11-9c1c-5143f2379776"), 3, "", null, null, null, new DateTime(2024, 11, 2, 15, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { new Guid("d666ef9e-3635-45a9-b8f1-6011ef77a25c"), 1, "", new DateTime(2024, 11, 4, 11, 40, 0, 0, DateTimeKind.Unspecified), 1, null, new DateTime(2024, 11, 4, 7, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 },
                    { new Guid("e02c5199-6ceb-4678-9e53-af0a5b5a7e22"), 2, "", new DateTime(2024, 11, 2, 12, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2024, 11, 2, 11, 30, 0, 0, DateTimeKind.Unspecified), null, 1 }
                });
        }
    }
}
