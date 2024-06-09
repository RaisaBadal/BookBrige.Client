using System.Text.Json.Serialization;

namespace BookBridge.Server.DecerializerDtos
{
    public class BookCategoryModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("name")]
        public  string? Name { get; set; }
        [JsonPropertyName("description")]
        public  string? Description { get; set; }
    }
}
