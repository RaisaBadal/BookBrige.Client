using BookBridge.Server.DecerializerDtos;
using BookBridge.Server.ModelInterfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBridge.Server.ModelServices
{
    public class AuthorModelService:IAuthorModelService
    {

        public async Task<IEnumerable<AuthorData>> GetAuthors()
        {
            string baseUrl = "https://localhost:7278/api/Book/AllAuthor";
            using (var client = new HttpClient())
            {
                var res = await client.GetAsync(baseUrl);
                if (res.IsSuccessStatusCode)
                {
                    var content = await res.Content.ReadAsStringAsync();
                    IEnumerable<AuthorData> response = JsonConvert.DeserializeObject<IEnumerable<AuthorData>>(content);
                    return response;
                }
            }
            return null;
        }
    }
}
