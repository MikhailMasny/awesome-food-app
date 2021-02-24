using Microsoft.EntityFrameworkCore.Migrations;

namespace Masny.Pizza.Data.Migrations
{
    public partial class UpdateFieldAtProductIngredientField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductIngredients_ProductDetails_ProductDetailId",
                table: "ProductIngredients");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductIngredients");

            migrationBuilder.AlterColumn<int>(
                name: "ProductDetailId",
                table: "ProductIngredients",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductIngredients_ProductDetails_ProductDetailId",
                table: "ProductIngredients",
                column: "ProductDetailId",
                principalTable: "ProductDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductIngredients_ProductDetails_ProductDetailId",
                table: "ProductIngredients");

            migrationBuilder.AlterColumn<int>(
                name: "ProductDetailId",
                table: "ProductIngredients",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductIngredients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductIngredients_ProductDetails_ProductDetailId",
                table: "ProductIngredients",
                column: "ProductDetailId",
                principalTable: "ProductDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
