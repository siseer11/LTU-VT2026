using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorageApi.Migrations
{
    /// <inheritdoc />
    public partial class ProductExpirationDateAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "ExpirationDate",
                table: "Products",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Products");
        }
    }
}
