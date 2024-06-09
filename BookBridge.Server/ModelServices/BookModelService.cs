using BookBridge.Server.DecerializerDtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBridge.Server.ModelInterfaces;
using static System.Net.WebRequestMethods;
using System.Net;
using BookBridge.Client.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookBridge.Server.ModelServices
{
    public class BookModelService : IBookModelService
    {
        private readonly string BaseURL;
        private readonly IAuthorModelService authorModelService;
        public BookModelService(IAuthorModelService authorModelService)
        {
            BaseURL = "https://localhost:7278/api/Book/";
            this.authorModelService = authorModelService;
        }

        public async Task<IEnumerable<BookData>> GetAllBooksAsync()
        {
            using (var client = new HttpClient())
            {
                var res = await client.GetAsync(BaseURL + "AllBook");
                if (res.IsSuccessStatusCode)
                {
                    var content = await res.Content.ReadAsStringAsync();
                    IEnumerable<BookData> response = JsonConvert.DeserializeObject<IEnumerable<BookData>>(content);
                    return response;
                }
            }
            return null;
        }

        public async Task<IEnumerable<BookCategoryModel>> GetAllBookCategoriessAsync()
        {
            using (var client = new HttpClient())
            {
                var res = await client.GetAsync("https://localhost:7278/api/Book/AllBookCategory");
                if (res.IsSuccessStatusCode)
                {
                    var content = await res.Content.ReadAsStringAsync();
                    IEnumerable<BookCategoryModel> response = JsonConvert.DeserializeObject<IEnumerable<BookCategoryModel>>(content);
                    return response;
                }
                return null;
            }
        }

        public async Task<bool> DeleteBookByID(long Id)
        {
            using (var client = new HttpClient())
            { 
               var res=await  client.GetAsync($"https://localhost:7278/api/Book/RemoveBook/{Id}");
                var content = res.Content.ReadAsStringAsync();
               if (content.IsCompleted)
               {
                   return true;
               }
               return false;
            }
        }

        public async Task<bool> InsertBook(BookData bk)
        {
            var link = "https://localhost:7278/api/Book/InsertBook";
            using (var client = new HttpClient())
            {
                string jsonbody=Newtonsoft.Json.JsonConvert.SerializeObject(bk);
                var content=new StringContent(jsonbody, Encoding.UTF8, "application/json");
               var res= await client.PostAsync(link, content);
                var response = await res.Content.ReadAsStringAsync();

                return response.Count()>0;
            }
        }

        public async Task<BookData> GetBookModel()
        {
           var bookdate= new BookData();
            bookdate.Authors = new List<SelectListItem>();
            var authors = await authorModelService.GetAuthors();
            foreach (var item in authors)
            {
                var SelectIte = new SelectListItem(item.Name + ' ' + item.Surname, item.Id.ToString());
                bookdate.Authors.Add(SelectIte);
            }
            bookdate.Categories = new List<SelectListItem>();
            var categorie = await GetAllBookCategoriessAsync();

            foreach (var item in categorie)
            {
                var SelectIte = new SelectListItem(item.Name, item.Id.ToString());
                bookdate.Categories.Add(SelectIte);
            }
            return  bookdate;
        }

        public async Task<BookDetailModel> GetBookModelDetails(long id)
        {
            BookDetailModel mod = new BookDetailModel();
            var bookdata = await GetBookDataById(id);
                
            mod.title=bookdata.Title;
            mod.description=bookdata.Description;
            mod.publishedDate=bookdata.PublishedDate;
            mod.availableCopies=bookdata.AvailableCopies;
            mod.authorDatas = new List<AuthorData>();

            var auth = await authorModelService.GetAuthors();
            if(auth != null)
            {
                var filtrAuth = auth.Where(i => i.Id == bookdata.AuthorId).ToList();
                foreach (var item in filtrAuth)
                {
                    AuthorData authorda = new AuthorData();
                    authorda.BirthDate = item.BirthDate;
                    authorda.Surname = item.Surname;
                    authorda.Name = item.Name;
                    mod.authorDatas.Add(authorda);
                }
            }
            mod.bookCategories = new List<BookCategoryModel>();
            var bookcat = await GetAllBookCategoriessAsync();
            if (bookcat != null)
            {
                var filtrCat = bookcat.Where(i => i.Id == bookdata.BookCategoryId).ToList();
                foreach (var item in filtrCat)
                {
                    BookCategoryModel bookCategory = new BookCategoryModel();
                    bookCategory.Name = item.Name;
                    bookCategory.Description = item.Description;
                    mod.bookCategories.Add(bookCategory);
                }
            }
            return mod;
        }

        public async Task<BookData> GetBookDataById(long Id)
        {
            string baseurl = $"https://localhost:7278/api/Book/GetByIdBook/{Id}";
            using (var client=new HttpClient())
            {
                var res = await client.GetAsync(baseurl);
                var content =await res.Content.ReadAsStringAsync();
                if (content is not null)
                {
                   var rekl= Newtonsoft.Json.JsonConvert.DeserializeObject<BookData>(content);
                    await Console.Out.WriteLineAsync( rekl.PublishedDate.ToString());
                    return rekl;
                }
            }
            return null;
        }

        public async Task<bool> UpdateBooks(BookData book)
        {
            string baseLink = $"https://localhost:7278/api/Book/UpdateBook/{book.Id}";
            using (var client = new HttpClient())
            {
                string jsonbody = Newtonsoft.Json.JsonConvert.SerializeObject(book);
                var content = new StringContent(jsonbody, Encoding.UTF8, "application/json");
                var res = await client.PutAsync(baseLink, content);
                if (res.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
