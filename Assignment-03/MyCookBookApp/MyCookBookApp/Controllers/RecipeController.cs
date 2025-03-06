using Microsoft.AspNetCore.Mvc;
using MyCookBookApp.Services;
using System.Threading.Tasks;
using MyCookBookApp.Models;
using System.Collections.Generic;

namespace MyCookBookApp.Controllers
{
    public class RecipeController : Controller
    {
        private readonly RecipeService _recipeService;

        public RecipeController(RecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        public async Task<IActionResult> RecipeList()
        {
            List<Recipe> recipes = await _recipeService.GetRecipesAsync();
            if (recipes == null)
            {
                recipes = new List<Recipe>();
            }
            return PartialView("~/Views/Recipe/ssrRecipeList.cshtml", recipes);
        }
    }
}