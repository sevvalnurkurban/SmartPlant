using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartPlant.Data;
using SmartPlant.Helpers;
using SmartPlant.Models.ViewModels;
using SmartPlant.Models.Entities;
using SmartPlant.Services.Interfaces;
using System.Security.Claims;

namespace SmartPlant.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPlantService _plantService;
        private readonly IUserService _userService;
        private readonly IFeedbackService _feedbackService;

        public AdminController(ApplicationDbContext context, IPlantService plantService, IUserService userService, IFeedbackService feedbackService)
        {
            _context = context;
            _plantService = plantService;
            _userService = userService;
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true && User.IsInRole("Admin"))
                return RedirectToAction("Dashboard");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var admin = await _context.Admins
                .FirstOrDefaultAsync(a => (a.Email == model.EmailOrUsername || a.Username == model.EmailOrUsername) && !a.IsDeleted);

            if (admin == null || !PasswordHelper.VerifyPassword(model.Password, admin.Password))
            {
                ModelState.AddModelError("", "Invalid credentials");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
                new Claim(ClaimTypes.Name, admin.Username),
                new Claim(ClaimTypes.Email, admin.Email),
                new Claim("FullName", $"{admin.Name} {admin.Surname}"),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, // Cookie will persist across browser sessions
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30) // Cookie expires in 30 days
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Dashboard");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ManagePlants(string? sortBy)
        {
            var plants = await _plantService.GetAllPlantsAsync();

            if (!string.IsNullOrEmpty(sortBy))
            {
                plants = sortBy.ToLower() switch
                {
                    "name" => plants.OrderBy(p => p.Name),
                    "type" => plants.OrderBy(p => p.Type),
                    _ => plants.OrderBy(p => p.Name)
                };
            }

            ViewBag.SortBy = sortBy;
            return View(plants);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreatePlant()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePlant(PlantViewModel model, string? removePhoto = null)
        {
            if (!ModelState.IsValid)
                return View(model);

            string? photoUrl = null;

            // Only upload photo if removePhoto is not set and a photo file exists
            if (string.IsNullOrEmpty(removePhoto) || removePhoto != "true")
            {
                if (model.Photo != null)
                {
                    photoUrl = await FileUploadHelper.UploadFileAsync(model.Photo, "uploads/plants");
                }
            }

            await _plantService.CreatePlantAsync(model.Name, model.Type, model.WaterPeriod, model.Light, model.Temperature, model.Description, photoUrl);

            TempData["Success"] = "Plant created successfully!";
            return RedirectToAction("ManagePlants");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditPlant(int id)
        {
            var plant = await _plantService.GetByIdAsync(id);
            if (plant == null)
                return NotFound();

            var model = new PlantViewModel
            {
                Id = plant.Id,
                Name = plant.Name,
                Type = plant.Type,
                WaterPeriod = plant.WaterPeriod,
                Light = plant.Light,
                Temperature = plant.Temperature,
                Description = plant.Description,
                PhotoUrl = plant.PhotoUrl
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPlant(PlantViewModel model, string? removePhoto = null)
        {
            System.Diagnostics.Debug.WriteLine($"EditPlant called - removePhoto parameter: '{removePhoto}'");
            System.Diagnostics.Debug.WriteLine($"model.PhotoUrl: '{model.PhotoUrl}'");

            if (!ModelState.IsValid)
                return View(model);

            string? photoUrl;

            // Check if photo should be removed
            if (!string.IsNullOrEmpty(removePhoto) && removePhoto == "true")
            {
                System.Diagnostics.Debug.WriteLine("REMOVING PHOTO - setting photoUrl to null");
                photoUrl = null;
            }
            // Check if new photo uploaded
            else if (model.Photo != null)
            {
                System.Diagnostics.Debug.WriteLine("NEW PHOTO UPLOADED");
                photoUrl = await FileUploadHelper.UploadFileAsync(model.Photo, "uploads/plants");
            }
            // Keep existing photo
            else
            {
                photoUrl = string.IsNullOrEmpty(model.PhotoUrl) ? null : model.PhotoUrl;
            }

            System.Diagnostics.Debug.WriteLine($"Final photoUrl being saved: '{(photoUrl == null ? "NULL" : photoUrl)}'");
            await _plantService.UpdatePlantAsync(model.Id, model.Name, model.Type, model.WaterPeriod, model.Light, model.Temperature, model.Description, photoUrl);

            TempData["Success"] = "Plant updated successfully!";
            return RedirectToAction("ManagePlants");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePlant(int id)
        {
            await _plantService.DeletePlantAsync(id);
            TempData["Success"] = "Plant deleted successfully!";
            return RedirectToAction("ManagePlants");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ManageUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ViewUser(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("ManageUsers");
            }
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            TempData["Success"] = "User deleted successfully!";
            return RedirectToAction("ManageUsers");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ViewFeedback()
        {
            var feedbacks = await _feedbackService.GetAllFeedbacksAsync();
            return View(feedbacks);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var adminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Id == adminId && !a.IsDeleted);

            if (admin == null)
                return RedirectToAction("Login");

            var model = new ProfileViewModel
            {
                FullName = $"{admin.Name} {admin.Surname}",
                Name = admin.Name,
                Surname = admin.Surname,
                Email = admin.Email,
                Username = admin.Username,
                PhotoUrl = admin.PhotoUrl
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileViewModel model, IFormFile? photoFile, string? removePhoto = null)
        {
            if (!ModelState.IsValid)
                return View(model);

            var adminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            // Split FullName into Name and Surname
            var nameParts = model.FullName.Split(' ', 2);
            var firstName = nameParts[0];
            var lastName = nameParts.Length > 1 ? nameParts[1] : "";

            // Get current admin to preserve existing photo if needed
            var currentAdmin = await _context.Admins.FirstOrDefaultAsync(a => a.Id == adminId && !a.IsDeleted);

            if (currentAdmin == null)
                return RedirectToAction("Login");

            // Handle photo removal
            string? photoUrl = currentAdmin.PhotoUrl;
            bool shouldRemovePhoto = removePhoto?.ToLower() == "true";

            if (shouldRemovePhoto)
            {
                photoUrl = null;
            }
            // Handle photo upload
            else if (photoFile != null && photoFile.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var fileExtension = Path.GetExtension(photoFile.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("", "Only image files are allowed");
                    return View(model);
                }

                if (photoFile.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("", "File size must be less than 5MB");
                    return View(model);
                }

                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "profiles");
                if (!Directory.Exists(uploadsPath))
                    Directory.CreateDirectory(uploadsPath);

                var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                var filePath = Path.Combine(uploadsPath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photoFile.CopyToAsync(stream);
                }

                photoUrl = "/uploads/profiles/" + uniqueFileName;
            }

            // Update admin profile
            currentAdmin.Name = firstName;
            currentAdmin.Surname = lastName;
            currentAdmin.Username = model.Username;
            currentAdmin.Email = model.Email;
            currentAdmin.PhotoUrl = photoUrl;

            // Update password if provided
            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                currentAdmin.Password = PasswordHelper.HashPassword(model.Password);
            }

            _context.Admins.Update(currentAdmin);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Profile updated successfully!";
            return RedirectToAction("Profile");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Admin");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var adminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Id == adminId && !a.IsDeleted);

            if (admin == null)
            {
                TempData["Error"] = "Admin not found";
                return View(model);
            }

            // Verify current password
            if (!PasswordHelper.VerifyPassword(model.CurrentPassword, admin.Password))
            {
                TempData["Error"] = "Current password is incorrect";
                return View(model);
            }

            // Update password
            admin.Password = PasswordHelper.HashPassword(model.NewPassword);
            _context.Admins.Update(admin);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Password changed successfully!";
            return RedirectToAction("Profile");
        }
    }
}