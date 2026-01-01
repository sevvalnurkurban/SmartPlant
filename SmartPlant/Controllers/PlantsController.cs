using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPlant.Helpers;
using SmartPlant.Models.ViewModels;
using SmartPlant.Services.Interfaces;

namespace SmartPlant.Controllers
{
    [Authorize]
    public class PlantsController : Controller
    {
        private readonly IPlantService _plantService;

        public PlantsController(IPlantService plantService)
        {
            _plantService = plantService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string? search, string? sortBy)
        {
            var plants = string.IsNullOrEmpty(search)
                ? await _plantService.GetAllPlantsAsync()
                : await _plantService.SearchPlantsAsync(search);

            // Sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                plants = sortBy.ToLower() switch
                {
                    "name" => plants.OrderBy(p => p.Name),
                    "type" => plants.OrderBy(p => p.Type),
                    _ => plants.OrderBy(p => p.Name)
                };
            }

            ViewBag.Search = search;
            ViewBag.SortBy = sortBy;
            return View(plants);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var plant = await _plantService.GetByIdAsync(id);
            if (plant == null)
                return NotFound();

            return View(plant);
        }
    }
}