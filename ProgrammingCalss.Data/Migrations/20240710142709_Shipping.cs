using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PgrogrammingClass.Data.Migrations
{
    /// <inheritdoc />
    public partial class Shipping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShippingPrice",
                table: "TblShoppingcart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ShippingWayName",
                table: "TblShoppingcart",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingPrice",
                table: "TblShoppingcart");

            migrationBuilder.DropColumn(
                name: "ShippingWayName",
                table: "TblShoppingcart");
        }
    }
}
