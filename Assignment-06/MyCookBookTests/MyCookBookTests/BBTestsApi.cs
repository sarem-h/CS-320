using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyCookBookApiBBTests
{
    [TestFixture]
    public class ApiTests
    {
        private RestClient _client = null!;
        private string? _newRecipeId;

        [SetUp]
        public void Setup()
        {
            _client = new RestClient("http://localhost:5085/api/Recipe");
        }

        [TearDown]
        public void Teardown()
        {
            // Dispose of the RestClient or any other resources if necessary
            _client = null!;
        }

        [Test, Order(1)]
        public async Task TestAddRecipe()
        {
            var newRecipe = new
            {
                Name = "Test Recipe",
                Category = "Dessert",
                Instructions = "Mix ingredients and bake.",
                Thumbnail = "test-thumbnail.jpg",
                Ingredients = new Dictionary<string, string> { { "Sugar", "1 cup" }, { "Flour", "2 cups" } }
            };

            var request = new RestRequest("", Method.Post);
            request.AddJsonBody(newRecipe);

            var response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                TestContext.WriteLine($"Response Content: {response.Content}");
            }

            NUnit.Framework.Assert.IsTrue(response.IsSuccessful, $"POST /api/Recipe failed. Response: {response.Content}");

            // Deserialize response content to extract the ID
            var responseData = JsonSerializer.Deserialize<Dictionary<string, object>>(response.Content!);
            NUnit.Framework.Assert.IsNotNull(responseData, "Response data is null.");
            NUnit.Framework.Assert.IsTrue(responseData!.ContainsKey("id"), "Response does not contain 'id'.");

            _newRecipeId = responseData["id"]?.ToString();
            NUnit.Framework.Assert.IsNotNull(_newRecipeId, "Failed to retrieve the ID of the newly added recipe.");
        }

        [Test, Order(2)]
        public async Task TestGetRecipes()
        {
            var request = new RestRequest("", Method.Get);
            var response = await _client.ExecuteAsync<List<dynamic>>(request);

            if (!response.IsSuccessful)
            {
                TestContext.WriteLine($"Response Content: {response.Content}");
            }

            NUnit.Framework.Assert.IsTrue(response.IsSuccessful, "GET /api/Recipe failed.");
            NUnit.Framework.Assert.IsNotNull(response.Data, "No recipes returned.");
        }

        [Test, Order(3)]
        public async Task TestGetRecipeById()
        {
            NUnit.Framework.Assert.IsNotNull(_newRecipeId, "New recipe ID is null. Ensure TestAddRecipe runs first.");

            var request = new RestRequest($"/{_newRecipeId}", Method.Get);
            var response = await _client.ExecuteAsync<dynamic>(request);

            NUnit.Framework.Assert.IsTrue(response.IsSuccessful, "GET /api/Recipe/{id} failed.");
            NUnit.Framework.Assert.IsNotNull(response.Data, "Recipe not found.");
        }

        [Test, Order(4)]
        public async Task TestUpdateRecipe()
        {
            NUnit.Framework.Assert.IsNotNull(_newRecipeId, "New recipe ID is null. Ensure TestAddRecipe runs first.");

            var updatedRecipe = new
            {
                Id = _newRecipeId,
                Name = "Updated Recipe",
                Category = "Main Course",
                Instructions = "Updated instructions.",
                Thumbnail = "updated-thumbnail.jpg",
                Ingredients = new Dictionary<string, string> { { "Salt", "1 tsp" }, { "Pepper", "1 tsp" } }
            };

            var request = new RestRequest($"/{_newRecipeId}", Method.Put);
            request.AddJsonBody(updatedRecipe);

            var response = await _client.ExecuteAsync(request);

            NUnit.Framework.Assert.IsTrue(response.IsSuccessful, "PUT /api/Recipe/{id} failed.");
        }

        [Test, Order(5)]
        public async Task TestDeleteRecipe()
        {
            NUnit.Framework.Assert.IsNotNull(_newRecipeId, "New recipe ID is null. Ensure TestAddRecipe runs first.");

            var request = new RestRequest($"/{_newRecipeId}", Method.Delete);
            var response = await _client.ExecuteAsync(request);

            NUnit.Framework.Assert.IsTrue(response.IsSuccessful, "DELETE /api/Recipe/{id} failed.");
        }
    }
}
