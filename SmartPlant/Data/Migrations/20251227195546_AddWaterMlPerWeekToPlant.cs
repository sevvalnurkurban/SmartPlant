using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartPlant.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddWaterMlPerWeekToPlant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "water_ml_per_week",
                table: "plants",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "water_ml_per_week",
                table: "plants");
        }
    }
}
