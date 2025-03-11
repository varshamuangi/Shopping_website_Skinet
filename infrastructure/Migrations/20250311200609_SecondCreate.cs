using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SecondCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductType",
                table: "Products",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "ProductBrand",
                table: "Products",
                newName: "Brand");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Products",
                newName: "ProductType");

            migrationBuilder.RenameColumn(
                name: "Brand",
                table: "Products",
                newName: "ProductBrand");
        }
    }
}
