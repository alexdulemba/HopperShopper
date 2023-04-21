using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HopperShopper.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovePaymentMethodType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentMethodsTypes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentMethodsTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PaymentMethodObjectID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethodsTypes_PaymentMethodObjectID",
                table: "PaymentMethodsTypes",
                column: "PaymentMethodObjectID",
                unique: true);
        }
    }
}
