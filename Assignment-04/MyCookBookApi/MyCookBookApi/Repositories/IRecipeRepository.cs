using MyCookBookApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCookBookApi.Repositories
{
    public interface IRecipeRepository
    {
        List<Recipe> GetRecipes(string search);
        void AddRecipe(Recipe newRecipe);
        Task AddRecipeAsync(Recipe newRecipe);
    }
}
