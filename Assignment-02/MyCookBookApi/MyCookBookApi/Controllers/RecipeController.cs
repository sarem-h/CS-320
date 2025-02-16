using Microsoft.AspNetCore.Mvc;

namespace MyCookBookApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        // Define the Recipe model
        public class Recipe
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public string[] Steps { get; set; }
            public string[] Ingredients { get; set; }
        }

        [HttpGet]
        public IActionResult GetRecipe()
        {
            var recipe = new Recipe
            {
                Id = "1",
                Name = "Pancakes",
                Category = "Breakfast",
                Steps = new string[]
                {
                    "Mix all ingredients in a bowl.",
                    "Heat a pan and pour the batter.",
                    "Cook until golden brown on both sides."
                },
                Ingredients = new string[] { "Flour", "Milk", "Egg", "Butter", "Sugar" }
            };

            return Ok(recipe);
        }
    }
}