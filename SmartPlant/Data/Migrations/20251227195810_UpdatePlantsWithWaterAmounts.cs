using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartPlant.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePlantsWithWaterAmounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Update water amounts for each plant (ml per week)
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 250 WHERE name = 'Pothos'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 100 WHERE name = 'Snake Plant'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 300 WHERE name = 'Spider Plant'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 400 WHERE name = 'Peace Lily'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 500 WHERE name = 'Monstera Deliciosa'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 150 WHERE name = 'ZZ Plant'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 350 WHERE name = 'Rubber Plant'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 400 WHERE name = 'Fiddle Leaf Fig'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 300 WHERE name = 'Philodendron'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 100 WHERE name = 'Aloe Vera'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 350 WHERE name = 'English Ivy'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 250 WHERE name = 'Dracaena'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 500 WHERE name = 'Boston Fern'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 200 WHERE name = 'Chinese Money Plant'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 100 WHERE name = 'Jade Plant'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 400 WHERE name = 'Calathea'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 100 WHERE name = 'String of Pearls'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 600 WHERE name = 'Bird of Paradise'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 200 WHERE name = 'Christmas Cactus'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 150 WHERE name = 'Lavender'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 700 WHERE name = 'Basil'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 200 WHERE name = 'Rosemary'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 500 WHERE name = 'Mint'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 250 WHERE name = 'Orchid'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 350 WHERE name = 'Anthurium'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 300 WHERE name = 'African Violet'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 350 WHERE name = 'Begonia'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 400 WHERE name = 'Geranium'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 150 WHERE name = 'Hoya'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 300 WHERE name = 'Tradescantia'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 200 WHERE name = 'Peperomia'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 300 WHERE name = 'Schefflera'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 500 WHERE name = 'Areca Palm'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 300 WHERE name = 'Parlor Palm'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 100 WHERE name = 'Ponytail Palm'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 80 WHERE name = 'Haworthia'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 100 WHERE name = 'Echeveria'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 80 WHERE name = 'Cactus Mix'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 60 WHERE name = 'Golden Barrel Cactus'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 350 WHERE name = 'Dieffenbachia'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 350 WHERE name = 'Croton'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 300 WHERE name = 'Ficus Benjamina'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 150 WHERE name = 'Yucca'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 300 WHERE name = 'Wandering Jew'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 400 WHERE name = 'Coleus'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 350 WHERE name = 'Prayer Plant'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 120 WHERE name = 'String of Hearts'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 400 WHERE name = 'Nerve Plant'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 200 WHERE name = 'Cast Iron Plant'");
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = 300 WHERE name = 'Lucky Bamboo'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE plants SET water_ml_per_week = NULL");
        }
    }
}
