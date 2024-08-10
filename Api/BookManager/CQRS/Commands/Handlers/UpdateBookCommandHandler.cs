using BookManager.CQRS.Commands.UseCases;
using BooksService;
using FluentValidation;
using MediatR;
using System.Diagnostics;

namespace BookManager.CQRS.Commands.Handlers
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
    {
        private readonly IBookService _bookService;
        private readonly IValidator<UpdateBookCommand> _validator;
        public UpdateBookCommandHandler(IBookService bookService, IValidator<UpdateBookCommand> validator)
        {
            _bookService = bookService;
            _validator = validator;
        }

        public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _validator.ValidateAsync(request);
                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }

                await _bookService.UpdateAsync(request.Id, request.Book);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
        }
    }
}