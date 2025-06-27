using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingSystem.Repository.Migrations
{
    /// <inheritdoc />
    public partial class UserAccommodation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedFromUserId",
                table: "Accommodations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accommodations_CreatedFromUserId",
                table: "Accommodations",
                column: "CreatedFromUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accommodations_AspNetUsers_CreatedFromUserId",
                table: "Accommodations",
                column: "CreatedFromUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accommodations_AspNetUsers_CreatedFromUserId",
                table: "Accommodations");

            migrationBuilder.DropIndex(
                name: "IX_Accommodations_CreatedFromUserId",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "CreatedFromUserId",
                table: "Accommodations");
        }
    }
}
