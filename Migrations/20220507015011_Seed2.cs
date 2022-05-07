using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwapApp.Migrations
{
    public partial class Seed2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WherePickup_Item_ItemId",
                table: "WherePickup");

            migrationBuilder.DropIndex(
                name: "IX_WherePickup_ItemId",
                table: "WherePickup");

            migrationBuilder.AlterColumn<int>(
                name: "WherePickupId",
                table: "Item",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "WherePickupId1",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_WherePickup_WherePickupId1",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_WherePickupId1",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "WherePickupId1",
                table: "Item");

            migrationBuilder.AlterColumn<string>(
                name: "WherePickupId",
                table: "Item",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_WherePickup_ItemId",
                table: "WherePickup",
                column: "ItemId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WherePickup_Item_ItemId",
                table: "WherePickup",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
