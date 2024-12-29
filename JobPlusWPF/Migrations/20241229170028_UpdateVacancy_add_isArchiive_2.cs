using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPlusWPF.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVacancy_add_isArchiive_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Archive");

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Vacancies",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Vacancies");

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Archive",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
