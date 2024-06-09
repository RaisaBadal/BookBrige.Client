using BookBridge.Client.Models;
using BookBridge.Server.DecerializerDtos;

namespace BookBridge.Server.ModelInterfaces
{
    public interface IBookModelService
    {
        Task<IEnumerable<BookData>> GetAllBooksAsync();
        Task<bool> DeleteBookByID(long Id);
        Task<bool> InsertBook(BookData bk);
        Task<BookData> GetBookModel();
        Task<IEnumerable<BookCategoryModel>> GetAllBookCategoriessAsync();
        Task<BookData> GetBookDataById(long Id);
        Task<BookDetailModel> GetBookModelDetails(long id);
        Task<bool> UpdateBooks(BookData book);
    }
}
