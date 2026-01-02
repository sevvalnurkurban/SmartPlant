using Microsoft.EntityFrameworkCore;
using SmartPlant.Data;
using SmartPlant.Helpers;
using SmartPlant.Models.Entities;
using SmartPlant.Services.Interfaces;

namespace SmartPlant.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;

        public AdminService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Admin?> CreateAdminAsync(string name, string surname, string email, string username, string password)
        {
            // Check if email already exists
            if (await EmailExistsAsync(email))
                return null;

            // Check if username already exists
            if (await UsernameExistsAsync(username))
                return null;

            // Create new admin
            var admin = new Admin
            {
                Name = name,
                Surname = surname,
                Email = email,
                Username = username,
                Password = PasswordHelper.HashPassword(password),
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };

            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            return admin;
        }

        public async Task<Admin?> GetByIdAsync(int id)
        {
            return await _context.Admins
                .FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted != true);
        }

        public async Task<Admin?> GetByEmailAsync(string email)
        {
            return await _context.Admins
                .FirstOrDefaultAsync(a => a.Email == email && a.IsDeleted != true);
        }

        public async Task<Admin?> GetByUsernameAsync(string username)
        {
            return await _context.Admins
                .FirstOrDefaultAsync(a => a.Username == username && a.IsDeleted != true);
        }

        public async Task<IEnumerable<Admin>> GetAllAdminsAsync()
        {
            return await _context.Admins
                .Where(a => a.IsDeleted != true)
                .OrderByDescending(a => a.CreatedDate)
                .ToListAsync();
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Admins
                .AnyAsync(a => a.Email == email && a.IsDeleted != true);
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.Admins
                .AnyAsync(a => a.Username == username && a.IsDeleted != true);
        }
    }
}