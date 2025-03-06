using MyCookBookApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System;
using System.Threading.Tasks;

namespace MyCookBookApi.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly string _jsonFilePath;

        public RecipeRepository()
        {
            _jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "recipes.json");

            // Initialize with test data if the file does not exist or is empty
            if (!File.Exists(_jsonFilePath) || new FileInfo(_jsonFilePath).Length == 0)
            {
                var testRecipes = new List<Recipe>
                {
                    new Recipe
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Test Pancakes",
                        Category = "Breakfast",
                        Instructions = "Mix and cook",
                        Thumbnail = "http://example.com/image.jpg",
                        Ingredients = new Dictionary<string, string> { { "Flour", "1 cup" }, { "Milk", "1 cup" }, { "Egg", "1" } }
                    }
                };

                var jsonData = JsonSerializer.Serialize(testRecipes);
                File.WriteAllText(_jsonFilePath, jsonData);
            }
        }

        public List<Recipe> GetRecipes(string search)
        {
            var jsonData = File.ReadAllText(_jsonFilePath);
            var recipes = JsonSerializer.Deserialize<List<Recipe>>(jsonData) ?? new List<Recipe>();

            if (!string.IsNullOrEmpty(search))
            {
                return recipes.Where(r => r.Id == search || r.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return recipes;
        }

        public void AddRecipe(Recipe newRecipe)
        {
            var jsonData = File.ReadAllText(_jsonFilePath);
            var recipes = JsonSerializer.Deserialize<List<Recipe>>(jsonData) ?? new List<Recipe>();

            newRecipe.Id = Guid.NewGuid().ToString();
            recipes.Insert(0, newRecipe);

            var updatedJsonData = JsonSerializer.Serialize(recipes);
            File.WriteAllText(_jsonFilePath, updatedJsonData);
        }

        public async Task AddRecipeAsync(Recipe newRecipe)
        {
            var jsonData = await File.ReadAllTextAsync(_jsonFilePath);
            var recipes = JsonSerializer.Deserialize<List<Recipe>>(jsonData) ?? new List<Recipe>();

            newRecipe.Id = Guid.NewGuid().ToString();
            recipes.Insert(0, newRecipe);

            var updatedJsonData = JsonSerializer.Serialize(recipes);
            await File.WriteAllTextAsync(_jsonFilePath, updatedJsonData);
        }
    }
}
