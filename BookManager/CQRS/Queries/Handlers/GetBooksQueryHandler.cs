using BookManager.CQRS.Queries.UseCases;
using BooksService;
using DomainModels;
using MediatR;
using System.Diagnostics;

namespace BookManager.CQRS.Queries.Handlers
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<Book>>
    {
        private readonly IBookService _bookService;

        public GetBooksQueryHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<List<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _bookService.AllAsync(request.PageIndex, request.PageSize);
                return data.Items;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
        }
    }
}