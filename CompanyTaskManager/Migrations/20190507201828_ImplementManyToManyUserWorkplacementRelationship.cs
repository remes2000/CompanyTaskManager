using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CompanyTaskManager.Migrations
{
    public partial class ImplementManyToManyUserWorkplacementRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersWorkplacements",
                table: "UsersWorkplacements");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Workplacements");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UsersWorkplacements");

            migrationBuilder.DropColumn(
                name: "CanManageTasks",
                table: "UsersWorkplacements");

            migrationBuilder.RenameTable(
                name: "UsersWorkplacements",
                newName: "UserWorkplacements");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Workplacements",
                newName: "WorkplacementId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserWorkplacements",
                table: "UserWorkplacements",
                columns: new[] { "UserId", "WorkplacementId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkplacements_WorkplacementId",
                table: "UserWorkplacements",
                column: "WorkplacementId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWorkplacements_Users_UserId",
                table: "UserWorkplacements",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserWorkplacements_Workplacements_WorkplacementId",
                table: "UserWorkplacements",
                column: "WorkplacementId",
                principalTable: "Workplacements",
                principalColumn: "WorkplacementId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWorkplacements_Users_UserId",
                table: "UserWorkplacements");

            migrationBuilder.DropForeignKey(
                name: "FK_UserWorkplacements_Workplacements_WorkplacementId",
                table: "UserWorkplacements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserWorkplacements",
                table: "UserWorkplacements");

            migrationBuilder.DropIndex(
                name: "IX_UserWorkplacements_WorkplacementId",
                table: "UserWorkplacements");

            migrationBuilder.RenameTable(
                name: "UserWorkplacements",
                newName: "UsersWorkplacements");

            migrationBuilder.RenameColumn(
                name: "WorkplacementId",
                table: "Workplacements",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Workplacements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UsersWorkplacements",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<bool>(
                name: "CanManageTasks",
                table: "UsersWorkplacements",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersWorkplacements",
                table: "UsersWorkplacements",
                column: "Id");
        }
    }
}
