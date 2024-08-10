using DomainModels;
using MediatR;

namespace BookManager.CQRS.Queries.UseCases
{
    public record class GetBookQuery : IRequest<Book>
    {
        public Guid Id { get; private set; }
        public GetBookQuery(Guid id)
        {
            Id = id;
        }
    }
}
