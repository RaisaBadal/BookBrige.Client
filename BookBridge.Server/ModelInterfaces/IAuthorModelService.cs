using BookBridge.Server.DecerializerDtos;

namespace BookBridge.Server.ModelInterfaces
{
    public interface IAuthorModelService
    {
        Task<IEnumerable<AuthorData>> GetAuthors();
    }
}
