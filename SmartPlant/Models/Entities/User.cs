using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPlant.Models.Entities
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column("username")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Surname is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Surname must be between 2 and 100 characters")]
        [Column("surname")]
        public string Surname { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        [Column("password")]
        public string Password { get; set; } = string.Empty;

        [StringLength(500)]
        [Column("photo_url")]
        public string? PhotoUrl { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(200)]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("bio")]
        public string? Bio { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(50)]
        [Column("role")]
        public string Role { get; set; } = "User";

        [Column("password_hash")]
        [StringLength(255)]
        public string? PasswordHash { get; set; }

        [Column("is_email_verified")]
        public bool IsEmailVerified { get; set; } = false;

        [Column("email_verification_token")]
        [StringLength(255)]
        public string? EmailVerificationToken { get; set; }

        [Column("email_verification_token_expiry")]
        public DateTime? EmailVerificationTokenExpiry { get; set; }

        [Column("email_verification_otp")]
        [StringLength(6)]
        public string? EmailVerificationOtp { get; set; }

        [Column("email_verification_otp_expiry")]
        public DateTime? EmailVerificationOtpExpiry { get; set; }

        // Navigation properties
        public virtual ICollection<UserPlant>? UserPlants { get; set; }
        public virtual ICollection<Reminder>? Reminders { get; set; }
        public virtual ICollection<UserFeedback>? Feedbacks { get; set; }
        public virtual ICollection<PasswordResetToken>? PasswordResetTokens { get; set; }
    }
}