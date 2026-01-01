using System.ComponentModel.DataAnnotations;

namespace SmartPlant.Models.ViewModels
{
    public class PreferencesViewModel
    {
        [Required(ErrorMessage = "Reminder time is required")]
        [Display(Name = "Reminder Time")]
        [RegularExpression(@"^([01]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time format (HH:MM)")]
        public string ReminderTime { get; set; } = "09:00";

        [Required(ErrorMessage = "Time zone is required")]
        [Display(Name = "Time Zone")]
        public string TimeZone { get; set; } = "Turkey Standard Time";

        [Required(ErrorMessage = "Reminder frequency is required")]
        [Display(Name = "Reminder Frequency")]
        public string ReminderFrequency { get; set; } = "Daily";

        [Display(Name = "Email Notifications")]
        public bool EmailNotificationsEnabled { get; set; } = true;

        [Display(Name = "In-App Notifications")]
        public bool InAppNotificationsEnabled { get; set; } = true;
    }
}