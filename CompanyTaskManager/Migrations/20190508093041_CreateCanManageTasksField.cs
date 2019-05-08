using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanyTaskManager.Migrations
{
    public partial class CreateCanManageTasksField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanManageTasks",
                table: "UserWorkplacements",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanManageTasks",
                table: "UserWorkplacements");
        }
    }
}
