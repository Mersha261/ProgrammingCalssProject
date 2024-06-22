using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PgrogrammingClass.Data.Migrations
{
    /// <inheritdoc />
    public partial class aboutUs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutUsFooter",
                table: "TblAboutUs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "TblAboutUs",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "TblAboutUs",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumbers",
                table: "TblAboutUs",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutUsFooter",
                table: "TblAboutUs");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "TblAboutUs");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "TblAboutUs");

            migrationBuilder.DropColumn(
                name: "PhoneNumbers",
                table: "TblAboutUs");
        }
    }
}
