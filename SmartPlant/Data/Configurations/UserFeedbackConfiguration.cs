using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartPlant.Models.Entities;

namespace SmartPlant.Data.Configurations
{
    public class UserFeedbackConfiguration : IEntityTypeConfiguration<UserFeedback>
    {
        public void Configure(EntityTypeBuilder<UserFeedback> builder)
        {
            builder.ToTable("user_feedback");

            builder.HasKey(uf => uf.Id);

            builder.Property(uf => uf.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(uf => uf.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(uf => uf.Feedback)
                .HasColumnName("feedback")
                .IsRequired();

            builder.Property(uf => uf.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(uf => uf.IsDeleted)
                .HasColumnName("is_deleted")
                .HasDefaultValue(false);

            // Relationships already defined in User configuration
        }
    }
}
