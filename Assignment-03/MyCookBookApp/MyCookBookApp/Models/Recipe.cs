namespace MyCookBookApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Instructions { get; set; }
        public string Thumbnail { get; set; }
        public Dictionary<string, string> Ingredients { get; set; }
    }
}