using Microsoft.EntityFrameworkCore;
using SmartPlant.Data;
using SmartPlant.Models.Entities;
using SmartPlant.Repositories.Interfaces;

namespace SmartPlant.Repositories.Implementations
{
    public class ReminderRepository : Repository<Reminder>, IReminderRepository
    {
        public ReminderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Reminder>> GetUserRemindersAsync(int userId)
        {
            return await _dbSet
                .Include(r => r.Plant)
                .Where(r => r.UserId == userId && !r.IsDeleted && r.Plant != null && !r.Plant.IsDeleted)
                .OrderByDescending(r => r.ReminderDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reminder>> GetPendingRemindersAsync(int userId)
        {
            return await _dbSet
                .Include(r => r.Plant)
                .Where(r => r.UserId == userId &&
                           !r.IsDeleted &&
                           r.Status == "Pending" &&
                           r.Plant != null && !r.Plant.IsDeleted)
                .OrderBy(r => r.ReminderDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reminder>> SearchRemindersAsync(int userId, string searchTerm)
        {
            return await _dbSet
                .Include(r => r.Plant)
                .Where(r => r.UserId == userId &&
                           !r.IsDeleted &&
                           r.Plant != null && !r.Plant.IsDeleted &&
                           (r.Task!.Contains(searchTerm) ||
                            r.Plant!.Name.Contains(searchTerm)))
                .OrderByDescending(r => r.ReminderDate)
                .ToListAsync();
        }
    }
}
