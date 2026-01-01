using Microsoft.EntityFrameworkCore;
using SmartPlant.Data;
using SmartPlant.Models.Entities;
using SmartPlant.Services.Interfaces;

namespace SmartPlant.Services.Implementations
{
    public class UserPlantService : IUserPlantService
    {
        private readonly ApplicationDbContext _context;

        public UserPlantService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserPlant?> GetByIdAsync(int id)
        {
            return await _context.UserPlants
                .Include(up => up.Plant)
                .FirstOrDefaultAsync(up => up.Id == id && !up.IsDeleted);
        }

        public async Task<IEnumerable<UserPlant>> GetUserPlantsAsync(int userId, int page = 1, int pageSize = 10)
        {
            return await _context.UserPlants
                .Include(up => up.Plant)
                .Where(up => up.UserId == userId && !up.IsDeleted)
                .OrderByDescending(up => up.CreatedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetUserPlantsCountAsync(int userId)
        {
            return await _context.UserPlants
                .CountAsync(up => up.UserId == userId && !up.IsDeleted);
        }

        public async Task<UserPlant?> AddPlantToUserAsync(int userId, int plantId, DateTime? lastWatered, DateTime? nextWatering, string? status)
        {
            var userPlant = new UserPlant
            {
                UserId = userId,
                PlantId = plantId,
                LastWatered = lastWatered,
                NextWatering = nextWatering,
                Status = status,
                CreatedDate = DateTime.Now
            };

            _context.UserPlants.Add(userPlant);
            await _context.SaveChangesAsync();

            return userPlant;
        }

        public async Task<bool> UpdateUserPlantAsync(int id, DateTime? lastWatered, DateTime? nextWatering, string? status)
        {
            var userPlant = await GetByIdAsync(id);
            if (userPlant == null)
                return false;

            userPlant.LastWatered = lastWatered;
            userPlant.NextWatering = nextWatering;
            userPlant.Status = status;

            _context.UserPlants.Update(userPlant);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteUserPlantAsync(int id)
        {
            var userPlant = await GetByIdAsync(id);
            if (userPlant == null)
                return false;

            userPlant.IsDeleted = true;
            _context.UserPlants.Update(userPlant);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
