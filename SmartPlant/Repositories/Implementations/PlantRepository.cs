using Microsoft.EntityFrameworkCore;
using SmartPlant.Data;
using SmartPlant.Models.Entities;
using SmartPlant.Repositories.Interfaces;

namespace SmartPlant.Repositories.Implementations
{
    public class PlantRepository : Repository<Plant>, IPlantRepository
    {
        public PlantRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Plant>> GetActivePlantsAsync()
        {
            return await _dbSet
                .Where(p => !p.IsDeleted)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Plant>> SearchPlantsAsync(string searchTerm)
        {
            return await _dbSet
                .Where(p => !p.IsDeleted &&
                           (p.Name.Contains(searchTerm) ||
                            p.Type!.Contains(searchTerm) ||
                            p.Description!.Contains(searchTerm)))
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Plant>> GetPlantsByTypeAsync(string type)
        {
            return await _dbSet
                .Where(p => !p.IsDeleted && p.Type == type)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }
    }
}
