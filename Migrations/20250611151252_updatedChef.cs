using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantMVC.Migrations
{
    /// <inheritdoc />
    public partial class updatedChef : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverImgUrl",
                table: "Chefs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImgUrl",
                table: "Chefs");
        }
    }
}
