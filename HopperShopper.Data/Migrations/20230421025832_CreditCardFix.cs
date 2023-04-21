using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HopperShopper.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreditCardFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_PaymentMethodsTypes_PaymentMethodTypeId",
                table: "CreditCards");

            migrationBuilder.DropIndex(
                name: "IX_CreditCards_PaymentMethodTypeId",
                table: "CreditCards");

            migrationBuilder.DropColumn(
                name: "PaymentMethodTypeId",
                table: "CreditCards");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodTypeId",
                table: "CreditCards",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_PaymentMethodTypeId",
                table: "CreditCards",
                column: "PaymentMethodTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCards_PaymentMethodsTypes_PaymentMethodTypeId",
                table: "CreditCards",
                column: "PaymentMethodTypeId",
                principalTable: "PaymentMethodsTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
