using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPlusWPF.Migrations
{
    /// <inheritdoc />
    public partial class EmployerUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Employers");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Employers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StreetId",
                table: "Employers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employers_CityId",
                table: "Employers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Employers_StreetId",
                table: "Employers",
                column: "StreetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employers_Cities_CityId",
                table: "Employers",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employers_Streets_StreetId",
                table: "Employers",
                column: "StreetId",
                principalTable: "Streets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employers_Cities_CityId",
                table: "Employers");

            migrationBuilder.DropForeignKey(
                name: "FK_Employers_Streets_StreetId",
                table: "Employers");

            migrationBuilder.DropIndex(
                name: "IX_Employers_CityId",
                table: "Employers");

            migrationBuilder.DropIndex(
                name: "IX_Employers_StreetId",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "StreetId",
                table: "Employers");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Employers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
