using Xunit;
using MyCookBookApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MyCookBookTests
{
    public class RecipeTests
    {
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
    }
}