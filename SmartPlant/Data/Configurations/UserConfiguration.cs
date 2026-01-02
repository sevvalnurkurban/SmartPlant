using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartPlant.Models.Entities;

namespace SmartPlant.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Username)
                .HasColumnName("username")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Surname)
                .HasColumnName("surname")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Password)
                .HasColumnName("password")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.PhotoUrl)
                .HasColumnName("photo_url")
                .HasMaxLength(500);

            builder.Property(u => u.Email)
                .HasColumnName("email")
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(u => u.Bio)
                .HasColumnName("bio");

            builder.Property(u => u.IsDeleted)
                .HasColumnName("is_deleted")
                .HasDefaultValue(false);

            builder.Property(u => u.CreatedDate)
                .HasColumnName("created_date")
                .HasDefaultValueSql("GETDATE()");

            // Relationships
            builder.HasMany(u => u.UserPlants)
                .WithOne(up => up.User)
                .HasForeignKey(up => up.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(u => u.Reminders)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(u => u.UserFeedbacks)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(u => u.PasswordResetTokens)
                .WithOne(prt => prt.User)
                .HasForeignKey(prt => prt.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
