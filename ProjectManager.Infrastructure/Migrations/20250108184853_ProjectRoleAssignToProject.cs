using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProjectRoleAssignToProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "ProjectRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRoles_ProjectId",
                table: "ProjectRoles",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRoles_Projects_ProjectId",
                table: "ProjectRoles",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRoles_Projects_ProjectId",
                table: "ProjectRoles");

            migrationBuilder.DropIndex(
                name: "IX_ProjectRoles_ProjectId",
                table: "ProjectRoles");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "ProjectRoles");
        }
    }
}
