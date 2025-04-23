using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MyCookBookApi.Controllers;
using MyCookBookApi.Models;
using MyCookBookApi.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyCookBookApi.Tests
{
    public class RecipeControllerTests
    {
        private readonly Mock<IRecipeRepository> _mockRepo;
        private readonly Mock<ILogger<RecipeController>> _mockLogger;
        private readonly RecipeController _controller;

        public RecipeControllerTests()
        {
            _mockRepo = new Mock<IRecipeRepository>();
            _mockLogger = new Mock<ILogger<RecipeController>>();
            _controller = new RecipeController(_mockLogger.Object, _mockRepo.Object);
        }

        [Fact]
        public void GetRecipes_ReturnsEmptyList_WhenNoRecipesFound()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetRecipes(It.IsAny<string>())).Returns(new List<Recipe>());

            // Act
            var result = _controller.GetRecipes(null) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Recipe>>(result.Value);
            Assert.Empty((List<Recipe>)result.Value);
        }

        [Fact]
        public void GetRecipes_ReturnsRecipes_WhenRecipesExist()
        {
            // Arrange
            var recipes = new List<Recipe> { new Recipe { Id = "1", Name = "Test Recipe", Category = "Dessert" } };
            _mockRepo.Setup(repo => repo.GetRecipes(It.IsAny<string>())).Returns(recipes);

            // Act
            var result = _controller.GetRecipes(null) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Recipe>>(result.Value);
            Assert.Single((List<Recipe>)result.Value);
        }

        [Fact]
        public async Task AddRecipe_ReturnsBadRequest_WhenRecipeIsInvalid()
        {
            // Act
            var result = await _controller.AddRecipe(null!); // Explicitly mark null as nullable

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Invalid recipe data.", (result as BadRequestObjectResult)?.Value);
        }

        [Fact]
        public async Task AddRecipe_ReturnsCreatedAtAction_WhenRecipeIsValid()
        {
            // Arrange
            var newRecipe = new Recipe { Name = "Test Recipe", Category = "Dessert" };
            _mockRepo.Setup(repo => repo.AddRecipeAsync(It.IsAny<Recipe>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddRecipe(newRecipe) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(nameof(RecipeController.GetRecipeById), result.ActionName);
        }

        [Fact]
        public void GetRecipeById_ReturnsBadRequest_WhenIdIsInvalid()
        {
            // Act
            var result = _controller.GetRecipeById("invalid-id") as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Invalid ID format.", result.Value);
        }

        [Fact]
        public void GetRecipeById_ReturnsNotFound_WhenRecipeDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetRecipeById(It.IsAny<string>())).Returns((Recipe?)null);

            // Act
            var result = _controller.GetRecipeById("67ec649889d04c2207ee9ece");

            // Assert
            Assert.NotNull(result); // Ensure result is not null
            Assert.IsType<NotFoundResult>(result); // Check for NotFoundResult
        }

        [Fact]
        public async Task UpdateRecipe_ReturnsBadRequest_WhenRecipeIsInvalid()
        {
            // Act
            var result = await _controller.UpdateRecipe("id", null!); // Explicitly mark null as nullable

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Invalid recipe data.", (result as BadRequestObjectResult)?.Value);
        }

        [Fact]
        public async Task UpdateRecipe_ReturnsNotFound_WhenRecipeDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetRecipeById(It.IsAny<string>())).Returns((Recipe?)null);

            // Act
            var result = await _controller.UpdateRecipe("id", new Recipe { Id = "id" }) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteRecipe_ReturnsNotFound_WhenRecipeDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetRecipeById(It.IsAny<string>())).Returns((Recipe?)null);

            // Act
            var result = await _controller.DeleteRecipe("id") as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteRecipe_ReturnsNoContent_WhenRecipeIsDeleted()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetRecipeById(It.IsAny<string>())).Returns(new Recipe { Id = "id" });
            _mockRepo.Setup(repo => repo.DeleteRecipeAsync(It.IsAny<string>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteRecipe("id") as NoContentResult;

            // Assert
            Assert.NotNull(result);
        }
    }
}
