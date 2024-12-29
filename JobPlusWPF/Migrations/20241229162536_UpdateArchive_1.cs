using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPlusWPF.Migrations
{
    /// <inheritdoc />
    public partial class UpdateArchive_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Archive",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Archive_UserId",
                table: "Archive",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Archive_Users_UserId",
                table: "Archive",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Archive_Users_UserId",
                table: "Archive");

            migrationBuilder.DropIndex(
                name: "IX_Archive_UserId",
                table: "Archive");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Archive");
        }
    }
}
