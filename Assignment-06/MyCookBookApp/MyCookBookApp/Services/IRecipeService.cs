using MyCookBookApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCookBookApp.Services
{
    public interface IRecipeService
    {
        Task<List<Recipe>> GetRecipesAsync();
        Task<Recipe> AddRecipeAsync(Recipe newRecipe);
        Task<Recipe> GetRecipeByIdAsync(string id);
        Task<Recipe> UpdateRecipeAsync(Recipe recipe);
        Task<bool> DeleteRecipeAsync(string id);
    }
}
