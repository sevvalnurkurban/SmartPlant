using System.ComponentModel.DataAnnotations;

namespace SmartPlant.Models.ViewModels
{
    public class PlantViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Plant name is required")]
        [StringLength(100)]
        [Display(Name = "Plant Name")]
        public string Name { get; set; } = string.Empty;

        [StringLength(100)]
        [Display(Name = "Type/Category")]
        public string? Type { get; set; }

        [StringLength(100)]
        [Display(Name = "Water Period")]
        public string? WaterPeriod { get; set; }

        [Display(Name = "Water Amount (ml/week)")]
        public int? WaterMlPerWeek { get; set; }

        [StringLength(50)]
        [Display(Name = "Light Requirement")]
        public string? Light { get; set; }

        [StringLength(50)]
        [Display(Name = "Temperature Range")]
        public string? Temperature { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Photo")]
        public IFormFile? Photo { get; set; }

        public string? PhotoUrl { get; set; }
    }
}