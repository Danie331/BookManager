using BookManager.CQRS.Commands.UseCases;
using BooksService;
using FluentValidation;

namespace BookManager.Validators
{
    public class DeleteBookValidator : AbstractValidator<DeleteBookCommand>
    {
        private readonly IBookService _bookService;
        public DeleteBookValidator(IBookService bookService)
        {
            _bookService = bookService;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.Id)
              .NotEmpty()
              .WithMessage("Must have a valid ID")
              .WithErrorCode("400");

            RuleFor(command => command.Id)
                .MustAsync(async (x, y, z) => (await _bookService.GetByIdAsync(x.Id)) is not null)
                .WithErrorCode("404")
                .WithMessage("Book not found");
        }
    }
}