using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingSystem.Repository.Migrations
{
    /// <inheritdoc />
    public partial class CountryChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capital",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "CurrencySymbol",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "FlagUrl",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Countries");

            migrationBuilder.RenameColumn(
                name: "OfficialName",
                table: "Countries",
                newName: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Countries",
                newName: "OfficialName");

            migrationBuilder.AddColumn<string>(
                name: "Capital",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrencySymbol",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FlagUrl",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
