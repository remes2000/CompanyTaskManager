using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanyTaskManager.Migrations
{
    public partial class CreateTaskTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Users_AddedById",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_Users_EmployeeId",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_Workplacements_WorkplacementId",
                table: "Task");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Task",
                table: "Task");

            migrationBuilder.RenameTable(
                name: "Task",
                newName: "Tasks");

            migrationBuilder.RenameIndex(
                name: "IX_Task_WorkplacementId",
                table: "Tasks",
                newName: "IX_Tasks_WorkplacementId");

            migrationBuilder.RenameIndex(
                name: "IX_Task_EmployeeId",
                table: "Tasks",
                newName: "IX_Tasks_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Task_AddedById",
                table: "Tasks",
                newName: "IX_Tasks_AddedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_AddedById",
                table: "Tasks",
                column: "AddedById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_EmployeeId",
                table: "Tasks",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Workplacements_WorkplacementId",
                table: "Tasks",
                column: "WorkplacementId",
                principalTable: "Workplacements",
                principalColumn: "WorkplacementId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_AddedById",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_EmployeeId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Workplacements_WorkplacementId",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "Task");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_WorkplacementId",
                table: "Task",
                newName: "IX_Task_WorkplacementId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_EmployeeId",
                table: "Task",
                newName: "IX_Task_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_AddedById",
                table: "Task",
                newName: "IX_Task_AddedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Task",
                table: "Task",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Users_AddedById",
                table: "Task",
                column: "AddedById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Users_EmployeeId",
                table: "Task",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Workplacements_WorkplacementId",
                table: "Task",
                column: "WorkplacementId",
                principalTable: "Workplacements",
                principalColumn: "WorkplacementId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
