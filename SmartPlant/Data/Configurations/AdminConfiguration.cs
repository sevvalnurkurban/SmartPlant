using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartPlant.Models.Entities;

namespace SmartPlant.Data.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.ToTable("admins");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(a => a.Username)
                .HasColumnName("username")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Surname)
                .HasColumnName("surname")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Password)
                .HasColumnName("password")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(a => a.Email)
                .HasColumnName("email")
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.IsDeleted)
                .HasColumnName("is_deleted")
                .HasDefaultValue(false);

            builder.Property(a => a.CreatedDate)
                .HasColumnName("created_date")
                .HasDefaultValueSql("GETDATE()");

            // Relationships
            builder.HasMany(a => a.PasswordResetTokens)
                .WithOne(prt => prt.Admin)
                .HasForeignKey(prt => prt.AdminId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
