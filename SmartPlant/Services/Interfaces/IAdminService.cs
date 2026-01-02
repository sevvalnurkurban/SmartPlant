using SmartPlant.Models.Entities;

namespace SmartPlant.Services.Interfaces
{
    public interface IAdminService
    {
        Task<Admin?> CreateAdminAsync(string name, string surname, string email, string username, string password);
        Task<Admin?> GetByIdAsync(int id);
        Task<Admin?> GetByEmailAsync(string email);
        Task<Admin?> GetByUsernameAsync(string username);
        Task<IEnumerable<Admin>> GetAllAdminsAsync();
        Task<bool> EmailExistsAsync(string email);
        Task<bool> UsernameExistsAsync(string username);
    }
}