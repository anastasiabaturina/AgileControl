using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileControl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserNameColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "UserName",
            table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
