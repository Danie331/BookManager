using BookManager.CQRS.Commands.UseCases;
using BooksService;
using FluentValidation;
using MediatR;
using System.Diagnostics;

namespace BookManager.CQRS.Commands.Handlers
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
    {
        private readonly IBookService _bookService;
        private readonly IValidator<DeleteBookCommand> _validator;
        public DeleteBookCommandHandler(IBookService bookService, IValidator<DeleteBookCommand> validator)
        {
            _bookService = bookService;
            _validator = validator;
        }

        public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _validator.ValidateAsync(request);
                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }

                await _bookService.DeleteAsync(request.Id);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
        }
    }
}
