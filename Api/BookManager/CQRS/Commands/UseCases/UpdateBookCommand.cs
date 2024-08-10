using DomainModels;
using MediatR;

namespace BookManager.CQRS.Commands.UseCases
{
    public record class UpdateBookCommand : IRequest
    {
        public Book Book { get; private set; }
        public Guid Id { get; private set; }
        public UpdateBookCommand(Guid id, Book book)
        {
            Id = id;
            Book = book;
        }
    }
}