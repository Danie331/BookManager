
namespace DomainModels
{
    public record class PaginatedList<T>
    {
        public List<T> Items { get; }
        public int PageIndex { get; }
        public int TotalPages { get; }
        public int PageSize { get; }
        public int Total { get; }

        public PaginatedList(List<T> items, int pageIndex, int pageSize, int totalPages, int total)
        {
            Items = items;
            PageIndex = pageIndex;
            PageSize = pageSize;
            Total = total; 
            TotalPages = totalPages;
        }
    }
}
