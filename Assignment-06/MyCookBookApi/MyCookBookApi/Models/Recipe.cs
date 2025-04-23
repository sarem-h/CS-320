using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MyCookBookApi.Models
{
    public class Recipe
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty; // Changed from ObjectId to string
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public Dictionary<string, string> Ingredients { get; set; } = new Dictionary<string, string>();
    }
}