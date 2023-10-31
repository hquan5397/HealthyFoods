using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthyFoods.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_OriginalPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Items",
                newName: "PricePerEach");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Foods",
                newName: "PricePerKg");

            migrationBuilder.AddColumn<double>(
                name: "OriginalPrice",
                table: "Items",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "OriginalPrice",
                table: "Foods",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalPrice",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "OriginalPrice",
                table: "Foods");

            migrationBuilder.RenameColumn(
                name: "PricePerEach",
                table: "Items",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "PricePerKg",
                table: "Foods",
                newName: "Price");
        }
    }
}
