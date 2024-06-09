using System.Text.Json.Serialization;

namespace BookBridge.Server.DecerializerDtos
{
    public class AuthorData
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }


        [JsonPropertyName("surname")]
        public  string? Surname { get; set; }

        [JsonPropertyName("birthDate")]
        public DateTime BirthDate { get; set; }
    }
}
