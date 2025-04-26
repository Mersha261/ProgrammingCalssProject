using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PgrogrammingClass.Data.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Tblproduct",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "TblShoppingcart",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Cookie = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    IsSentToUser = table.Column<bool>(type: "bit", nullable: false),
                    PayDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPaied = table.Column<bool>(type: "bit", nullable: false),
                    IsSentToBank = table.Column<bool>(type: "bit", nullable: false),
                    TotalPrice = table.Column<int>(type: "int", nullable: false),
                    OffCopon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PriceWithoutOff = table.Column<int>(type: "int", nullable: false),
                    OffPercent = table.Column<int>(type: "int", nullable: false),
                    TransactionNumber = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CustomOrderNumber = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    IsCoponSet = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblShoppingcart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblShoppingCartDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ShoppingCartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    PriceWithOffCopon = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblShoppingCartDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblUserAddress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Family = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblUserAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblUserAddress_TblCity_CityId",
                        column: x => x.CityId,
                        principalTable: "TblCity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblUserAddress_CityId",
                table: "TblUserAddress",
                column: "CityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblShoppingcart");

            migrationBuilder.DropTable(
                name: "TblShoppingCartDetails");

            migrationBuilder.DropTable(
                name: "TblUserAddress");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Tblproduct");
        }
    }
}
