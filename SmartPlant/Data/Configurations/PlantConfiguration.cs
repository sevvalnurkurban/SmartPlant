using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartPlant.Models.Entities;

namespace SmartPlant.Data.Configurations
{
    public class PlantConfiguration : IEntityTypeConfiguration<Plant>
    {
        public void Configure(EntityTypeBuilder<Plant> builder)
        {
            builder.ToTable("plants");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.PhotoUrl)
                .HasColumnName("photo_url")
                .HasMaxLength(500);

            builder.Property(p => p.Type)
                .HasColumnName("type")
                .HasMaxLength(100);

            builder.Property(p => p.WaterPeriod)
                .HasColumnName("water_period")
                .HasMaxLength(100);

            builder.Property(p => p.Light)
                .HasColumnName("light")
                .HasMaxLength(50);

            builder.Property(p => p.Temperature)
                .HasColumnName("temperature")
                .HasMaxLength(50);

            builder.Property(p => p.Description)
                .HasColumnName("description");

            builder.Property(p => p.IsDeleted)
                .HasColumnName("is_deleted")
                .HasDefaultValue(false);

            builder.Property(p => p.CreatedDate)
                .HasColumnName("created_date")
                .HasDefaultValueSql("GETDATE()");

            // Relationships
            builder.HasMany(p => p.UserPlants)
                .WithOne(up => up.Plant)
                .HasForeignKey(up => up.PlantId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.Reminders)
                .WithOne(r => r.Plant)
                .HasForeignKey(r => r.PlantId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
