namespace SmartPlant.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string toEmail, string subject, string body);
        Task<bool> SendPasswordResetEmailAsync(string toEmail, string resetToken);
        Task<bool> SendEmailVerificationAsync(string toEmail, string verificationToken);
        Task<bool> SendEmailVerificationOtpAsync(string toEmail, string otpCode);
    }
}