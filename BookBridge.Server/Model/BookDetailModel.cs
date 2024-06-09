using BookBridge.Server.DecerializerDtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BookBridge.Client.Models
{
    public class BookDetailModel
    {
        public  int  Id { get; set; }
        public string title { get; set; }
        public DateTime publishedDate { get; set; } = DateTime.Now;
        public string description { get; set; }
        public string coverImageUrl { get; set; }
        public int availableCopies { get; set; }
        public List<AuthorData> authorDatas { get; set; }
        public List<BookCategoryModel> bookCategories { get; set; }

    }
}
