using BookManager.CQRS.Commands.UseCases;
using BooksService;
using FluentValidation;
using MediatR;
using System;
using System.Diagnostics;

namespace BookManager.CQRS.Commands.Handlers
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand>
    {
        private readonly IBookService _bookService;
        private readonly IValidator<AddBookCommand> _validator;
        public AddBookCommandHandler(IBookService bookService, IValidator<AddBookCommand> validator)
        {
            _bookService = bookService;
            _validator = validator;
        }

        public async Task Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _validator.ValidateAsync(request);
                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }

                await _bookService.AddAsync(request.Book);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
        }
    }
}
