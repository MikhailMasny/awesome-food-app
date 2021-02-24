using Microsoft.EntityFrameworkCore.Migrations;

namespace Masny.Pizza.Data.Migrations
{
    public partial class RenameProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductIngredients_ProductTemps_ProductTempId",
                table: "ProductIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTemps_ProductTempId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductTemps");

            migrationBuilder.RenameColumn(
                name: "ProductTempId",
                table: "Products",
                newName: "ProductDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductTempId",
                table: "Products",
                newName: "IX_Products_ProductDetailId");

            migrationBuilder.RenameColumn(
                name: "ProductTempId",
                table: "ProductIngredients",
                newName: "ProductDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductIngredients_ProductTempId",
                table: "ProductIngredients",
                newName: "IX_ProductIngredients_ProductDetailId");

            migrationBuilder.CreateTable(
                name: "ProductDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetails", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductIngredients_ProductDetails_ProductDetailId",
                table: "ProductIngredients",
                column: "ProductDetailId",
                principalTable: "ProductDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductDetails_ProductDetailId",
                table: "Products",
                column: "ProductDetailId",
                principalTable: "ProductDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductIngredients_ProductDetails_ProductDetailId",
                table: "ProductIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductDetails_ProductDetailId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductDetails");

            migrationBuilder.RenameColumn(
                name: "ProductDetailId",
                table: "Products",
                newName: "ProductTempId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductDetailId",
                table: "Products",
                newName: "IX_Products_ProductTempId");

            migrationBuilder.RenameColumn(
                name: "ProductDetailId",
                table: "ProductIngredients",
                newName: "ProductTempId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductIngredients_ProductDetailId",
                table: "ProductIngredients",
                newName: "IX_ProductIngredients_ProductTempId");

            migrationBuilder.CreateTable(
                name: "ProductTemps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTemps", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductIngredients_ProductTemps_ProductTempId",
                table: "ProductIngredients",
                column: "ProductTempId",
                principalTable: "ProductTemps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTemps_ProductTempId",
                table: "Products",
                column: "ProductTempId",
                principalTable: "ProductTemps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
