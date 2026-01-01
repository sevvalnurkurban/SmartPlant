using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPlant.Models.Entities
{
    [Table("plants")]
    public class Plant
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Plant name is required")]
        [StringLength(100, MinimumLength = 2)]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        [Column("photo_url")]
        public string? PhotoUrl { get; set; }

        [StringLength(100)]
        [Column("type")]
        public string? Type { get; set; }

        [StringLength(100)]
        [Column("water_period")]
        public string? WaterPeriod { get; set; }

        [Column("water_ml_per_week")]
        public int? WaterMlPerWeek { get; set; }

        [StringLength(50)]
        [Column("light")]
        public string? Light { get; set; }

        [StringLength(50)]
        [Column("temperature")]
        public string? Temperature { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual ICollection<UserPlant>? UserPlants { get; set; }
        public virtual ICollection<Reminder>? Reminders { get; set; }
    }
}