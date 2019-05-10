using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanyTaskManager.Migrations
{
    public partial class SetEmployeeIdAsOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_EmployeeId",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Tasks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_EmployeeId",
                table: "Tasks",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_EmployeeId",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Tasks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_EmployeeId",
                table: "Tasks",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
