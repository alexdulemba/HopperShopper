using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HopperShopper.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    ObjectID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.ObjectID);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    ObjectID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.ObjectID);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    ObjectID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Subtotal = table.Column<float>(type: "REAL", nullable: false),
                    CustomerObjectID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_Carts_Customers_CustomerObjectID",
                        column: x => x.CustomerObjectID,
                        principalTable: "Customers",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerSearchHistory",
                columns: table => new
                {
                    ObjectID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    TimeEntered = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CustomerObjectID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSearchHistory", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_CustomerSearchHistory_Customers_CustomerObjectID",
                        column: x => x.CustomerObjectID,
                        principalTable: "Customers",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ObjectID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Total = table.Column<float>(type: "REAL", nullable: false),
                    DatePlaced = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CustomerObjectID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerObjectID",
                        column: x => x.CustomerObjectID,
                        principalTable: "Customers",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    ObjectID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDefault = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomerObjectID = table.Column<Guid>(type: "TEXT", nullable: false),
                    PaymentID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_PaymentMethods_Customers_CustomerObjectID",
                        column: x => x.CustomerObjectID,
                        principalTable: "Customers",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ObjectID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<float>(type: "REAL", nullable: false),
                    CartObjectID = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_Products_Carts_CartObjectID",
                        column: x => x.CartObjectID,
                        principalTable: "Carts",
                        principalColumn: "ObjectID");
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethodsTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    PaymentMethodObjectID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethodsTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentMethodsTypes_PaymentMethods_PaymentMethodObjectID",
                        column: x => x.PaymentMethodObjectID,
                        principalTable: "PaymentMethods",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProduct",
                columns: table => new
                {
                    OrdersObjectID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductsObjectID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProduct", x => new { x.OrdersObjectID, x.ProductsObjectID });
                    table.ForeignKey(
                        name: "FK_OrderProduct_Orders_OrdersObjectID",
                        column: x => x.OrdersObjectID,
                        principalTable: "Orders",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProduct_Products_ProductsObjectID",
                        column: x => x.ProductsObjectID,
                        principalTable: "Products",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductProductCategory",
                columns: table => new
                {
                    CategoriesObjectID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductsObjectID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductCategory", x => new { x.CategoriesObjectID, x.ProductsObjectID });
                    table.ForeignKey(
                        name: "FK_ProductProductCategory_ProductCategories_CategoriesObjectID",
                        column: x => x.CategoriesObjectID,
                        principalTable: "ProductCategories",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProductCategory_Products_ProductsObjectID",
                        column: x => x.ProductsObjectID,
                        principalTable: "Products",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    ObjectID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    CardholderName = table.Column<string>(type: "TEXT", nullable: false),
                    AccountNumber = table.Column<string>(type: "TEXT", nullable: false),
                    CCV = table.Column<string>(type: "TEXT", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PaymentMethodTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    PaymentMethodObjectID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.ObjectID);
                    table.ForeignKey(
                        name: "FK_CreditCards_PaymentMethodsTypes_PaymentMethodTypeId",
                        column: x => x.PaymentMethodTypeId,
                        principalTable: "PaymentMethodsTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreditCards_PaymentMethods_PaymentMethodObjectID",
                        column: x => x.PaymentMethodObjectID,
                        principalTable: "PaymentMethods",
                        principalColumn: "ObjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CustomerObjectID",
                table: "Carts",
                column: "CustomerObjectID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_PaymentMethodObjectID",
                table: "CreditCards",
                column: "PaymentMethodObjectID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_PaymentMethodTypeId",
                table: "CreditCards",
                column: "PaymentMethodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSearchHistory_CustomerObjectID",
                table: "CustomerSearchHistory",
                column: "CustomerObjectID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_ProductsObjectID",
                table: "OrderProduct",
                column: "ProductsObjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerObjectID",
                table: "Orders",
                column: "CustomerObjectID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_CustomerObjectID",
                table: "PaymentMethods",
                column: "CustomerObjectID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethodsTypes_PaymentMethodObjectID",
                table: "PaymentMethodsTypes",
                column: "PaymentMethodObjectID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductCategory_ProductsObjectID",
                table: "ProductProductCategory",
                column: "ProductsObjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CartObjectID",
                table: "Products",
                column: "CartObjectID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropTable(
                name: "CustomerSearchHistory");

            migrationBuilder.DropTable(
                name: "OrderProduct");

            migrationBuilder.DropTable(
                name: "ProductProductCategory");

            migrationBuilder.DropTable(
                name: "PaymentMethodsTypes");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
