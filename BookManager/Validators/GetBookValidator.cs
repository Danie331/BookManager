using BookManager.CQRS.Queries.UseCases;
using BooksService;
using FluentValidation;

namespace BookManager.Validators
{
    public class GetBookValidator : AbstractValidator<GetBookQuery>
    {
        private readonly IBookService _bookService;
        public GetBookValidator(IBookService bookService)
        {
            _bookService = bookService;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(query => query.Id)
              .NotEmpty()
              .WithMessage("Must have a valid ID")
              .WithErrorCode("400");

            RuleFor(query => query.Id)
                .MustAsync(async (x, y, z) => (await _bookService.GetByIdAsync(x.Id)) is not null)
                .WithErrorCode("404")
                .WithMessage("Book not found");
        }
    }
}
