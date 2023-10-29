using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthyFoods.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_ImportedFrom_field_to_Food_and_Item : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImportedFrom",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImportedFrom",
                table: "Foods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImportedFrom",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ImportedFrom",
                table: "Foods");
        }
    }
}
