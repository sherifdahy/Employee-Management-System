using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionItem_TransactionItemCategory_TransactionItemCategoryId",
                table: "TransactionItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionItemCategory",
                table: "TransactionItemCategory");

            migrationBuilder.DropSequence(
                name: "TransactionNumberSequence");

            migrationBuilder.RenameTable(
                name: "TransactionItemCategory",
                newName: "TransactionItemCategories");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TransactionDate",
                table: "DailyTransactions",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TransactionItemCategories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionItemCategories",
                table: "TransactionItemCategories",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 21, 16, 32, 9, 84, DateTimeKind.Utc).AddTicks(9719), new DateTime(2025, 8, 21, 16, 32, 9, 84, DateTimeKind.Utc).AddTicks(9721) });

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionItem_TransactionItemCategories_TransactionItemCategoryId",
                table: "TransactionItem",
                column: "TransactionItemCategoryId",
                principalTable: "TransactionItemCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionItem_TransactionItemCategories_TransactionItemCategoryId",
                table: "TransactionItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionItemCategories",
                table: "TransactionItemCategories");

            migrationBuilder.RenameTable(
                name: "TransactionItemCategories",
                newName: "TransactionItemCategory");

            migrationBuilder.CreateSequence(
                name: "TransactionNumberSequence");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "TransactionDate",
                table: "DailyTransactions",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "TransactionItemCategory",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionItemCategory",
                table: "TransactionItemCategory",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 21, 9, 53, 52, 55, DateTimeKind.Utc).AddTicks(8567), new DateTime(2025, 8, 21, 9, 53, 52, 55, DateTimeKind.Utc).AddTicks(8569) });

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionItem_TransactionItemCategory_TransactionItemCategoryId",
                table: "TransactionItem",
                column: "TransactionItemCategoryId",
                principalTable: "TransactionItemCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
