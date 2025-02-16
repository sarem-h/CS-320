using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MyCookBookApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class RecipeController : ControllerBase
    {
        // Define the Recipe model
        public class Recipe
        {
            public string Id { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string Category { get; set; } = string.Empty;
            public string Instructions { get; set; } = string.Empty;
            public string Thumbnail { get; set; } = string.Empty;
            public string YoutubeLink { get; set; } = string.Empty;
            public Dictionary<string, string> Ingredients { get; set; } = new Dictionary<string, string>();
        }

        [HttpGet]
        public IActionResult GetRecipe([FromQuery] string search)
        {
            var recipes = new List<Recipe>
            {
                new Recipe
                {
                    Id = "00001",
                    Name = "Pancakes",
                    Category = "Breakfast",
                    Instructions = "Mix all ingredients in a bowl. Heat a pan and pour the batter. Cook until golden brown on both sides.",
                    Thumbnail = "https://www.allrecipes.com/thmb/FE0PiuuR0Uh06uVh1c2AsKjRGbc=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/21014-Good-old-Fashioned-Pancakes-mfs_002-0e249c95678f446291ebc9408ae64c05.jpg",
                    YoutubeLink = "https://www.youtube.com/watch?v=9zCVCL4V8JQ",
                    Ingredients = new Dictionary<string, string>
                    {
                        { "Flour", "1 cup" },
                        { "Milk", "1 cup" },
                        { "Egg", "1" },
                        { "Butter", "2 tbsp" },
                        { "Sugar", "2 tbsp" }
                    }
                },
            };

            var filteredRecipes = recipes.Where(r => r.Name.Contains(search, StringComparison.OrdinalIgnoreCase) || r.Id.Equals(search, StringComparison.OrdinalIgnoreCase)).ToList();

            return Ok(filteredRecipes);
        }
    }
}