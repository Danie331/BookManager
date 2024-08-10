using DomainModels;
using MediatR;

namespace BookManager.CQRS.Queries.UseCases
{
    public record class GetBooksFilterQuery : IRequest<List<Book>>
    {
        public SearchFilter Filter { get; }
        public GetBooksFilterQuery(SearchFilter filter)
        {
            Filter = filter;
        }
    }
}