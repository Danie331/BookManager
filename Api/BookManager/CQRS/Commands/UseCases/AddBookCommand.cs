using DomainModels;
using MediatR;

namespace BookManager.CQRS.Commands.UseCases
{
    public record class AddBookCommand : IRequest
    {
        public Book Book { get; private set; }
        public AddBookCommand(Book book)
        {
            Book = book;
        }
    }
}
