using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PgrogrammingClass.Data.Migrations
{
    /// <inheritdoc />
    public partial class shoppingCartRealtion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ShoppingCartId",
                table: "TblShoppingCartDetails",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_TblShoppingCartDetails_ShoppingCartId",
                table: "TblShoppingCartDetails",
                column: "ShoppingCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblShoppingCartDetails_TblShoppingcart_ShoppingCartId",
                table: "TblShoppingCartDetails",
                column: "ShoppingCartId",
                principalTable: "TblShoppingcart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblShoppingCartDetails_TblShoppingcart_ShoppingCartId",
                table: "TblShoppingCartDetails");

            migrationBuilder.DropIndex(
                name: "IX_TblShoppingCartDetails_ShoppingCartId",
                table: "TblShoppingCartDetails");

            migrationBuilder.AlterColumn<Guid>(
                name: "ShoppingCartId",
                table: "TblShoppingCartDetails",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);
        }
    }
}
