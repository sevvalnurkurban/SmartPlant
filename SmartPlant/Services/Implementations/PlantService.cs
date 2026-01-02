using Microsoft.EntityFrameworkCore;
using SmartPlant.Data;
using SmartPlant.Models.Entities;
using SmartPlant.Repositories.Interfaces;
using SmartPlant.Services.Interfaces;

namespace SmartPlant.Services.Implementations
{
    public class PlantService : IPlantService
    {
        private readonly IPlantRepository _plantRepository;
        private readonly ApplicationDbContext _context;

        public PlantService(IPlantRepository plantRepository, ApplicationDbContext context)
        {
            _plantRepository = plantRepository;
            _context = context;
        }

        public async Task<Plant?> GetByIdAsync(int id)
        {
            return await _plantRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Plant>> GetAllPlantsAsync()
        {
            return await _plantRepository.GetActivePlantsAsync();
        }

        public async Task<IEnumerable<Plant>> SearchPlantsAsync(string searchTerm)
        {
            return await _plantRepository.SearchPlantsAsync(searchTerm);
        }

        public async Task<Plant?> CreatePlantAsync(string name, string? type, string? waterPeriod, string? light, string? temperature, string? description, string? photoUrl)
        {
            var plant = new Plant
            {
                Name = name,
                Type = type,
                WaterPeriod = waterPeriod,
                Light = light,
                Temperature = temperature,
                Description = description,
                PhotoUrl = photoUrl,
                CreatedDate = DateTime.Now
            };

            await _plantRepository.AddAsync(plant);
            await _plantRepository.SaveChangesAsync();

            return plant;
        }

        public async Task<bool> UpdatePlantAsync(int id, string name, string? type, string? waterPeriod, string? light, string? temperature, string? description, string? photoUrl)
        {
            var plant = await _plantRepository.GetByIdAsync(id);
            if (plant == null)
                return false;

            plant.Name = name;
            plant.Type = type;
            plant.WaterPeriod = waterPeriod;
            plant.Light = light;
            plant.Temperature = temperature;
            plant.Description = description;
            plant.PhotoUrl = photoUrl; // Always update photo URL (can be null to remove photo)

            _plantRepository.Update(plant);
            await _plantRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeletePlantAsync(int id)
        {
            var plant = await _plantRepository.GetByIdAsync(id);
            if (plant == null)
                return false;

            // Soft delete the plant
            plant.IsDeleted = true;
            _plantRepository.Update(plant);

            // Soft delete all UserPlants associated with this plant
            var userPlants = await _context.UserPlants
                .Where(up => up.PlantId == id && up.IsDeleted != true)
                .ToListAsync();

            foreach (var userPlant in userPlants)
            {
                userPlant.IsDeleted = true;
            }

            // Soft delete all Reminders associated with this plant
            var reminders = await _context.Reminders
                .Where(r => r.PlantId == id && r.IsDeleted != true)
                .ToListAsync();

            foreach (var reminder in reminders)
            {
                reminder.IsDeleted = true;
            }

            await _plantRepository.SaveChangesAsync();

            return true;
        }
    }
}
