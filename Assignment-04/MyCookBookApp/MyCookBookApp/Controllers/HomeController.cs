using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCookBookApp.Models;

namespace MyCookBookApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Home";
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Recipes()
        {
            return View();
        }

        // New action method to handle dynamic recipe IDs
        [Route("recipes/{id:int}")]
        public IActionResult RecipeDetail(int id)
        {
            // Fetch the recipe details based on the ID
            // For demonstration, we'll just pass the ID to the view
            ViewData["RecipeId"] = id;
            return View("RecipeDetails");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}