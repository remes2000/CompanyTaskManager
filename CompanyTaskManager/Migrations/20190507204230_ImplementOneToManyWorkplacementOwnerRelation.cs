using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanyTaskManager.Migrations
{
    public partial class ImplementOneToManyWorkplacementOwnerRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Workplacements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Workplacements_OwnerId",
                table: "Workplacements",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workplacements_Users_OwnerId",
                table: "Workplacements",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workplacements_Users_OwnerId",
                table: "Workplacements");

            migrationBuilder.DropIndex(
                name: "IX_Workplacements_OwnerId",
                table: "Workplacements");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Workplacements");
        }
    }
}
