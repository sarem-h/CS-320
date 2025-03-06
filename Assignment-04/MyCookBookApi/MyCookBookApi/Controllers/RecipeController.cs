using Microsoft.AspNetCore.Mvc;
using MyCookBookApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace MyCookBookApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetRecipes([FromQuery] string? search)
        {
            var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "recipes.json");
            var jsonData = System.IO.File.ReadAllText(jsonFilePath);
            var recipes = JsonSerializer.Deserialize<List<Recipe>>(jsonData);

            if (!string.IsNullOrEmpty(search))
            {
                var matchingRecipes = recipes.Where(r => r.Id == search || r.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
                if (matchingRecipes.Any())
                {
                    return Ok(matchingRecipes);
                }
                return NotFound();
            }

            return Ok(recipes);
        }
    }
}