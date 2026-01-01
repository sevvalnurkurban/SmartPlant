using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPlant.Models.Entities
{
    [Table("user_plants")]
    public class UserPlant
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [Column("plant_id")]
        public int PlantId { get; set; }

        [Column("last_watered")]
        public DateTime? LastWatered { get; set; }

        [Column("next_watering")]
        public DateTime? NextWatering { get; set; }

        [StringLength(50)]
        [Column("status")]
        public string? Status { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        [ForeignKey("PlantId")]
        public virtual Plant? Plant { get; set; }
    }
}