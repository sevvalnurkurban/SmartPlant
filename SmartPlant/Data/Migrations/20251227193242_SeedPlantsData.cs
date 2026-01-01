using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartPlant.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedPlantsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "plants",
                columns: new[] { "name", "photo_url", "type", "water_period", "light", "temperature", "description", "is_deleted", "created_date" },
                values: new object[,]
                {
                    { "Pothos", null, "Vine", "Every 7-10 days", "Low to Bright Indirect", "18-27°C", "Easy-care trailing plant, perfect for beginners. Can tolerate low light and irregular watering.", false, DateTime.Now },
                    { "Snake Plant", null, "Succulent", "Every 2-3 weeks", "Low to Bright Indirect", "15-29°C", "Very low maintenance, can survive in almost any condition. Great air purifier.", false, DateTime.Now },
                    { "Spider Plant", null, "Foliage", "Every 7-10 days", "Bright Indirect Light", "15-24°C", "Easy-care houseplant that produces baby plants. Great for hanging baskets.", false, DateTime.Now },
                    { "Peace Lily", null, "Tropical", "Every 5-7 days", "Low to Medium Light", "18-27°C", "Beautiful white flowers, excellent air purifier. Prefers consistently moist soil.", false, DateTime.Now },
                    { "Monstera Deliciosa", null, "Tropical", "Every 7-10 days", "Bright Indirect Light", "18-27°C", "Popular indoor plant with large split leaves. Fast-growing and dramatic.", false, DateTime.Now },
                    { "ZZ Plant", null, "Foliage", "Every 2-3 weeks", "Low to Bright Light", "15-24°C", "Extremely drought-tolerant. Perfect for low-light spaces and forgetful waterers.", false, DateTime.Now },
                    { "Rubber Plant", null, "Tree", "Every 7-10 days", "Bright Indirect Light", "15-24°C", "Large glossy leaves, can grow into a small indoor tree. Needs regular wiping.", false, DateTime.Now },
                    { "Fiddle Leaf Fig", null, "Tree", "Every 7-10 days", "Bright Indirect Light", "18-27°C", "Trendy plant with large violin-shaped leaves. Needs consistent care.", false, DateTime.Now },
                    { "Philodendron", null, "Vine", "Every 7-10 days", "Medium to Bright Indirect", "18-27°C", "Easy-care trailing or climbing plant. Many varieties available.", false, DateTime.Now },
                    { "Aloe Vera", null, "Succulent", "Every 2-3 weeks", "Bright Light", "13-27°C", "Succulent with medicinal properties. Needs well-draining soil and bright light.", false, DateTime.Now },
                    { "English Ivy", null, "Vine", "Every 5-7 days", "Medium Light", "15-21°C", "Classic trailing plant. Prefers cooler temperatures and moderate moisture.", false, DateTime.Now },
                    { "Dracaena", null, "Tree", "Every 7-10 days", "Low to Medium Light", "18-24°C", "Low-maintenance with colorful striped leaves. Many varieties available.", false, DateTime.Now },
                    { "Boston Fern", null, "Fern", "Every 3-5 days", "Bright Indirect Light", "15-24°C", "Lush, feathery fronds. Needs high humidity and consistent moisture.", false, DateTime.Now },
                    { "Chinese Money Plant", null, "Foliage", "Every 7-10 days", "Bright Indirect Light", "15-24°C", "Unique round leaves on delicate stems. Easy to propagate and share.", false, DateTime.Now },
                    { "Jade Plant", null, "Succulent", "Every 2-3 weeks", "Bright Light", "18-24°C", "Lucky plant with thick, glossy leaves. Long-lived and easy to care for.", false, DateTime.Now },
                    { "Calathea", null, "Foliage", "Every 5-7 days", "Medium Indirect Light", "18-24°C", "Stunning patterned leaves. Needs high humidity and filtered water.", false, DateTime.Now },
                    { "String of Pearls", null, "Succulent", "Every 2-3 weeks", "Bright Indirect Light", "18-24°C", "Unique trailing succulent with bead-like leaves. Needs good drainage.", false, DateTime.Now },
                    { "Bird of Paradise", null, "Tropical", "Every 7-10 days", "Bright Light", "18-27°C", "Large tropical plant with paddle-shaped leaves. Needs space to grow.", false, DateTime.Now },
                    { "Christmas Cactus", null, "Cactus", "Every 7-10 days", "Bright Indirect Light", "15-21°C", "Blooms in winter with colorful flowers. Prefers slightly moist soil.", false, DateTime.Now },
                    { "Lavender", null, "Herb", "Every 7-14 days", "Full Sun", "15-30°C", "Aromatic herb with purple flowers. Needs well-draining soil and good airflow.", false, DateTime.Now },
                    { "Basil", null, "Herb", "Every 2-3 days", "Bright Light", "18-27°C", "Popular culinary herb. Needs regular watering and pinching for bushiness.", false, DateTime.Now },
                    { "Rosemary", null, "Herb", "Every 7-10 days", "Full Sun", "15-24°C", "Fragrant herb for cooking. Prefers drier conditions and good drainage.", false, DateTime.Now },
                    { "Mint", null, "Herb", "Every 3-5 days", "Partial Sun", "15-24°C", "Fast-growing aromatic herb. Keep moist and contain to prevent spreading.", false, DateTime.Now },
                    { "Orchid", null, "Flowering", "Every 7-10 days", "Bright Indirect Light", "18-27°C", "Elegant flowering plant. Water with ice cubes or soak method weekly.", false, DateTime.Now },
                    { "Anthurium", null, "Flowering", "Every 5-7 days", "Bright Indirect Light", "18-27°C", "Glossy heart-shaped flowers. Needs humidity and regular feeding.", false, DateTime.Now },
                    { "African Violet", null, "Flowering", "Every 3-5 days", "Bright Indirect Light", "18-24°C", "Compact flowering plant. Water from below to avoid leaf spots.", false, DateTime.Now },
                    { "Begonia", null, "Flowering", "Every 5-7 days", "Bright Indirect Light", "18-24°C", "Colorful flowers and foliage. Many varieties for different conditions.", false, DateTime.Now },
                    { "Geranium", null, "Flowering", "Every 5-7 days", "Full Sun to Partial", "15-24°C", "Classic flowering plant with aromatic leaves. Deadhead for more blooms.", false, DateTime.Now },
                    { "Hoya", null, "Vine", "Every 7-14 days", "Bright Indirect Light", "18-27°C", "Waxy leaves and fragrant flowers. Prefers to dry out between waterings.", false, DateTime.Now },
                    { "Tradescantia", null, "Vine", "Every 5-7 days", "Bright Indirect Light", "15-24°C", "Colorful trailing plant, easy to propagate. Fast-growing and low-maintenance.", false, DateTime.Now },
                    { "Peperomia", null, "Foliage", "Every 7-10 days", "Medium Light", "18-24°C", "Compact plant with thick leaves. Many varieties with different patterns.", false, DateTime.Now },
                    { "Schefflera", null, "Tree", "Every 7-10 days", "Bright Indirect Light", "15-24°C", "Umbrella-like leaf clusters. Can grow tall indoors with proper care.", false, DateTime.Now },
                    { "Areca Palm", null, "Palm", "Every 5-7 days", "Bright Indirect Light", "18-27°C", "Graceful feathery fronds. Excellent air purifier and humidifier.", false, DateTime.Now },
                    { "Parlor Palm", null, "Palm", "Every 7-10 days", "Low to Medium Light", "15-24°C", "Small palm perfect for low-light areas. Slow-growing and compact.", false, DateTime.Now },
                    { "Ponytail Palm", null, "Succulent", "Every 2-3 weeks", "Bright Light", "15-27°C", "Unique trunk stores water. Very drought-tolerant and easy to care for.", false, DateTime.Now },
                    { "Haworthia", null, "Succulent", "Every 2-3 weeks", "Bright Indirect Light", "15-27°C", "Small striped succulent. Perfect for small spaces and beginners.", false, DateTime.Now },
                    { "Echeveria", null, "Succulent", "Every 2-3 weeks", "Bright Light", "15-27°C", "Rosette-shaped succulent in many colors. Needs good drainage.", false, DateTime.Now },
                    { "Cactus Mix", null, "Cactus", "Every 2-4 weeks", "Full Sun", "15-30°C", "Various small cacti. Very low water needs and bright light requirements.", false, DateTime.Now },
                    { "Golden Barrel Cactus", null, "Cactus", "Every 3-4 weeks", "Full Sun", "15-35°C", "Round, golden-spined cactus. Extremely drought-tolerant.", false, DateTime.Now },
                    { "Dieffenbachia", null, "Foliage", "Every 5-7 days", "Medium Indirect Light", "18-24°C", "Large variegated leaves. Keep away from pets as it's toxic.", false, DateTime.Now },
                    { "Croton", null, "Foliage", "Every 5-7 days", "Bright Light", "18-27°C", "Colorful multicolored leaves. Needs bright light for best color.", false, DateTime.Now },
                    { "Ficus Benjamina", null, "Tree", "Every 7-10 days", "Bright Indirect Light", "18-24°C", "Classic indoor tree. Can be temperamental with location changes.", false, DateTime.Now },
                    { "Yucca", null, "Tree", "Every 2-3 weeks", "Bright Light", "15-27°C", "Sword-like leaves on woody stems. Very drought-tolerant.", false, DateTime.Now },
                    { "Wandering Jew", null, "Vine", "Every 5-7 days", "Bright Indirect Light", "15-24°C", "Fast-growing colorful trailer. Easy to propagate in water.", false, DateTime.Now },
                    { "Coleus", null, "Foliage", "Every 3-5 days", "Bright Indirect Light", "18-27°C", "Vibrant colorful foliage. Easy to grow and propagate.", false, DateTime.Now },
                    { "Prayer Plant", null, "Foliage", "Every 5-7 days", "Medium Indirect Light", "18-24°C", "Leaves fold up at night like praying hands. Needs humidity.", false, DateTime.Now },
                    { "String of Hearts", null, "Succulent", "Every 2-3 weeks", "Bright Indirect Light", "18-24°C", "Delicate trailing vine with heart-shaped leaves. Easy care.", false, DateTime.Now },
                    { "Nerve Plant", null, "Foliage", "Every 3-5 days", "Medium Indirect Light", "18-24°C", "Striking veined leaves. Dramatic when thirsty but recovers quickly.", false, DateTime.Now },
                    { "Cast Iron Plant", null, "Foliage", "Every 2-3 weeks", "Low Light", "10-27°C", "Nearly indestructible. Tolerates neglect, low light, and temperature extremes.", false, DateTime.Now },
                    { "Lucky Bamboo", null, "Foliage", "Keep in water", "Low to Medium Light", "18-27°C", "Can grow in water or soil. Symbol of good fortune and prosperity.", false, DateTime.Now }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM plants WHERE name IN ('Pothos', 'Snake Plant', 'Spider Plant', 'Peace Lily', 'Monstera Deliciosa', 'ZZ Plant', 'Rubber Plant', 'Fiddle Leaf Fig', 'Philodendron', 'Aloe Vera', 'English Ivy', 'Dracaena', 'Boston Fern', 'Chinese Money Plant', 'Jade Plant', 'Calathea', 'String of Pearls', 'Bird of Paradise', 'Christmas Cactus', 'Lavender', 'Basil', 'Rosemary', 'Mint', 'Orchid', 'Anthurium', 'African Violet', 'Begonia', 'Geranium', 'Hoya', 'Tradescantia', 'Peperomia', 'Schefflera', 'Areca Palm', 'Parlor Palm', 'Ponytail Palm', 'Haworthia', 'Echeveria', 'Cactus Mix', 'Golden Barrel Cactus', 'Dieffenbachia', 'Croton', 'Ficus Benjamina', 'Yucca', 'Wandering Jew', 'Coleus', 'Prayer Plant', 'String of Hearts', 'Nerve Plant', 'Cast Iron Plant', 'Lucky Bamboo')");
        }
    }
}
