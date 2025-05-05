using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileControl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewColumnCheclLists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckList_ProjectTasks_TaskId",
                table: "CheckList");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckList_ProjectTasks_TaskId1",
                table: "CheckList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckList",
                table: "CheckList");

            migrationBuilder.RenameTable(
                name: "CheckList",
                newName: "CheckLists");

            migrationBuilder.RenameIndex(
                name: "IX_CheckList_TaskId1",
                table: "CheckLists",
                newName: "IX_CheckLists_TaskId1");

            migrationBuilder.RenameIndex(
                name: "IX_CheckList_TaskId",
                table: "CheckLists",
                newName: "IX_CheckLists_TaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckLists",
                table: "CheckLists",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckLists_ProjectTasks_TaskId",
                table: "CheckLists",
                column: "TaskId",
                principalTable: "ProjectTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckLists_ProjectTasks_TaskId1",
                table: "CheckLists",
                column: "TaskId1",
                principalTable: "ProjectTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckLists_ProjectTasks_TaskId",
                table: "CheckLists");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckLists_ProjectTasks_TaskId1",
                table: "CheckLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckLists",
                table: "CheckLists");

            migrationBuilder.RenameTable(
                name: "CheckLists",
                newName: "CheckList");

            migrationBuilder.RenameIndex(
                name: "IX_CheckLists_TaskId1",
                table: "CheckList",
                newName: "IX_CheckList_TaskId1");

            migrationBuilder.RenameIndex(
                name: "IX_CheckLists_TaskId",
                table: "CheckList",
                newName: "IX_CheckList_TaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckList",
                table: "CheckList",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckList_ProjectTasks_TaskId",
                table: "CheckList",
                column: "TaskId",
                principalTable: "ProjectTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckList_ProjectTasks_TaskId1",
                table: "CheckList",
                column: "TaskId1",
                principalTable: "ProjectTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
