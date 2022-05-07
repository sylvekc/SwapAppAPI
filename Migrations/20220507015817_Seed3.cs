using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwapApp.Migrations
{
    public partial class Seed3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_WherePickup_WherePickupId1",
                table: "Item");

            migrationBuilder.DropTable(
                name: "WherePickup");

            migrationBuilder.DropIndex(
                name: "IX_Item_WherePickupId1",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "WherePickupId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "WherePickupId1",
                table: "Item");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Item",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Item",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Item",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "District",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Item");

            migrationBuilder.AddColumn<int>(
                name: "WherePickupId",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WherePickupId1",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "WherePickup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WherePickup", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_WherePickupId1",
                table: "Item",
                column: "WherePickupId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_WherePickup_WherePickupId1",
                table: "Item",
                column: "WherePickupId1",
                principalTable: "WherePickup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
