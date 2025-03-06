using Xunit;
using MyCookBookApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MyCookBookApi.Models;
using System.Linq;

namespace MyCookBookTests
{
    public class RecipeTests
    {
        [Fact]
        public void GetRecipes_ShouldReturnAllRecipes()
        {
            // Arrange: Create a controller instance
            var controller = new RecipeController();

            // Act: Call the method on the controller
            IActionResult result = controller.GetRecipes(null);

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
            // Arrange: Create a controller instance
            var controller = new RecipeController();
            string searchQuery = "Pancakes";

            // Act: Call the method on the controller with a search query
            IActionResult result = controller.GetRecipes(searchQuery);

            // Assert: Check if the result is of the correct type (OkObjectResult)
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Assert: Check if the value of the OkObjectResult is a list of Recipe objects
            var recipes = Assert.IsType<List<Recipe>>(okResult.Value);

            // Further assertions on the recipes list
            Assert.NotEmpty(recipes);
            Assert.Contains(recipes, r => r.Name.Contains(searchQuery, System.StringComparison.OrdinalIgnoreCase));
        }
    }
}