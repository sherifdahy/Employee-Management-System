using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.Migrations
{
    /// <inheritdoc />
    public partial class add_column_intrasaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyTransactions_Companies_CompanyId",
                table: "DailyTransactions");

            migrationBuilder.DropTable(
                name: "DailyTransactionTransactionItem");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "TransactionItem",
                newName: "Note");

            migrationBuilder.CreateSequence(
                name: "TransactionNumberSequence");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "TransactionItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DailyTransactionId",
                table: "TransactionItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TransactionItemCategoryId",
                table: "TransactionItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "DailyTransactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateOnly>(
                name: "TransactionDate",
                table: "DailyTransactions",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<long>(
                name: "TransactionNumber",
                table: "DailyTransactions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "TransactionItemCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionItemCategory", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 21, 7, 52, 40, 394, DateTimeKind.Utc).AddTicks(7684), new DateTime(2025, 8, 21, 7, 52, 40, 394, DateTimeKind.Utc).AddTicks(7686) });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionItem_CompanyId",
                table: "TransactionItem",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionItem_DailyTransactionId",
                table: "TransactionItem",
                column: "DailyTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionItem_TransactionItemCategoryId",
                table: "TransactionItem",
                column: "TransactionItemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyTransactions_TransactionNumber",
                table: "DailyTransactions",
                column: "TransactionNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DailyTransactions_Companies_CompanyId",
                table: "DailyTransactions",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionItem_Companies_CompanyId",
                table: "TransactionItem",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionItem_DailyTransactions_DailyTransactionId",
                table: "TransactionItem",
                column: "DailyTransactionId",
                principalTable: "DailyTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionItem_TransactionItemCategory_TransactionItemCategoryId",
                table: "TransactionItem",
                column: "TransactionItemCategoryId",
                principalTable: "TransactionItemCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyTransactions_Companies_CompanyId",
                table: "DailyTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionItem_Companies_CompanyId",
                table: "TransactionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionItem_DailyTransactions_DailyTransactionId",
                table: "TransactionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionItem_TransactionItemCategory_TransactionItemCategoryId",
                table: "TransactionItem");

            migrationBuilder.DropTable(
                name: "TransactionItemCategory");

            migrationBuilder.DropIndex(
                name: "IX_TransactionItem_CompanyId",
                table: "TransactionItem");

            migrationBuilder.DropIndex(
                name: "IX_TransactionItem_DailyTransactionId",
                table: "TransactionItem");

            migrationBuilder.DropIndex(
                name: "IX_TransactionItem_TransactionItemCategoryId",
                table: "TransactionItem");

            migrationBuilder.DropIndex(
                name: "IX_DailyTransactions_TransactionNumber",
                table: "DailyTransactions");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "TransactionItem");

            migrationBuilder.DropColumn(
                name: "DailyTransactionId",
                table: "TransactionItem");

            migrationBuilder.DropColumn(
                name: "TransactionItemCategoryId",
                table: "TransactionItem");

            migrationBuilder.DropColumn(
                name: "TransactionDate",
                table: "DailyTransactions");

            migrationBuilder.DropColumn(
                name: "TransactionNumber",
                table: "DailyTransactions");

            migrationBuilder.DropSequence(
                name: "TransactionNumberSequence");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "TransactionItem",
                newName: "Description");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "DailyTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DailyTransactionTransactionItem",
                columns: table => new
                {
                    DailyTransactionsId = table.Column<int>(type: "int", nullable: false),
                    TransactionItemsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyTransactionTransactionItem", x => new { x.DailyTransactionsId, x.TransactionItemsId });
                    table.ForeignKey(
                        name: "FK_DailyTransactionTransactionItem_DailyTransactions_DailyTransactionsId",
                        column: x => x.DailyTransactionsId,
                        principalTable: "DailyTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DailyTransactionTransactionItem_TransactionItem_TransactionItemsId",
                        column: x => x.TransactionItemsId,
                        principalTable: "TransactionItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 19, 18, 40, 55, 374, DateTimeKind.Utc).AddTicks(677), new DateTime(2025, 8, 19, 18, 40, 55, 374, DateTimeKind.Utc).AddTicks(679) });

            migrationBuilder.CreateIndex(
                name: "IX_DailyTransactionTransactionItem_TransactionItemsId",
                table: "DailyTransactionTransactionItem",
                column: "TransactionItemsId");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyTransactions_Companies_CompanyId",
                table: "DailyTransactions",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
