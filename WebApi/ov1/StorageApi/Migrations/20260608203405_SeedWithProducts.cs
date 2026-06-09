using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StorageApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedWithProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Count", "Description", "ExpirationDate", "Name", "Price", "Shelf" },
                values: new object[,]
                {
                    { 1, "Electronics", 1, "Best keyboard out there, get it now!", null, "Keyboard", 120, null },
                    { 2, "Food", null, null, null, "Salami", 3, null },
                    { 3, "Frozen Food", null, null, null, "Frozen Pizza", 5, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
