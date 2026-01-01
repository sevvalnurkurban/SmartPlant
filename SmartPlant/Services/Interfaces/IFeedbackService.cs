using SmartPlant.Models.Entities;

namespace SmartPlant.Services.Interfaces
{
    public interface IFeedbackService
    {
        Task<UserFeedback?> CreateFeedbackAsync(int userId, string feedback);
        Task<IEnumerable<UserFeedback>> GetAllFeedbacksAsync();
        Task<bool> DeleteFeedbackAsync(int id);
    }
}
