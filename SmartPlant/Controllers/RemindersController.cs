using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartPlant.Models.ViewModels;
using SmartPlant.Services.Interfaces;
using System.Security.Claims;

namespace SmartPlant.Controllers
{
    [Authorize]
    public class RemindersController : Controller
    {
        private readonly IReminderService _reminderService;
        private readonly IPlantService _plantService;

        public RemindersController(IReminderService reminderService, IPlantService plantService)
        {
            _reminderService = reminderService;
            _plantService = plantService;
        }

        public async Task<IActionResult> Index(string? search, string? filterStatus)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var reminders = string.IsNullOrEmpty(search)
                ? await _reminderService.GetUserRemindersAsync(userId)
                : await _reminderService.SearchRemindersAsync(userId, search);

            // Filter by status
            if (!string.IsNullOrEmpty(filterStatus) && filterStatus != "All")
            {
                reminders = reminders.Where(r => r.Status == filterStatus);
            }

            ViewBag.Search = search;
            ViewBag.FilterStatus = filterStatus;
            return View(reminders);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var plants = await _plantService.GetAllPlantsAsync();
            ViewBag.Plants = new SelectList(plants, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReminderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var plants = await _plantService.GetAllPlantsAsync();
                ViewBag.Plants = new SelectList(plants, "Id", "Name");
                return View(model);
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _reminderService.CreateReminderAsync(userId, model.PlantId, model.Task, model.ReminderDate);

            TempData["Success"] = "Reminder created successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsDone(int id)
        {
            await _reminderService.UpdateReminderStatusAsync(id, "Done");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsPending(int id)
        {
            await _reminderService.UpdateReminderStatusAsync(id, "Pending");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _reminderService.DeleteReminderAsync(id);
            TempData["Success"] = "Reminder deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}