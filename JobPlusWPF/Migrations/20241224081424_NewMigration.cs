using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPlusWPF.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_JobSeekers_UserId",
                table: "JobSeekers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employers_UserId",
                table: "Employers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Benefits_UserId",
                table: "Benefits",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Benefits_Users_UserId",
                table: "Benefits",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employers_Users_UserId",
                table: "Employers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobSeekers_Users_UserId",
                table: "JobSeekers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Benefits_Users_UserId",
                table: "Benefits");

            migrationBuilder.DropForeignKey(
                name: "FK_Employers_Users_UserId",
                table: "Employers");

            migrationBuilder.DropForeignKey(
                name: "FK_JobSeekers_Users_UserId",
                table: "JobSeekers");

            migrationBuilder.DropIndex(
                name: "IX_JobSeekers_UserId",
                table: "JobSeekers");

            migrationBuilder.DropIndex(
                name: "IX_Employers_UserId",
                table: "Employers");

            migrationBuilder.DropIndex(
                name: "IX_Benefits_UserId",
                table: "Benefits");
        }
    }
}
