using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodTracker.Data.Migrations
{
    public partial class AddOwnerToFood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerName",
                table: "Foods",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_OwnerName",
                table: "Foods",
                column: "OwnerName");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_AspNetUsers_OwnerName",
                table: "Foods",
                column: "OwnerName",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_AspNetUsers_OwnerName",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Foods_OwnerName",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "OwnerName",
                table: "Foods");
        }
    }
}
