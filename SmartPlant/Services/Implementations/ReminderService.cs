using SmartPlant.Models.Entities;
using SmartPlant.Repositories.Interfaces;
using SmartPlant.Services.Interfaces;

namespace SmartPlant.Services.Implementations
{
    public class ReminderService : IReminderService
    {
        private readonly IReminderRepository _reminderRepository;

        public ReminderService(IReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }

        public async Task<Reminder?> GetByIdAsync(int id)
        {
            return await _reminderRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Reminder>> GetUserRemindersAsync(int userId)
        {
            return await _reminderRepository.GetUserRemindersAsync(userId);
        }

        public async Task<IEnumerable<Reminder>> GetPendingRemindersAsync(int userId)
        {
            return await _reminderRepository.GetPendingRemindersAsync(userId);
        }

        public async Task<IEnumerable<Reminder>> SearchRemindersAsync(int userId, string searchTerm)
        {
            return await _reminderRepository.SearchRemindersAsync(userId, searchTerm);
        }

        public async Task<Reminder?> CreateReminderAsync(int userId, int plantId, string task, DateTime reminderDate)
        {
            var reminder = new Reminder
            {
                UserId = userId,
                PlantId = plantId,
                Task = task,
                ReminderDate = reminderDate,
                Status = "Pending",
                CreatedDate = DateTime.Now
            };

            await _reminderRepository.AddAsync(reminder);
            await _reminderRepository.SaveChangesAsync();

            return reminder;
        }

        public async Task<bool> UpdateReminderStatusAsync(int id, string status)
        {
            var reminder = await _reminderRepository.GetByIdAsync(id);
            if (reminder == null)
                return false;

            reminder.Status = status;
            _reminderRepository.Update(reminder);
            await _reminderRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteReminderAsync(int id)
        {
            var reminder = await _reminderRepository.GetByIdAsync(id);
            if (reminder == null)
                return false;

            reminder.IsDeleted = true;
            _reminderRepository.Update(reminder);
            await _reminderRepository.SaveChangesAsync();

            return true;
        }
    }
}
