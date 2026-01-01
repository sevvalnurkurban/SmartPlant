using System.ComponentModel.DataAnnotations;

namespace SmartPlant.Models.ViewModels
{
    public class VerifyOtpViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "OTP code is required")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "OTP must be exactly 6 digits")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "OTP must be 6 digits")]
        [Display(Name = "OTP Code")]
        public string OtpCode { get; set; } = string.Empty;
    }
}