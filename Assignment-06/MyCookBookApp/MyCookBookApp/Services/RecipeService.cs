using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MyCookBookApp.Models;
using System.Collections.Generic;
using System;
using MyCookBookApp.Services;

namespace MyCookBookApp.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly HttpClient _httpClient;

        public RecipeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Recipe>> GetRecipesAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:5085/api/recipe");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var recipes = JsonConvert.DeserializeObject<List<Recipe>>(json);
            return recipes;
        }

        public async Task<Recipe> AddRecipeAsync(Recipe newRecipe)
        {
            newRecipe.Id = Guid.NewGuid().ToString(); // Generate a new Id
            var json = JsonConvert.SerializeObject(newRecipe);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            Console.WriteLine("Sending request to add recipe: {0}", json);
            var response = await _httpClient.PostAsync("http://localhost:5085/api/recipe", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            //Console.WriteLine("Received response: {0}", responseContent);
            response.EnsureSuccessStatusCode();
            return newRecipe; // Return the updated recipe with the Id
        }

        public async Task<Recipe> GetRecipeByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5085/api/recipe/{id}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var recipe = JsonConvert.DeserializeObject<Recipe>(json);
            return recipe;
        }

        public async Task<Recipe> UpdateRecipeAsync(Recipe recipe)
        {
            var json = JsonConvert.SerializeObject(recipe);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"http://localhost:5085/api/recipe/{recipe.Id}", content);
            response.EnsureSuccessStatusCode();
            return recipe;
        }

        public async Task<bool> DeleteRecipeAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5085/api/recipe/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}