using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwapApp.Migrations
{
    public partial class Seed4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Item_UserId",
                table: "Item");

            migrationBuilder.CreateIndex(
                name: "IX_Item_UserId",
                table: "Item",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Item_UserId",
                table: "Item");

            migrationBuilder.CreateIndex(
                name: "IX_Item_UserId",
                table: "Item",
                column: "UserId");
        }
    }
}
