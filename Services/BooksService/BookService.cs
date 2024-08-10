using DomainModels;
using Persistence.Contract;

namespace BooksService
{
    /// <summary>
    /// Domain/business logic will go here.
    /// </summary>
    public class BookService : IBookService
    {
        private readonly IBookDataService _dataService;
        public BookService(IBookDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task AddAsync(Book book)
        {
            book.Id = Guid.NewGuid();
            await _dataService.AddAsync(book);
        }

        public async Task<PaginatedList<Book>> AllAsync(int? pageIndex = null, int? pageSize = null)
        {
            var data = await _dataService.AllAsync();
            if (pageIndex.HasValue && pageSize.HasValue)
            {
                var count = data.Count;
                var pagedData = data.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
                var totalPages = (int)Math.Ceiling(count / (double)pageSize.Value);

                return new PaginatedList<Book>(pagedData, pageIndex.Value, totalPages);
            }

            return new PaginatedList<Book>(data, 1, 1);
        }

        public Task DeleteAsync(Guid id) => _dataService.DeleteAsync(id);

        public async Task<List<Book>> GetByFilterAsync(SearchFilter filter)
        {
            var searchedData = (await _dataService.AllAsync()).AsEnumerable();
            if (!string.IsNullOrWhiteSpace(filter.Author))
            {
                searchedData = searchedData.Where(x => x.Author.Equals(filter.Author, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                searchedData = searchedData.Where(x => x.Title.Contains(filter.Title, StringComparison.OrdinalIgnoreCase));
            }

            return searchedData.ToList();
        }

        public Task<Book?> GetByIdAsync(Guid id) => _dataService.GetByIdAsync(id);

        public Task UpdateAsync(Guid id, Book book) => _dataService.UpdateAsync(id, book);
    }
}