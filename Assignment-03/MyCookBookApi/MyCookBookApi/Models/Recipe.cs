using System.Collections.Generic;

namespace MyCookBookApi.Models
{
    public class Recipe
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Instructions { get; set; }
        public string Thumbnail { get; set; }
        public Dictionary<string, string> Ingredients { get; set; }
    }
}