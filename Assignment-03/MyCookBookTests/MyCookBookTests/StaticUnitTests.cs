using Xunit;
using Moq;
using MyCookBookApi.Controllers;
using MyCookBookApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyCookBookTests
{
    public class CombinedTests
    {
        // RecipeController Test (API)
        [Fact]
        public void GetRecipe_ShouldReturnRecipeObject()
        {
            // Arrange: Create a controller instance
            var controller = new RecipeController();

            // Act: Call the method on the controller
            IActionResult result = controller.GetRecipe();

            // Assert: Check if the result is of the correct type (OkObjectResult)
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Assert: Check if the value of the OkObjectResult is the expected Recipe object
            var recipe = Assert.IsType<RecipeController.Recipe>(okResult.Value);

            // Further assertions on the recipe object
            Assert.Equal("1", recipe.Id);
            Assert.Equal("Pancakes", recipe.Name);
        }

        // HomeController Test (MVC Views)
        [Fact]
        public void Index_ReturnsHomeView()
        {
            // Arrange: Create a mock logger and a HomeController instance
            var logger = new Mock<ILogger<HomeController>>();
            var controller = new HomeController(logger.Object);

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
            var controller = new HomeController(logger.Object);

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
            var controller = new HomeController(logger.Object);

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
            var controller = new HomeController(logger.Object);

            int recipeId = 1; // Example recipe ID

            // Act: Call the RecipeDetail action with the recipe ID
            IActionResult result = controller.RecipeDetail(recipeId);

            // Assert: Check if the returned result is a ViewResult
            var viewResult = Assert.IsType<ViewResult>(result);

            // Check if the correct view is being returned
            Assert.Equal("RecipeDetails", viewResult.ViewName);

            // Verify that the RecipeId is passed in ViewData
            Assert.Equal(recipeId, viewResult.ViewData["RecipeId"]);
        }
    }
}