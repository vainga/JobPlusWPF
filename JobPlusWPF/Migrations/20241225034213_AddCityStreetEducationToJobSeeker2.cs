using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPlusWPF.Migrations
{
    /// <inheritdoc />
    public partial class AddCityStreetEducationToJobSeeker2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employers_Cities_CityId",
                table: "Employers");

            migrationBuilder.DropForeignKey(
                name: "FK_Employers_Streets_StreetId",
                table: "Employers");

            migrationBuilder.AddForeignKey(
                name: "FK_Employers_Cities_CityId",
                table: "Employers",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employers_Streets_StreetId",
                table: "Employers",
                column: "StreetId",
                principalTable: "Streets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
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
    }
}
