using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedClassesandRankingdatas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0d06acb7-5ad4-458c-b26e-a3b390994335"), "Object Oriented Programming" },
                    { new Guid("0d06acb7-5ad4-458c-b26e-a3b390994447"), "Calculus" },
                    { new Guid("0d06acb7-5ad4-458c-b26e-a3b390995678"), "Data Structure and Algorythm" },
                    { new Guid("237b0357-dfb0-49e9-90a9-0a25933c439e"), "Introduction to programming" },
                    { new Guid("5c47bab3-293b-4b64-8860-bdeb1516ed43"), "Web Programming and Implementation" },
                    { new Guid("69e61e7e-6b80-4cde-b016-3a253e9a0c45"), "Java Programming" }
                });

            migrationBuilder.InsertData(
                table: "Rankings",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("044677d4-ec73-4c97-a007-86530e9c0769"), "Needs more work" },
                    { new Guid("b9de3c6d-55e8-4857-9e8e-87a8c35f5ae0"), "Brilliant" },
                    { new Guid("df1228e3-b8e6-4bd5-95a3-5b54e19b3a88"), "Average" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("0d06acb7-5ad4-458c-b26e-a3b390994335"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("0d06acb7-5ad4-458c-b26e-a3b390994447"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("0d06acb7-5ad4-458c-b26e-a3b390995678"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("237b0357-dfb0-49e9-90a9-0a25933c439e"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("5c47bab3-293b-4b64-8860-bdeb1516ed43"));

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: new Guid("69e61e7e-6b80-4cde-b016-3a253e9a0c45"));

            migrationBuilder.DeleteData(
                table: "Rankings",
                keyColumn: "Id",
                keyValue: new Guid("044677d4-ec73-4c97-a007-86530e9c0769"));

            migrationBuilder.DeleteData(
                table: "Rankings",
                keyColumn: "Id",
                keyValue: new Guid("b9de3c6d-55e8-4857-9e8e-87a8c35f5ae0"));

            migrationBuilder.DeleteData(
                table: "Rankings",
                keyColumn: "Id",
                keyValue: new Guid("df1228e3-b8e6-4bd5-95a3-5b54e19b3a88"));
        }
    }
}
