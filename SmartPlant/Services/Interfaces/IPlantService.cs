using SmartPlant.Models.Entities;

namespace SmartPlant.Services.Interfaces
{
    public interface IPlantService
    {
        Task<Plant?> GetByIdAsync(int id);
        Task<IEnumerable<Plant>> GetAllPlantsAsync();
        Task<IEnumerable<Plant>> SearchPlantsAsync(string searchTerm);
        Task<Plant?> CreatePlantAsync(string name, string? type, string? waterPeriod, string? light, string? temperature, string? description, string? photoUrl);
        Task<bool> UpdatePlantAsync(int id, string name, string? type, string? waterPeriod, string? light, string? temperature, string? description, string? photoUrl);
        Task<bool> DeletePlantAsync(int id);
    }
}
