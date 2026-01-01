using SmartPlant.Models.Entities;

namespace SmartPlant.Services.Interfaces
{
    public interface IUserPlantService
    {
        Task<UserPlant?> GetByIdAsync(int id);
        Task<IEnumerable<UserPlant>> GetUserPlantsAsync(int userId, int page = 1, int pageSize = 10);
        Task<int> GetUserPlantsCountAsync(int userId);
        Task<UserPlant?> AddPlantToUserAsync(int userId, int plantId, DateTime? lastWatered, DateTime? nextWatering, string? status);
        Task<bool> UpdateUserPlantAsync(int id, DateTime? lastWatered, DateTime? nextWatering, string? status);
        Task<bool> DeleteUserPlantAsync(int id);
    }
}
