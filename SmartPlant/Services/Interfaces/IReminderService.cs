using SmartPlant.Models.Entities;

namespace SmartPlant.Services.Interfaces
{
    public interface IReminderService
    {
        Task<Reminder?> GetByIdAsync(int id);
        Task<IEnumerable<Reminder>> GetUserRemindersAsync(int userId);
        Task<IEnumerable<Reminder>> GetPendingRemindersAsync(int userId);
        Task<IEnumerable<Reminder>> SearchRemindersAsync(int userId, string searchTerm);
        Task<Reminder?> CreateReminderAsync(int userId, int plantId, string task, DateTime reminderDate);
        Task<bool> UpdateReminderStatusAsync(int id, string status);
        Task<bool> DeleteReminderAsync(int id);
    }
}
