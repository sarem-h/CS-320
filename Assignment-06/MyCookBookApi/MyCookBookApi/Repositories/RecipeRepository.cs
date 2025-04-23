using MyCookBookApi.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCookBookApi.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly IMongoCollection<Recipe> _recipesCollection;

        public RecipeRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("CookBookDb");
            _recipesCollection = database.GetCollection<Recipe>("Recipes");
        }

        public List<Recipe> GetRecipes(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return _recipesCollection.Find(_ => true).ToList();
            }

            var filter = ObjectId.TryParse(search, out _)
                ? Builders<Recipe>.Filter.Eq(r => r.Id, search)
                : Builders<Recipe>.Filter.Regex(r => r.Name, new BsonRegularExpression(search, "i"));

            return _recipesCollection.Find(filter).ToList();
        }

        public void AddRecipe(Recipe newRecipe)
        {
            _recipesCollection.InsertOne(newRecipe);
        }

        public async Task AddRecipeAsync(Recipe newRecipe)
        {
            if (string.IsNullOrEmpty(newRecipe.Id))
            {
                newRecipe.Id = ObjectId.GenerateNewId().ToString(); // Ensure ID is set
            }
            await _recipesCollection.InsertOneAsync(newRecipe);
        }

        public Recipe GetRecipeById(string id)
        {
            if (!ObjectId.TryParse(id, out _))
            {
                return null!;
            }
            return _recipesCollection.Find(r => r.Id == id).FirstOrDefault()!;
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            await _recipesCollection.ReplaceOneAsync(r => r.Id == recipe.Id, recipe);
        }

        public async Task DeleteRecipeAsync(string id)
        {
            if (ObjectId.TryParse(id, out _))
            {
                await _recipesCollection.DeleteOneAsync(r => r.Id == id);
            }
        }
    }
}
