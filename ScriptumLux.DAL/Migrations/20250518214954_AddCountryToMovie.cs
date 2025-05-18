using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScriptumLux.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddCountryToMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Movies",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Movies");
        }
    }
}
