using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartPlant.Models.Entities;

namespace SmartPlant.Data.Configurations
{
    public class UserPlantConfiguration : IEntityTypeConfiguration<UserPlant>
    {
        public void Configure(EntityTypeBuilder<UserPlant> builder)
        {
            builder.ToTable("user_plants");

            builder.HasKey(up => up.Id);

            builder.Property(up => up.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(up => up.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(up => up.PlantId)
                .HasColumnName("plant_id")
                .IsRequired();

            builder.Property(up => up.LastWatered)
                .HasColumnName("last_watered");

            builder.Property(up => up.NextWatering)
                .HasColumnName("next_watering");

            builder.Property(up => up.Status)
                .HasColumnName("status")
                .HasMaxLength(50);

            builder.Property(up => up.IsDeleted)
                .HasColumnName("is_deleted")
                .HasDefaultValue(false);

            builder.Property(up => up.CreatedDate)
                .HasColumnName("created_date")
                .HasDefaultValueSql("GETDATE()");

            // Relationships already defined in User and Plant configurations
        }
    }
}
