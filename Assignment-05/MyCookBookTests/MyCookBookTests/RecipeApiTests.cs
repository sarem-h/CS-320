using Xunit;
using MyCookBookApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MyCookBookApi.Models;
using System.Linq;
using Microsoft.Extensions.Logging;
using MyCookBookApi.Repositories;
using Moq;

namespace MyCookBookTests
{
    public class RecipeTests
    {
        private readonly Mock<ILogger<RecipeController>> _logger;
        private readonly RecipeRepository _recipeRepository;
        private readonly RecipeController _controller;

        public RecipeTests()
        {
            _logger = new Mock<ILogger<RecipeController>>();
            _recipeRepository = new RecipeRepository();
            _controller = new RecipeController(_logger.Object, _recipeRepository);
        }

        [Fact]
        public void GetRecipes_ShouldReturnAllRecipes()
        {
            // Act: Call the method on the controller
            IActionResult result = _controller.GetRecipes(null);

            // Assert: Check if the result is of the correct type (OkObjectResult)
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Assert: Check if the value of the OkObjectResult is a list of Recipe objects
            var recipes = Assert.IsType<List<Recipe>>(okResult.Value);

            // Further assertions on the recipes list
            Assert.NotEmpty(recipes);
        }

        [Fact]
        public void GetRecipes_WithSearchQuery_ShouldReturnMatchingRecipes()
        {
            // Arrange: Create a search query
            string searchQuery = "Pancakes";

            // Act: Call the method on the controller with a search query
            IActionResult result = _controller.GetRecipes(searchQuery);

            // Assert: Check if the result is of the correct type (OkObjectResult)
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Assert: Check if the value of the OkObjectResult is a list of Recipe objects
            var recipes = Assert.IsType<List<Recipe>>(okResult.Value);

            // Further assertions on the recipes list
            Assert.NotEmpty(recipes);
            Assert.Contains(recipes, r => r.Name.Contains(searchQuery, System.StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public async Task AddRecipe_ShouldReturnCreatedRecipe()
        {
            // Arrange: Create a new recipe
            var newRecipe = new Recipe
            {
                Name = "Pancakes",
                Category = "Breakfast",
                Instructions = "Mix and cook",
                Thumbnail = "http://example.com/image.jpg",
                Ingredients = new Dictionary<string, string> { { "Flour", "1 cup" } }
            };

            // Act: Call the AddRecipe method on the controller
            var result = await _controller.AddRecipe(newRecipe);

            // Assert: Check if the result is of the correct type (CreatedAtActionResult)
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);

            // Assert: Check if the value of the CreatedAtActionResult is the new Recipe object
            var returnedRecipe = Assert.IsType<Recipe>(createdResult.Value);
            Assert.Equal(newRecipe.Name, returnedRecipe.Name);
            Assert.Equal(newRecipe.Category, returnedRecipe.Category);
            Assert.Equal(newRecipe.Instructions, returnedRecipe.Instructions);
            Assert.Equal(newRecipe.Thumbnail, returnedRecipe.Thumbnail);
            Assert.Equal(newRecipe.Ingredients, returnedRecipe.Ingredients);
        }
    }
}