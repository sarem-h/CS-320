using System.Collections.Generic;

namespace MyCookBookApi.Models
{
    public class Recipe
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public Dictionary<string, string> Ingredients { get; set; } = new Dictionary<string, string>();
    }
}