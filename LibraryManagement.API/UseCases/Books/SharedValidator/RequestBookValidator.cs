using FluentValidation;
using LibraryManagement.Communication.Requests;

namespace LibraryManagement.API.UseCases.Books.SharedValidator;

public class RequestBookValidator : AbstractValidator<RequestBookJson>
{
    private static readonly HashSet<string> AvailableGenres =
    [
        "Programming",
        "Architecture",
        "Science Fiction",
        "Fantasy",
        "Mystery",
        "Romance",
        "History",
        "Biography",
        "Self-Help",
        "Technology"
    ];

    public RequestBookValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
        RuleFor(x => x.Author).NotEmpty().WithMessage("Author is required.");
        RuleFor(x => x.Genre.ToLower()).NotEmpty().WithMessage("Genre is required.")
            .Must(genre => AvailableGenres.Contains(genre.ToLower().Trim()))
            .WithMessage("Genre is invalid.");
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative.");
        RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative.");
    }
}
