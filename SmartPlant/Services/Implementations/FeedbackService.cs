using Microsoft.EntityFrameworkCore;
using SmartPlant.Data;
using SmartPlant.Models.Entities;
using SmartPlant.Services.Interfaces;

namespace SmartPlant.Services.Implementations
{
    public class FeedbackService : IFeedbackService
    {
        private readonly ApplicationDbContext _context;

        public FeedbackService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserFeedback?> CreateFeedbackAsync(int userId, string feedback)
        {
            var userFeedback = new UserFeedback
            {
                UserId = userId,
                Feedback = feedback,
                CreatedAt = DateTime.Now
            };

            _context.UserFeedbacks.Add(userFeedback);
            await _context.SaveChangesAsync();

            return userFeedback;
        }

        public async Task<IEnumerable<UserFeedback>> GetAllFeedbacksAsync()
        {
            return await _context.UserFeedbacks
                .Include(f => f.User)
                .Where(f => !f.IsDeleted)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();
        }

        public async Task<bool> DeleteFeedbackAsync(int id)
        {
            var feedback = await _context.UserFeedbacks.FindAsync(id);
            if (feedback == null)
                return false;

            feedback.IsDeleted = true;
            _context.UserFeedbacks.Update(feedback);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
