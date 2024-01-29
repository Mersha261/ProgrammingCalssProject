using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PgrogrammingClass.Data.Migrations
{
    /// <inheritdoc />
    public partial class AllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblProductImage_TblProduct_ProductId",
                table: "TblProductImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TblProduct",
                table: "TblProduct");

            migrationBuilder.RenameTable(
                name: "TblProduct",
                newName: "Tblproduct");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Tblproduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tblproduct",
                table: "Tblproduct",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TblAboutUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AboutUs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblAboutUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    MetaKeywords = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MetaDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MetaTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: false),
                    IsIncludeInTopMenu = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblContactUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAndFamily = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserIp = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblContactUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblPost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InZeroTooFiveHunderedPishtaz = table.Column<int>(type: "int", nullable: false),
                    InZeroTooFiveHunderedSefareshi = table.Column<int>(type: "int", nullable: false),
                    InFiveHondredToThousandPishtaz = table.Column<int>(type: "int", nullable: false),
                    InFiveHondredToThousandSefareshi = table.Column<int>(type: "int", nullable: false),
                    InOnetoTowThousandPishtaz = table.Column<int>(type: "int", nullable: false),
                    InOnetoTowThousandSefareshi = table.Column<int>(type: "int", nullable: false),
                    InTowtoFiveThousandSefareshi = table.Column<int>(type: "int", nullable: false),
                    InTowtoThreeThousandPishtaz = table.Column<int>(type: "int", nullable: false),
                    InThreetoFourThousandPishtaz = table.Column<int>(type: "int", nullable: false),
                    InFourtoFiveThousandPishtaz = table.Column<int>(type: "int", nullable: false),
                    InPerKiloSefareshi = table.Column<int>(type: "int", nullable: false),
                    InPerKiloPishtaz = table.Column<int>(type: "int", nullable: false),
                    OutZeroTooFiveHunderedPishtaz = table.Column<int>(type: "int", nullable: false),
                    OutZeroTooFiveHunderedSefareshi = table.Column<int>(type: "int", nullable: false),
                    OutFiveHondredToThousandPishtaz = table.Column<int>(type: "int", nullable: false),
                    OutFiveHondredToThousandSefareshi = table.Column<int>(type: "int", nullable: false),
                    OutOnetoTowThousandPishtaz = table.Column<int>(type: "int", nullable: false),
                    OutOnetoTowThousandSefareshi = table.Column<int>(type: "int", nullable: false),
                    OutTowtoFiveThousandSefareshi = table.Column<int>(type: "int", nullable: false),
                    OutTowtoThreeThousandPishtaz = table.Column<int>(type: "int", nullable: false),
                    OutThreetoFourThousandPishtaz = table.Column<int>(type: "int", nullable: false),
                    OutFourtoFiveThousandPishtaz = table.Column<int>(type: "int", nullable: false),
                    OutPerKiloSefareshi = table.Column<int>(type: "int", nullable: false),
                    OutPerKiloPishtaz = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblPost", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblProductComment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAndFamily = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserIp = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblProductComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblProductComment_Tblproduct_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Tblproduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblProvince",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblProvince", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Icon = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblSocialMedia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Link = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblSocialMedia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblCity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblCity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblCity_TblProvince_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "TblProvince",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tblproduct_CategoryId",
                table: "Tblproduct",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TblCity_ProvinceId",
                table: "TblCity",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_TblProductComment_ProductId",
                table: "TblProductComment",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tblproduct_TblCategory_CategoryId",
                table: "Tblproduct",
                column: "CategoryId",
                principalTable: "TblCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TblProductImage_Tblproduct_ProductId",
                table: "TblProductImage",
                column: "ProductId",
                principalTable: "Tblproduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tblproduct_TblCategory_CategoryId",
                table: "Tblproduct");

            migrationBuilder.DropForeignKey(
                name: "FK_TblProductImage_Tblproduct_ProductId",
                table: "TblProductImage");

            migrationBuilder.DropTable(
                name: "TblAboutUs");

            migrationBuilder.DropTable(
                name: "TblCategory");

            migrationBuilder.DropTable(
                name: "TblCity");

            migrationBuilder.DropTable(
                name: "TblContactUs");

            migrationBuilder.DropTable(
                name: "TblPost");

            migrationBuilder.DropTable(
                name: "TblProductComment");

            migrationBuilder.DropTable(
                name: "TblSetting");

            migrationBuilder.DropTable(
                name: "TblSocialMedia");

            migrationBuilder.DropTable(
                name: "TblProvince");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tblproduct",
                table: "Tblproduct");

            migrationBuilder.DropIndex(
                name: "IX_Tblproduct_CategoryId",
                table: "Tblproduct");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Tblproduct");

            migrationBuilder.RenameTable(
                name: "Tblproduct",
                newName: "TblProduct");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TblProduct",
                table: "TblProduct",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TblProductImage_TblProduct_ProductId",
                table: "TblProductImage",
                column: "ProductId",
                principalTable: "TblProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
