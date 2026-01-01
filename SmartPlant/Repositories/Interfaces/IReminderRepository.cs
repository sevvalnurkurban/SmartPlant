using SmartPlant.Models.Entities;

namespace SmartPlant.Repositories.Interfaces
{
    public interface IReminderRepository : IRepository<Reminder>
    {
        Task<IEnumerable<Reminder>> GetUserRemindersAsync(int userId);
        Task<IEnumerable<Reminder>> GetPendingRemindersAsync(int userId);
        Task<IEnumerable<Reminder>> SearchRemindersAsync(int userId, string searchTerm);
    }
}
