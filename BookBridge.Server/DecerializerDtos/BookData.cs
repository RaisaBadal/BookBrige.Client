using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookBridge.Server.DecerializerDtos
{
    public class BookData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("publishedDate")]
        public DateTime PublishedDate { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("coverImageUrl")]
        public string CoverImageUrl { get; set; }

        [JsonPropertyName("authorId")]
        public int AuthorId { get; set; }

        [JsonPropertyName("bookCategoryId")]
        public int BookCategoryId { get; set; }

        [JsonPropertyName("totalCopies")]
        public int TotalCopies { get; set; }

        [JsonPropertyName("availableCopies")]
        public int AvailableCopies { get; set; }
        public List<SelectListItem>? Authors { get; set; }
        public List<SelectListItem>? Categories { get; set; }
    }
}
