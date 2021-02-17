using Microsoft.EntityFrameworkCore.Migrations;

namespace Masny.Pizza.Data.Migrations
{
    public partial class ChangeFieldsInProductAndIngredientTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Diameter",
                table: "Products");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Ingredients",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Ingredients");

            migrationBuilder.AddColumn<int>(
                name: "Diameter",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
