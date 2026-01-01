using System.ComponentModel.DataAnnotations;

namespace SmartPlant.Models.ViewModels
{
    public class ReminderViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Plant")]
        public int PlantId { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Task")]
        public string Task { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Reminder Date")]
        [DataType(DataType.DateTime)]
        public DateTime ReminderDate { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; } = "Pending";

        // For display
        public string? PlantName { get; set; }
    }
}