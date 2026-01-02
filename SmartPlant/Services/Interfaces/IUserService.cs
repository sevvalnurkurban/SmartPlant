using SmartPlant.Models.Entities;

namespace SmartPlant.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> RegisterAsync(string name, string surname, string email, string password);
        Task<User?> LoginAsync(string emailOrUsername, string password);
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<bool> UpdateProfileAsync(int userId, string name, string surname, string? bio, string? photoUrl);
        Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword);
        Task<bool> RequestPasswordResetAsync(string email);
        Task<bool> ResetPasswordAsync(string token, string newPassword);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(int userId);
        Task SetEmailVerificationTokenAsync(int userId, string token);
        Task<bool> VerifyEmailAsync(string token);
        Task SetEmailVerificationOtpAsync(int userId, string otpCode);
        Task<bool> VerifyEmailOtpAsync(string email, string otpCode);
    }
}
