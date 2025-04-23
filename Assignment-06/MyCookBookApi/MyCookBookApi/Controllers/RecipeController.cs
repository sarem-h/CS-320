using Microsoft.AspNetCore.Mvc;
using MyCookBookApi.Models;
using MyCookBookApi.Repositories;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;
using MongoDB.Bson;

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
        public IActionResult GetRecipes([FromQuery] string? search) // Mark search as nullable
        {
            var recipes = _recipeRepository.GetRecipes(search ?? string.Empty); // Handle null case
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

            newRecipe.Id = ObjectId.GenerateNewId().ToString(); // Ensure ID is generated
            await _recipeRepository.AddRecipeAsync(newRecipe);
            _logger.LogInformation("Added new recipe: {0}", JsonSerializer.Serialize(newRecipe));

            return CreatedAtAction(nameof(GetRecipeById), new { id = newRecipe.Id }, newRecipe); // Fix route
        }

        [HttpGet("{id}")]
        public IActionResult GetRecipeById(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest("Invalid ID format.");
            }

            var recipe = _recipeRepository.GetRecipeById(id);
            if (recipe == null)
            {
                return NotFound(); // Ensure NotFound is returned when the recipe does not exist
            }
            return Ok(recipe);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecipe(string id, [FromBody] Recipe recipe)
        {
            if (recipe == null || recipe.Id != id)
            {
                return BadRequest("Invalid recipe data.");
            }

            var existingRecipe = _recipeRepository.GetRecipeById(id);
            if (existingRecipe == null)
            {
                return NotFound();
            }

            await _recipeRepository.UpdateRecipeAsync(recipe);
            _logger.LogInformation("Updated recipe: {0}", JsonSerializer.Serialize(recipe));

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(string id)
        {
            var existingRecipe = _recipeRepository.GetRecipeById(id);
            if (existingRecipe == null)
            {
                return NotFound();
            }

            await _recipeRepository.DeleteRecipeAsync(id);
            _logger.LogInformation("Deleted recipe with ID: {0}", id);

            return NoContent();
        }
    }
}