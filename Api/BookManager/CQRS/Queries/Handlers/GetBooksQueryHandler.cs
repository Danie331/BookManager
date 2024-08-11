using BookManager.CQRS.Queries.UseCases;
using BooksService;
using DomainModels;
using MediatR;
using System.Diagnostics;

namespace BookManager.CQRS.Queries.Handlers
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, PaginatedList<Book>>
    {
        private readonly IBookService _bookService;

        public GetBooksQueryHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<PaginatedList<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _bookService.AllAsync(request.PageIndex, request.PageSize);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
        }
    }
}