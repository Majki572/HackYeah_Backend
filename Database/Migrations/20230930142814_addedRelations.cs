using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class addedRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FridgeId",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FridgeId",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_FridgeId",
                table: "Users",
                column: "FridgeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_FridgeId",
                table: "Products",
                column: "FridgeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Fridges_FridgeId",
                table: "Products",
                column: "FridgeId",
                principalTable: "Fridges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Fridges_FridgeId",
                table: "Users",
                column: "FridgeId",
                principalTable: "Fridges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Fridges_FridgeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Fridges_FridgeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_FridgeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Products_FridgeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "FridgeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FridgeId",
                table: "Products");
        }
    }
}
