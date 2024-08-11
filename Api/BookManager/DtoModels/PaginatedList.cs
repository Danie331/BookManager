
namespace BookManager.DtoModels
{
    public record class PaginatedList<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int Total { get; set; }
        public List<T> Items { get; set; }
    }
}