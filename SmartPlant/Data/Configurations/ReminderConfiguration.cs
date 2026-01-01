using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartPlant.Models.Entities;

namespace SmartPlant.Data.Configurations
{
    public class ReminderConfiguration : IEntityTypeConfiguration<Reminder>
    {
        public void Configure(EntityTypeBuilder<Reminder> builder)
        {
            builder.ToTable("reminders");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(r => r.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(r => r.PlantId)
                .HasColumnName("plant_id")
                .IsRequired();

            builder.Property(r => r.Task)
                .HasColumnName("task")
                .HasMaxLength(255);

            builder.Property(r => r.ReminderDate)
                .HasColumnName("reminder_date");

            builder.Property(r => r.Status)
                .HasColumnName("status")
                .HasMaxLength(50)
                .HasDefaultValue("Pending");

            builder.Property(r => r.IsDeleted)
                .HasColumnName("is_deleted")
                .HasDefaultValue(false);

            builder.Property(r => r.CreatedDate)
                .HasColumnName("created_date")
                .HasDefaultValueSql("GETDATE()");

            // Relationships already defined in User and Plant configurations
        }
    }
}
