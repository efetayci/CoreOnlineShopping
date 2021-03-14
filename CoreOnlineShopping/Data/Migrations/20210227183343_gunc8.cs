using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreOnlineShopping.Data.Migrations
{
    public partial class gunc8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvilabele",
                table: "Products");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvilable",
                table: "Products",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvilable",
                table: "Products");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvilabele",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
