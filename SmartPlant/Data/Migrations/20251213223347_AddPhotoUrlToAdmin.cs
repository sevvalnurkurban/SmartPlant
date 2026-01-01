using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartPlant.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotoUrlToAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "photo_url",
                table: "admins",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "photo_url",
                table: "admins");
        }
    }
}
