using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwapApp.Migrations
{
    public partial class Seed5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_User_UserId",
                table: "Item");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Item_UserId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Item");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_UserId",
                table: "Item",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_User_UserId",
                table: "Item",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
