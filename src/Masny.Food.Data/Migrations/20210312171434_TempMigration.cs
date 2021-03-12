using Microsoft.EntityFrameworkCore.Migrations;

namespace Masny.Food.Data.Migrations
{
    public partial class TempMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryAddresses",
                schema: "user");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "user",
                table: "Profiles",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Kind",
                schema: "product",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Diameter",
                schema: "product",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                schema: "product",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                schema: "order",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);

            migrationBuilder.AddColumn<int>(
                name: "Payment",
                schema: "order",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                schema: "user",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                schema: "product",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Payment",
                schema: "order",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "Kind",
                schema: "product",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Diameter",
                schema: "product",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                schema: "order",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "DeliveryAddresses",
                schema: "user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Apartment = table.Column<int>(type: "int", nullable: true),
                    Entrance = table.Column<int>(type: "int", nullable: true),
                    Floor = table.Column<int>(type: "int", nullable: true),
                    Intercom = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryAddresses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAddresses_UserId",
                schema: "user",
                table: "DeliveryAddresses",
                column: "UserId");
        }
    }
}
