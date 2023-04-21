using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HopperShopper.Data.Migrations
{
    /// <inheritdoc />
    public partial class DataGenerationUpdates1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentID",
                table: "PaymentMethods");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PaymentID",
                table: "PaymentMethods",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
