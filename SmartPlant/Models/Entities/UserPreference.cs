using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPlant.Models.Entities
{
    [Table("user_preferences")]
    public class UserPreference
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("reminder_time")]
        [StringLength(5)]
        public string ReminderTime { get; set; } = "09:00";

        [Column("time_zone")]
        [StringLength(100)]
        public string TimeZone { get; set; } = "Turkey Standard Time";

        [Column("reminder_frequency")]
        [StringLength(50)]
        public string ReminderFrequency { get; set; } = "Daily";

        [Column("email_notifications_enabled")]
        public bool EmailNotificationsEnabled { get; set; } = true;

        [Column("in_app_notifications_enabled")]
        public bool InAppNotificationsEnabled { get; set; } = true;

        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Column("updated_date")]
        public DateTime? UpdatedDate { get; set; }

        // Navigation property
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
    }
}