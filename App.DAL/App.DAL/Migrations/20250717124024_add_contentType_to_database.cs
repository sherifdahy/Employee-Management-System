using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.Migrations
{
    /// <inheritdoc />
    public partial class add_contentType_to_database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Selector",
                newName: "selectorType");

            migrationBuilder.AddColumn<byte>(
                name: "contentType",
                table: "Selector",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "contentType",
                table: "Selector");

            migrationBuilder.RenameColumn(
                name: "selectorType",
                table: "Selector",
                newName: "Type");
        }
    }
}
