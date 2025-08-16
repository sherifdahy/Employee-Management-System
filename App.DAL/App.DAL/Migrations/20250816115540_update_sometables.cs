using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.Migrations
{
    /// <inheritdoc />
    public partial class update_sometables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: new Guid("5d158b89-2e6d-4d57-953e-d11e881ee6c8"));

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Selectors");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Selectors");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Accounts");

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "CreatedAt", "Email", "IsDeleted", "Name", "Password", "UpdatedAt", "UserType" },
                values: new object[] { new Guid("39fa7df8-74b1-4444-a067-20fe8d6fd2a7"), new DateTime(2025, 8, 16, 11, 55, 39, 5, DateTimeKind.Utc).AddTicks(2908), "admin", false, "Sherif Dahy", "G2Po4Wgp2rqN2Aflcd61PwfgSPy8v0D37XXNFFZzhWk=", new DateTime(2025, 8, 16, 11, 55, 39, 5, DateTimeKind.Utc).AddTicks(2910), 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: new Guid("39fa7df8-74b1-4444-a067-20fe8d6fd2a7"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Selectors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Selectors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "CreatedAt", "Email", "IsDeleted", "Name", "Password", "UpdatedAt", "UserType" },
                values: new object[] { new Guid("5d158b89-2e6d-4d57-953e-d11e881ee6c8"), new DateTime(2025, 8, 15, 14, 13, 29, 10, DateTimeKind.Utc).AddTicks(7519), "admin", false, "Sherif Dahy", "G2Po4Wgp2rqN2Aflcd61PwfgSPy8v0D37XXNFFZzhWk=", new DateTime(2025, 8, 15, 14, 13, 29, 10, DateTimeKind.Utc).AddTicks(7521), 2 });
        }
    }
}
