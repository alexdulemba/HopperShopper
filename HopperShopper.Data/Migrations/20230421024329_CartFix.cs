using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HopperShopper.Data.Migrations
{
    /// <inheritdoc />
    public partial class CartFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Carts_CartObjectID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CartObjectID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CartObjectID",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "CartProduct",
                columns: table => new
                {
                    CartsObjectID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductsObjectID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProduct", x => new { x.CartsObjectID, x.ProductsObjectID });
                    table.ForeignKey(
                        name: "FK_CartProduct_Carts_CartsObjectID",
                        column: x => x.CartsObjectID,
                        principalTable: "Carts",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartProduct_Products_ProductsObjectID",
                        column: x => x.ProductsObjectID,
                        principalTable: "Products",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartProduct_ProductsObjectID",
                table: "CartProduct",
                column: "ProductsObjectID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartProduct");

            migrationBuilder.AddColumn<Guid>(
                name: "CartObjectID",
                table: "Products",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CartObjectID",
                table: "Products",
                column: "CartObjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Carts_CartObjectID",
                table: "Products",
                column: "CartObjectID",
                principalTable: "Carts",
                principalColumn: "ObjectID");
        }
    }
}
