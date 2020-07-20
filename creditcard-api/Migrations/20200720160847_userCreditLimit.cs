using Microsoft.EntityFrameworkCore.Migrations;

namespace creditcard_api.Migrations
{
    public partial class userCreditLimit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CreditLimit",
                table: "Users",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "MaxCreditLimit",
                table: "Users",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditLimit",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MaxCreditLimit",
                table: "Users");
        }
    }
}
