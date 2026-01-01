using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPlant.Services.Interfaces;
using System.Security.Claims;

namespace SmartPlant.Controllers
{
    [Authorize]
    public class FeedbackController : Controller
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string feedback)
        {
            if (string.IsNullOrWhiteSpace(feedback))
            {
                ModelState.AddModelError("", "Feedback cannot be empty");
                return View();
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _feedbackService.CreateFeedbackAsync(userId, feedback);

            TempData["Success"] = "Thank you for your feedback!";
            return RedirectToAction("Index", "UserPlants");
        }
    }
}