using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAtField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UpdateAt",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Posts");
        }
    }
}
