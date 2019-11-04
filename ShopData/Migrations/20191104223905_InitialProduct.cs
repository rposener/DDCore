using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopData.Migrations
{
    public partial class InitialProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    _productId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x._productId);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    _reviewId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_productId = table.Column<int>(nullable: true),
                    Product_productId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x._reviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Products_Product_productId",
                        column: x => x.Product_productId,
                        principalTable: "Products",
                        principalColumn: "_productId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Products_Product_productId1",
                        column: x => x.Product_productId1,
                        principalTable: "Products",
                        principalColumn: "_productId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_Product_productId",
                table: "Reviews",
                column: "Product_productId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_Product_productId1",
                table: "Reviews",
                column: "Product_productId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
