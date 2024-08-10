using BookManager.CQRS.Queries.UseCases;
using BooksService;
using DomainModels;
using MediatR;
using System.Diagnostics;

namespace BookManager.CQRS.Queries.Handlers
{
    public class GetBooksFilterQueryHandler : IRequestHandler<GetBooksFilterQuery, List<Book>>
    {
        private readonly IBookService _bookService;

        public GetBooksFilterQueryHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<List<Book>> Handle(GetBooksFilterQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _bookService.GetByFilterAsync(request.Filter);
                return data;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
        }
    }
}
