using Microsoft.EntityFrameworkCore;
using SmartPlant.Models.Entities;

namespace SmartPlant.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<UserPlant> UserPlants { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<UserFeedback> UserFeedbacks { get; set; }
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations
            modelBuilder.ApplyConfiguration(new Configurations.UserConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.AdminConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.PlantConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.UserPlantConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.ReminderConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.UserFeedbackConfiguration());

            // Indexes
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<Admin>()
                .HasIndex(a => a.Email)
                .IsUnique();

            modelBuilder.Entity<Admin>()
                .HasIndex(a => a.Username)
                .IsUnique();

            modelBuilder.Entity<UserPlant>()
                .HasIndex(up => up.UserId);

            modelBuilder.Entity<UserPlant>()
                .HasIndex(up => up.PlantId);

            modelBuilder.Entity<Reminder>()
                .HasIndex(r => r.UserId);

            modelBuilder.Entity<Reminder>()
                .HasIndex(r => r.ReminderDate);

            modelBuilder.Entity<PasswordResetToken>()
                .HasIndex(prt => prt.Token)
                .IsUnique();
        }
    }
}