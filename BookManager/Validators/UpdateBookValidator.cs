using BookManager.CQRS.Commands.UseCases;
using BooksService;
using FluentValidation;

namespace BookManager.Validators
{
    public class UpdateBookValidator : AbstractValidator<UpdateBookCommand>
    {
        private readonly IBookService _bookService;
        public UpdateBookValidator(IBookService bookService)
        {
            _bookService = bookService;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.Book.Id)
              .NotEmpty()
              .WithMessage("Must have a valid ID")
              .WithErrorCode("400");

            RuleFor(command => command.Book.Id)
                .MustAsync(async (x, y, z) => (await _bookService.GetByIdAsync(x.Book.Id.Value)) is not null)
                .WithErrorCode("404")
                .WithMessage("Book not found");

            RuleFor(command => command.Book.Title)
              .NotEmpty()
              .WithMessage("Must have a title")
              .WithErrorCode("400");

            RuleFor(command => command.Book.Author)
              .NotEmpty()
              .WithMessage("Must have an author")
              .WithErrorCode("400");

            RuleFor(command => command.Book.Isbn)
              .NotEmpty()
              .WithMessage("Must have an ISBN")
              .WithErrorCode("400");

            When(command => command.Book.PublishedDate.HasValue, () =>
            {
                RuleFor(command => command.Book.PublishedDate)
                    .LessThanOrEqualTo(DateTime.Today)
                    .WithMessage("Published date can't be in the future")
                    .WithErrorCode("400");
            });
        }
    }
}
