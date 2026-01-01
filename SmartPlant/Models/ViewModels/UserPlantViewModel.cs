using System.ComponentModel.DataAnnotations;

namespace SmartPlant.Models.ViewModels
{
    public class UserPlantViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Select Plant")]
        public int PlantId { get; set; }

        [Display(Name = "Last Watered")]
        [DataType(DataType.DateTime)]
        public DateTime? LastWatered { get; set; }

        [Display(Name = "Next Watering")]
        [DataType(DataType.DateTime)]
        public DateTime? NextWatering { get; set; }

        [StringLength(50)]
        [Display(Name = "Status")]
        public string? Status { get; set; }

        // For display
        public string? PlantName { get; set; }
        public string? PlantPhotoUrl { get; set; }
    }
}