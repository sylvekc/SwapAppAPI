using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwapApp.Migrations
{
    public partial class AddReservedByColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservedBy",
                table: "Item",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservedBy",
                table: "Item");
        }
    }
}
