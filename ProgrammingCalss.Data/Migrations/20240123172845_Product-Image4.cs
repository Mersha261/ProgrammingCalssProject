using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PgrogrammingClass.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductImage4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TblProductImage_ProductId",
                table: "TblProductImage",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblProductImage_TblProduct_ProductId",
                table: "TblProductImage",
                column: "ProductId",
                principalTable: "TblProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblProductImage_TblProduct_ProductId",
                table: "TblProductImage");

            migrationBuilder.DropIndex(
                name: "IX_TblProductImage_ProductId",
                table: "TblProductImage");
        }
    }
}
