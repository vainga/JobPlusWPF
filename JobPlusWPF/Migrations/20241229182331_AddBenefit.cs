using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPlusWPF.Migrations
{
    /// <inheritdoc />
    public partial class AddBenefit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Benefits_JobSeekers_JobSeekerId",
                table: "Benefits");

            migrationBuilder.DropForeignKey(
                name: "FK_Benefits_Users_UserId",
                table: "Benefits");

            migrationBuilder.AddForeignKey(
                name: "FK_Benefits_JobSeekers_JobSeekerId",
                table: "Benefits",
                column: "JobSeekerId",
                principalTable: "JobSeekers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Benefits_Users_UserId",
                table: "Benefits",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Benefits_JobSeekers_JobSeekerId",
                table: "Benefits");

            migrationBuilder.DropForeignKey(
                name: "FK_Benefits_Users_UserId",
                table: "Benefits");

            migrationBuilder.AddForeignKey(
                name: "FK_Benefits_JobSeekers_JobSeekerId",
                table: "Benefits",
                column: "JobSeekerId",
                principalTable: "JobSeekers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Benefits_Users_UserId",
                table: "Benefits",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
