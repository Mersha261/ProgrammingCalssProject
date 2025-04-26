using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PgrogrammingClass.Data.Migrations
{
    /// <inheritdoc />
    public partial class shoppingCartAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TblShoppingcart_AddressId",
                table: "TblShoppingcart",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblShoppingcart_TblUserAddress_AddressId",
                table: "TblShoppingcart",
                column: "AddressId",
                principalTable: "TblUserAddress",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblShoppingcart_TblUserAddress_AddressId",
                table: "TblShoppingcart");

            migrationBuilder.DropIndex(
                name: "IX_TblShoppingcart_AddressId",
                table: "TblShoppingcart");
        }
    }
}
