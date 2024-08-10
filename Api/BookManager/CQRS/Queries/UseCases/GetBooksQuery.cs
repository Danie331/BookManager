using DomainModels;
using MediatR;

namespace BookManager.CQRS.Queries.UseCases
{
    public record class GetBooksQuery : IRequest<List<Book>>
    {
        public int? PageIndex { get; private set; }
        public int? PageSize { get; private set; }

        public GetBooksQuery(int? pageIndex, int? pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}