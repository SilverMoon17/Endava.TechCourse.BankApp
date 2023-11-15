using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Endava.TechCourse.BankApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("62df903b-c59b-47c7-b553-ec0f4b13a5b6"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("83bcf2a6-7f21-4abd-bbe0-66d4c404e1e2"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("81ac8aef-2025-4c04-ae2c-6ab35e43e350"), null, "Admin", "Admin" },
                    { new Guid("8f77b26c-ebbd-4df1-bd13-e752e95738d4"), null, "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("81ac8aef-2025-4c04-ae2c-6ab35e43e350"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8f77b26c-ebbd-4df1-bd13-e752e95738d4"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("62df903b-c59b-47c7-b553-ec0f4b13a5b6"), null, "User", "USER" },
                    { new Guid("83bcf2a6-7f21-4abd-bbe0-66d4c404e1e2"), null, "Admin", "ADMIN" }
                });
        }
    }
}
