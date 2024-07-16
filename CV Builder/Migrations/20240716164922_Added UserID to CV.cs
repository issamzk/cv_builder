using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CV_Builder.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserIDtoCV : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CV",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CV");
        }
    }
}
