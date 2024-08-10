
using DomainModels;

namespace BooksService
{
    public interface IBookService
    {
        Task<PaginatedList<Book>> AllAsync(int? pageIndex = null, int? pageSize = null);
        Task<Book?> GetByIdAsync(Guid id);
        Task AddAsync(Book book);
        Task UpdateAsync(Guid id, Book book);
        Task DeleteAsync(Guid id);
        Task<List<Book>> GetByFilterAsync(SearchFilter filter);
    }
}
