using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.Migrations
{
    /// <inheritdoc />
    public partial class sadasdasdasd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "TransactionNumberSequence");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionNumber",
                table: "DailyTransactions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR TransactionNumberSequence",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 21, 16, 38, 18, 531, DateTimeKind.Utc).AddTicks(7977), new DateTime(2025, 8, 21, 16, 38, 18, 531, DateTimeKind.Utc).AddTicks(7979) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "TransactionNumberSequence");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionNumber",
                table: "DailyTransactions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldDefaultValueSql: "NEXT VALUE FOR TransactionNumberSequence");

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 21, 16, 32, 9, 84, DateTimeKind.Utc).AddTicks(9719), new DateTime(2025, 8, 21, 16, 32, 9, 84, DateTimeKind.Utc).AddTicks(9721) });
        }
    }
}
