using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPlant.Models.Entities
{
    [Table("admins")]
    public class Admin
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column("username")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2)]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Surname is required")]
        [StringLength(100, MinimumLength = 2)]
        [Column("surname")]
        public string Surname { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        [Column("password")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(200)]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [StringLength(500)]
        [Column("photo_url")]
        public string? PhotoUrl { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual ICollection<PasswordResetToken>? PasswordResetTokens { get; set; }
    }
}