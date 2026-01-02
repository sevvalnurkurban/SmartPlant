using Microsoft.EntityFrameworkCore;
using SmartPlant.Data;
using SmartPlant.Helpers;
using SmartPlant.Models.Entities;
using SmartPlant.Repositories.Interfaces;
using SmartPlant.Services.Interfaces;

namespace SmartPlant.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public UserService(IUserRepository userRepository, ApplicationDbContext context, IEmailService emailService)
        {
            _userRepository = userRepository;
            _context = context;
            _emailService = emailService;
        }

        public async Task<User?> RegisterAsync(string name, string surname, string email, string password)
        {
            // Check if email exists
            if (await _userRepository.EmailExistsAsync(email))
                return null;

            // Generate unique username
            var username = UsernameGenerator.GenerateUnique(name, surname,
                un => _userRepository.UsernameExistsAsync(un).Result);

            // Create user
            var user = new User
            {
                Name = name,
                Surname = surname,
                Email = email,
                Username = username,
                Password = PasswordHelper.HashPassword(password),
                CreatedDate = DateTime.Now
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return user;
        }

        public async Task<User?> LoginAsync(string emailOrUsername, string password)
        {
            // Try to find by email or username
            var user = await _userRepository.GetByEmailAsync(emailOrUsername);
            if (user == null)
                user = await _userRepository.GetByUsernameAsync(emailOrUsername);

            if (user == null)
                return null;

            // Verify password
            if (!PasswordHelper.VerifyPassword(password, user.Password))
                return null;

            return user;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task<bool> UpdateProfileAsync(int userId, string name, string surname, string? bio, string? photoUrl)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return false;

            user.Name = name;
            user.Surname = surname;
            user.Bio = bio;
            user.PhotoUrl = photoUrl;

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return false;

            if (!PasswordHelper.VerifyPassword(oldPassword, user.Password))
                return false;

            user.Password = PasswordHelper.HashPassword(newPassword);
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RequestPasswordResetAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
                return false;

            var token = PasswordHelper.GenerateResetToken();
            var resetToken = new PasswordResetToken
            {
                UserId = user.Id,
                Token = token,
                ExpiresAt = DateTime.Now.AddHours(24),
                CreatedDate = DateTime.Now
            };

            _context.PasswordResetTokens.Add(resetToken);
            await _context.SaveChangesAsync();

            // Send password reset email
            await _emailService.SendPasswordResetEmailAsync(user.Email, token);

            return true;
        }

        public async Task<bool> ResetPasswordAsync(string token, string newPassword)
        {
            var resetToken = await _context.PasswordResetTokens
                .FirstOrDefaultAsync(t => t.Token == token && !t.IsUsed && t.ExpiresAt > DateTime.Now);

            if (resetToken == null)
                return false;

            var user = await _userRepository.GetByIdAsync(resetToken.UserId!.Value);
            if (user == null)
                return false;

            user.Password = PasswordHelper.HashPassword(newPassword);
            resetToken.IsUsed = true;

            _userRepository.Update(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetActiveUsersAsync();
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return false;

            user.IsDeleted = true;
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return true;
        }

        public async Task SetEmailVerificationTokenAsync(int userId, string token)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                user.EmailVerificationToken = token;
                user.EmailVerificationTokenExpiry = DateTime.UtcNow.AddHours(24);
                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();
            }
        }

        public async Task<bool> VerifyEmailAsync(string token)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.EmailVerificationToken == token
                    && u.EmailVerificationTokenExpiry > DateTime.UtcNow
                    && !u.IsDeleted);

            if (user == null)
                return false;

            user.IsEmailVerified = true;
            user.EmailVerificationToken = null;
            user.EmailVerificationTokenExpiry = null;

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return true;
        }

        public async Task SetEmailVerificationOtpAsync(int userId, string otpCode)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                user.EmailVerificationOtp = otpCode;
                user.EmailVerificationOtpExpiry = DateTime.UtcNow.AddMinutes(15);
                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();
            }
        }

        public async Task<bool> VerifyEmailOtpAsync(string email, string otpCode)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email
                    && u.EmailVerificationOtp == otpCode
                    && u.EmailVerificationOtpExpiry > DateTime.UtcNow
                    && !u.IsDeleted);

            if (user == null)
                return false;

            user.IsEmailVerified = true;
            user.EmailVerificationOtp = null;
            user.EmailVerificationOtpExpiry = null;

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return true;
        }
    }
}
