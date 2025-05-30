﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PgrogrammingClass.Data.Migrations
{
    /// <inheritdoc />
    public partial class IndexSentes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IndexSentens",
                table: "TblAboutUs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IndexSentens",
                table: "TblAboutUs");
        }
    }
}
