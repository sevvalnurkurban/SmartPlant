using SmartPlant.Models.Entities;

namespace SmartPlant.Repositories.Interfaces
{
    public interface IPlantRepository : IRepository<Plant>
    {
        Task<IEnumerable<Plant>> GetActivePlantsAsync();
        Task<IEnumerable<Plant>> SearchPlantsAsync(string searchTerm);
        Task<IEnumerable<Plant>> GetPlantsByTypeAsync(string type);
    }
}
