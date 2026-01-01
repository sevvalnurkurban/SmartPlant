using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartPlant.Models.ViewModels;
using SmartPlant.Services.Interfaces;
using System.Security.Claims;

namespace SmartPlant.Controllers
{
    [Authorize]
    public class UserPlantsController : Controller
    {
        private readonly IUserPlantService _userPlantService;
        private readonly IPlantService _plantService;
        private readonly IReminderService _reminderService;

        public UserPlantsController(IUserPlantService userPlantService, IPlantService plantService, IReminderService reminderService)
        {
            _userPlantService = userPlantService;
            _plantService = plantService;
            _reminderService = reminderService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var pageSize = 10;

            var userPlants = await _userPlantService.GetUserPlantsAsync(userId, page, pageSize);
            var totalCount = await _userPlantService.GetUserPlantsCountAsync(userId);

            // Convert entities to ViewModels
            var viewModels = userPlants.Select(up => new UserPlantViewModel
            {
                Id = up.Id,
                PlantId = up.PlantId,
                PlantName = up.Plant?.Name ?? "Unknown Plant",
                PlantPhotoUrl = up.Plant?.PhotoUrl,
                LastWatered = up.LastWatered,
                NextWatering = up.NextWatering,
                Status = up.Status
            }).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            // Get pending reminders for sidebar
            var allReminders = await _reminderService.GetUserRemindersAsync(userId);
            var pendingReminders = allReminders
                .Where(r => r.Status == "Pending" && r.ReminderDate.HasValue && r.ReminderDate.Value.Date <= DateTime.Now.Date.AddDays(1))
                .OrderBy(r => r.ReminderDate)
                .ToList();
            ViewBag.PendingReminders = pendingReminders;

            return View(viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> SelectPlant()
        {
            var plants = await _plantService.GetAllPlantsAsync();
            ViewBag.Plants = plants;
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string name, string? type, string? waterPeriod, string? light, string? tempMin, string? tempMax, string? description, IFormFile? photoFile)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ModelState.AddModelError("", "Plant name is required");
                return View();
            }

            // Combine temperature values
            string? temperature = null;
            if (!string.IsNullOrWhiteSpace(tempMin) && !string.IsNullOrWhiteSpace(tempMax))
            {
                temperature = $"{tempMin}-{tempMax}°C";
            }
            else if (!string.IsNullOrWhiteSpace(tempMin))
            {
                temperature = $"{tempMin}°C";
            }
            else if (!string.IsNullOrWhiteSpace(tempMax))
            {
                temperature = $"{tempMax}°C";
            }

            string? photoUrl = null;

            // Handle file upload
            if (photoFile != null && photoFile.Length > 0)
            {
                // Check file extension
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var fileExtension = Path.GetExtension(photoFile.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("", "Only image files (jpg, jpeg, png, gif) are allowed");
                    return View();
                }

                // Check file size (max 5MB)
                if (photoFile.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("", "File size must be less than 5MB");
                    return View();
                }

                // Create uploads directory if it doesn't exist
                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsPath))
                {
                    Directory.CreateDirectory(uploadsPath);
                }

                // Generate unique filename
                var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                var filePath = Path.Combine(uploadsPath, uniqueFileName);

                // Save file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photoFile.CopyToAsync(stream);
                }

                photoUrl = "/uploads/" + uniqueFileName;
            }

            await _plantService.CreatePlantAsync(name, type, waterPeriod, light, temperature, description, photoUrl);

            TempData["Success"] = "New plant added to database successfully!";
            return RedirectToAction("SelectPlant");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPlantToCollection(int plantId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            // Get plant details to determine watering frequency
            var plant = await _plantService.GetByIdAsync(plantId);

            // Calculate next watering based on plant's watering frequency
            int daysUntilNextWatering = 7; // Default
            if (plant?.WaterPeriod != null)
            {
                daysUntilNextWatering = plant.WaterPeriod switch
                {
                    "Daily" => 1,
                    "Every 2 days" => 2,
                    "Every 3 days" => 3,
                    "Weekly" => 7,
                    "Bi-weekly" => 14,
                    "Monthly" => 30,
                    _ => 7
                };
            }

            // Add plant with calculated next watering date
            await _userPlantService.AddPlantToUserAsync(
                userId,
                plantId,
                DateTime.Now, // LastWatered = today
                DateTime.Now.AddDays(daysUntilNextWatering), // NextWatering based on frequency
                "Healthy" // Default status
            );

            // Create automatic reminder for watering
            var nextWateringDate = DateTime.Now.AddDays(daysUntilNextWatering);
            await _reminderService.CreateReminderAsync(
                userId,
                plantId,
                "Water",
                nextWateringDate
            );

            TempData["Success"] = "Plant added to your collection successfully!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var userPlant = await _userPlantService.GetByIdAsync(id);
            if (userPlant == null)
                return NotFound();

            var model = new UserPlantViewModel
            {
                Id = userPlant.Id,
                PlantId = userPlant.PlantId,
                PlantName = userPlant.Plant?.Name ?? "Unknown Plant",
                PlantPhotoUrl = userPlant.Plant?.PhotoUrl,
                LastWatered = userPlant.LastWatered,
                NextWatering = userPlant.NextWatering,
                Status = userPlant.Status
            };

            // Pass plant data to ViewBag for displaying
            ViewBag.PlantType = userPlant.Plant?.Type;
            ViewBag.WaterPeriod = userPlant.Plant?.WaterPeriod;
            ViewBag.WaterMlPerWeek = userPlant.Plant?.WaterMlPerWeek;
            ViewBag.Light = userPlant.Plant?.Light;
            ViewBag.Temperature = userPlant.Plant?.Temperature;
            ViewBag.Description = userPlant.Plant?.Description;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _userPlantService.DeleteUserPlantAsync(id);
            TempData["Success"] = "Plant deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}