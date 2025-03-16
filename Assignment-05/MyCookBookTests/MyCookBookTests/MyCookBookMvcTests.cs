using Moq;
using Xunit;
using MyCookBookApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCookBookApp.Services;
using MyCookBookApp.Models;

namespace MyCookBookTests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_ReturnsHomeView()
        {
            // Arrange: Create a mock logger and a HomeController instance
            var logger = new Mock<ILogger<HomeController>>();
            var recipeService = new Mock<IRecipeService>();
            var controller = new HomeController(logger.Object, recipeService.Object);

            // Act: Call the Index action
            IActionResult result = controller.Index();

            // Assert: Check if the returned result is a ViewResult
            var viewResult = Assert.IsType<ViewResult>(result);

            // Check if the view name is "Home"
            Assert.Equal("Home", viewResult.ViewData["Title"]);
        }

        [Fact]
        public void About_ReturnsAboutView()
        {
            // Arrange: Create a mock logger and a HomeController instance
            var logger = new Mock<ILogger<HomeController>>();
            var recipeService = new Mock<IRecipeService>();
            var controller = new HomeController(logger.Object, recipeService.Object);

            // Act: Call the About action
            IActionResult result = controller.About();

            // Assert: Check if the returned result is a ViewResult
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Recipes_ReturnsRecipesView()
        {
            // Arrange: Create a mock logger and a HomeController instance
            var logger = new Mock<ILogger<HomeController>>();
            var recipeService = new Mock<IRecipeService>();
            var controller = new HomeController(logger.Object, recipeService.Object);

            // Act: Call the Recipes action
            IActionResult result = controller.Recipes();

            // Assert: Check if the returned result is a ViewResult
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void RecipeDetail_ReturnsRecipeDetailsView_WithCorrectId()
        {
            // Arrange: Create a mock logger and a HomeController instance
            var logger = new Mock<ILogger<HomeController>>();
            var recipeService = new Mock<IRecipeService>();
            var controller = new HomeController(logger.Object, recipeService.Object);

            string recipeId = "1"; // Example recipe ID

            // Act: Call the RecipeDetail action with the recipe ID
            IActionResult result = controller.RecipeDetail(recipeId);

            // Assert: Check if the returned result is a ViewResult
            var viewResult = Assert.IsType<ViewResult>(result);

            // Check if the correct view is being returned
            Assert.Equal("RecipeDetails", viewResult.ViewName);

            // Verify that the RecipeId is passed in ViewData
            Assert.Equal(recipeId, viewResult.ViewData["RecipeId"]);
        }

        [Fact]
        public async Task AddRecipe_Post_ReturnsRedirectToRecipes()
        {
            // Arrange: Create a mock logger, mock recipe service, and a HomeController instance
            var logger = new Mock<ILogger<HomeController>>();
            var recipeService = new Mock<IRecipeService>();
            var controller = new HomeController(logger.Object, recipeService.Object);

            // Mock the AddRecipeAsync method to return a new Recipe
            recipeService.Setup(service => service.AddRecipeAsync(It.IsAny<Recipe>()))
                         .ReturnsAsync(new Recipe { Id = "1" });

            // Act: Call the AddRecipe action with sample data
            var result = await controller.AddRecipe("Pancakes", "Breakfast", "Mix and cook", "http://example.com/image.jpg", 
                                                    new List<string> { "Flour" }, new List<string> { "1 cup" });

            // Assert: Check if the returned result is a RedirectToActionResult
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Recipes", redirectResult.ActionName);
        }
    }
}