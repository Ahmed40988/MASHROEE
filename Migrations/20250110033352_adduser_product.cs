using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MASHROEE.Migrations
{
    /// <inheritdoc />
    public partial class adduser_product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationuserId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ApplicationuserId",
                table: "Products",
                column: "ApplicationuserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_ApplicationuserId",
                table: "Products",
                column: "ApplicationuserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_ApplicationuserId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ApplicationuserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ApplicationuserId",
                table: "Products");
        }
    }
}
