using Microsoft.AspNetCore.Mvc;
using MyCookBookApi.Models;
using MyCookBookApi.Repositories;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyCookBookApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly ILogger<RecipeController> _logger;
        private readonly IRecipeRepository _recipeRepository;

        public RecipeController(ILogger<RecipeController> logger, IRecipeRepository recipeRepository)
        {
            _logger = logger;
            _recipeRepository = recipeRepository;
        }

        [HttpGet]
        public IActionResult GetRecipes([FromQuery] string? search)
        {
            var recipes = _recipeRepository.GetRecipes(search);
            if (recipes == null || !recipes.Any())
            {
                return Ok(new List<Recipe>()); // Return an empty list instead of NotFound
            }
            return Ok(recipes);
        }

        [HttpPost]
        public async Task<IActionResult> AddRecipe([FromBody] Recipe newRecipe)
        {
            if (newRecipe == null || string.IsNullOrEmpty(newRecipe.Name) || string.IsNullOrEmpty(newRecipe.Category))
            {
                return BadRequest("Invalid recipe data.");
            }

            await _recipeRepository.AddRecipeAsync(newRecipe);
            _logger.LogInformation("Added new recipe: {0}", JsonSerializer.Serialize(newRecipe));

            return CreatedAtAction(nameof(GetRecipes), new { id = newRecipe.Id }, newRecipe);
        }
    }
}