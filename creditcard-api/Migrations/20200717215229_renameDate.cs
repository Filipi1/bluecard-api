using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace creditcard_api.Migrations
{
    public partial class renameDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataOperacao",
                table: "Transactions");

            migrationBuilder.AddColumn<DateTime>(
                name: "OperationDate",
                table: "Transactions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OperationDate",
                table: "Transactions");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataOperacao",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
