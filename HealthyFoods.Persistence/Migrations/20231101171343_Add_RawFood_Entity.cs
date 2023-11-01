using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthyFoods.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_RawFood_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Foods");

            migrationBuilder.AddColumn<Guid>(
                name: "RawFoodId",
                table: "Foods",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "RawFood",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawFood", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Foods_RawFoodId",
                table: "Foods",
                column: "RawFoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_RawFood_RawFoodId",
                table: "Foods",
                column: "RawFoodId",
                principalTable: "RawFood",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_RawFood_RawFoodId",
                table: "Foods");

            migrationBuilder.DropTable(
                name: "RawFood");

            migrationBuilder.DropIndex(
                name: "IX_Foods_RawFoodId",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "RawFoodId",
                table: "Foods");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Foods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
