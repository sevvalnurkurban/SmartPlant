using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPlant.Helpers;
using SmartPlant.Models.ViewModels;
using SmartPlant.Services.Interfaces;
using System.Security.Claims;

namespace SmartPlant.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public AccountController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "UserPlants");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userService.LoginAsync(model.EmailOrUsername, model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email/username or password");
                return View(model);
            }

            // Check if email is verified
            if (user.IsEmailVerified != true)
            {
                ModelState.AddModelError("", "Please verify your email address before logging in. Check your inbox for the verification code.");
                return View(model);
            }

            // Create claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("FullName", $"{user.Name} {user.Surname}"),
                new Claim(ClaimTypes.Role, "User")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe,
                ExpiresUtc = model.RememberMe ? DateTimeOffset.UtcNow.AddDays(30) : DateTimeOffset.UtcNow.AddHours(24)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index", "UserPlants");
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "UserPlants");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userService.RegisterAsync(model.Name, model.Surname, model.Email, model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Email already exists");
                return View(model);
            }

            // Generate 6-digit OTP code
            var random = new Random();
            var otpCode = random.Next(100000, 999999).ToString();

            // Set OTP with 15 minutes expiration
            await _userService.SetEmailVerificationOtpAsync(user.Id, otpCode);

            // Send OTP email
            await _emailService.SendEmailVerificationOtpAsync(user.Email, otpCode);

            TempData["Success"] = "Registration successful! Please check your email for the verification code.";
            return RedirectToAction("VerifyOtp", new { email = user.Email });
        }

        [HttpGet]
        public IActionResult RegisterConfirmation()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> VerifyEmail(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                ViewBag.Message = "Invalid verification link.";
                ViewBag.Success = false;
                return View();
            }

            var result = await _userService.VerifyEmailAsync(token);

            if (result)
            {
                ViewBag.Message = "Email verified successfully! You can now login.";
                ViewBag.Success = true;
            }
            else
            {
                ViewBag.Message = "Invalid or expired verification link.";
                ViewBag.Success = false;
            }

            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Please enter your email");
                return View();
            }

            await _userService.RequestPasswordResetAsync(email);

            // Always show confirmation page (security best practice)
            return View("ForgotPasswordConfirmation");
        }

        [HttpGet]
        public IActionResult ResetPassword(string token)
        {
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login");

            ViewBag.Token = token;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(string token, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                ModelState.AddModelError("", "Passwords do not match");
                ViewBag.Token = token;
                return View();
            }

            var result = await _userService.ResetPasswordAsync(token, password);

            if (!result)
            {
                ModelState.AddModelError("", "Invalid or expired token");
                return View();
            }

            TempData["Success"] = "Password reset successful! Please login.";
            return RedirectToAction("Login");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var user = await _userService.GetByIdAsync(userId);

            if (user == null)
                return RedirectToAction("Login");

            var model = new ProfileViewModel
            {
                FullName = $"{user.Name} {user.Surname}",
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Username = user.Username,
                PhotoUrl = user.PhotoUrl
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileViewModel model, IFormFile? photoFile, string? removePhoto = null)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            // Split FullName into Name and Surname
            var nameParts = model.FullName.Split(' ', 2);
            var firstName = nameParts[0];
            var lastName = nameParts.Length > 1 ? nameParts[1] : "";

            // Get current user to preserve existing photo if needed
            var currentUser = await _userService.GetByIdAsync(userId);

            // Handle photo removal
            string? photoUrl = currentUser?.PhotoUrl;

            // DEBUG: Log what we received
            Console.WriteLine($"DEBUG - removePhoto parameter: '{removePhoto}'");
            Console.WriteLine($"DEBUG - Current photoUrl: '{photoUrl}'");

            bool shouldRemovePhoto = removePhoto?.ToLower() == "true";
            Console.WriteLine($"DEBUG - shouldRemovePhoto: {shouldRemovePhoto}");

            if (shouldRemovePhoto)
            {
                photoUrl = null;
                Console.WriteLine("DEBUG - Setting photoUrl to null");
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

            // Update profile
            Console.WriteLine($"DEBUG - Final photoUrl being sent to service: '{photoUrl}'");
            var updateResult = await _userService.UpdateProfileAsync(userId, firstName, lastName, model.Username, photoUrl);

            if (!updateResult)
            {
                ModelState.AddModelError("", "Failed to update profile");
                return View(model);
            }

            // Update password if provided
            if (!string.IsNullOrEmpty(model.Password))
            {
                var user = await _userService.GetByIdAsync(userId);
                if (user != null)
                {
                    var passwordResult = await _userService.UpdateProfileAsync(userId, user.Name, user.Surname, user.Username, user.PhotoUrl);
                    // Note: You'll need to add password update logic in UserService
                }
            }

            TempData["Success"] = "Profile updated successfully!";
            return RedirectToAction("Profile");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var result = await _userService.ChangePasswordAsync(userId, model.CurrentPassword, model.NewPassword);

            if (!result)
            {
                TempData["Error"] = "Current password is incorrect";
                return View(model);
            }

            TempData["Success"] = "Password changed successfully!";
            return RedirectToAction("Profile");
        }

        [HttpGet]
        public IActionResult VerifyOtp(string email)
        {
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login");

            var model = new VerifyOtpViewModel
            {
                Email = email
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyOtp(VerifyOtpViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _userService.VerifyEmailOtpAsync(model.Email, model.OtpCode);

            if (!result)
            {
                TempData["Error"] = "Invalid or expired OTP code. Please try again.";
                return View(model);
            }

            // Get the user and automatically log them in
            var user = await _userService.GetByEmailAsync(model.Email);

            if (user != null)
            {
                // Create claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("FullName", $"{user.Name} {user.Surname}"),
                    new Claim(ClaimTypes.Role, "User")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = false,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(24)
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                TempData["Success"] = "Email verified successfully! Welcome to SmartPlant!";
                return RedirectToAction("Index", "UserPlants");
            }

            TempData["Success"] = "Email verified successfully! Please login.";
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> ResendOtp(string email)
        {
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login");

            var user = await _userService.GetByEmailAsync(email);
            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("Login");
            }

            // Generate new 6-digit OTP code
            var random = new Random();
            var otpCode = random.Next(100000, 999999).ToString();

            // Set OTP with 15 minutes expiration
            await _userService.SetEmailVerificationOtpAsync(user.Id, otpCode);

            // Send OTP email
            await _emailService.SendEmailVerificationOtpAsync(user.Email, otpCode);

            TempData["Success"] = "A new verification code has been sent to your email.";
            return RedirectToAction("VerifyOtp", new { email = user.Email });
        }
    }
}