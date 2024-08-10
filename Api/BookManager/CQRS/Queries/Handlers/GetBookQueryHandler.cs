using BookManager.CQRS.Queries.UseCases;
using BooksService;
using DomainModels;
using FluentValidation;
using MediatR;
using System.Diagnostics;

namespace BookManager.CQRS.Queries.Handlers
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, Book>
    {
        private readonly IBookService _bookService;
        private readonly IValidator<GetBookQuery> _validator;

        public GetBookQueryHandler(IBookService bookService, IValidator<GetBookQuery> validator)
        {
            _bookService = bookService;
            _validator = validator;
        }

        public async Task<Book> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _validator.ValidateAsync(request);
                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }

                return await _bookService.GetByIdAsync(request.Id);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
        }
    }
}
