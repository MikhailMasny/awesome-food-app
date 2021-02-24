using Microsoft.EntityFrameworkCore.Migrations;

namespace Masny.Pizza.Data.Migrations
{
    public partial class RenameTempTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDetails_Products_ProductId",
                table: "ProductDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductIngredients_Products_ProductId",
                table: "ProductIngredients");

            migrationBuilder.DropIndex(
                name: "IX_ProductIngredients_ProductId",
                table: "ProductIngredients");

            migrationBuilder.DropIndex(
                name: "IX_ProductDetails_ProductId",
                table: "ProductDetails");

            migrationBuilder.AddColumn<int>(
                name: "ProductTempId",
                table: "ProductIngredients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductTempId",
                table: "ProductDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductIngredients_ProductTempId",
                table: "ProductIngredients",
                column: "ProductTempId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_ProductTempId",
                table: "ProductDetails",
                column: "ProductTempId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetails_Products_ProductTempId",
                table: "ProductDetails",
                column: "ProductTempId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductIngredients_Products_ProductTempId",
                table: "ProductIngredients",
                column: "ProductTempId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDetails_Products_ProductTempId",
                table: "ProductDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductIngredients_Products_ProductTempId",
                table: "ProductIngredients");

            migrationBuilder.DropIndex(
                name: "IX_ProductIngredients_ProductTempId",
                table: "ProductIngredients");

            migrationBuilder.DropIndex(
                name: "IX_ProductDetails_ProductTempId",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "ProductTempId",
                table: "ProductIngredients");

            migrationBuilder.DropColumn(
                name: "ProductTempId",
                table: "ProductDetails");

            migrationBuilder.CreateIndex(
                name: "IX_ProductIngredients_ProductId",
                table: "ProductIngredients",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_ProductId",
                table: "ProductDetails",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetails_Products_ProductId",
                table: "ProductDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductIngredients_Products_ProductId",
                table: "ProductIngredients",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
