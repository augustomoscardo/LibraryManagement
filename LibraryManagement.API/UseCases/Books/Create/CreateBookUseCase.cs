using Library_Management.API.Infrastructure;
using Library_Mangement.Entities;
using LibraryManagement.API.UseCases.Books.SharedValidator;
using LibraryManagement.Communication.Requests;
using LibraryManagement.Communication.Responses;
using LibraryManagement.Exceptions.ExceptionsBase;

namespace LibraryManagement.API.UseCases.Books.Create;

public class CreateBookUseCase
{
    public ResponseShortBookJson Execute(RequestBookJson request)
    {
        Validate(request);

        //var dbContext = new LibraryManagementDbContext();

        var entity = new Book
        {
            Title = request.Title,
            Author = request.Author,
            Genre = request.Genre,
            Price = request.Price,
            Stock = request.Stock,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        //dbContext.Books.Add(entity);

        //dbContext.SaveChanges();

        return new ResponseShortBookJson
        {
            Id = entity.Id,
            Title = entity.Title
        };
    }

    public void Validate(RequestBookJson request)
    {
        var validator = new RequestBookValidator();

        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errors = result.Errors.Select(failure => failure.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errors);
        }
    }
}
