
using DomainModels;

namespace Persistence.Contract
{
    public interface IBookDataService
    {
        Task<List<Book>> AllAsync();
        Task<Book?> GetByIdAsync(Guid id);
        Task AddAsync(Book book);
        Task UpdateAsync(Guid id, Book book);
        Task DeleteAsync(Guid id);
    }
}