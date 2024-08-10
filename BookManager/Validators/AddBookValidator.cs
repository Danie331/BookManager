using BookManager.CQRS.Commands.UseCases;
using FluentValidation;

namespace BookManager.Validators
{
    public class AddBookValidator : AbstractValidator<AddBookCommand>
    {
        public AddBookValidator()
        {
            RuleFor(command => command.Book.Id)
              .Null()
              .WithMessage("Empty ID required when adding")
              .WithErrorCode("400");

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
