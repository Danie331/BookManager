using MediatR;

namespace BookManager.CQRS.Commands.UseCases
{
    public class DeleteBookCommand : IRequest
    {
        public Guid Id { get; private set; }
        public DeleteBookCommand(Guid id)
        {
            Id = id;
        }
    }
}