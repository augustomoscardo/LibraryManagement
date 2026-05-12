using Library_Management.API.Infrastructure;
using LibraryManagement.API.UseCases.Books.SharedValidator;
using LibraryManagement.Communication.Requests;
using LibraryManagement.Communication.Responses;
using LibraryManagement.Exceptions.ExceptionsBase;

namespace LibraryManagement.API.UseCases.Books.Update;

public class UpdateBookUseCase
{
    public void Execute(Guid id, RequestBookJson request)
    {
        Validate(request);

        //var dbContext = new LibraryManagementDbContext();

        //var entity = dbContext.Books.FirstOrDefault(book => book.Id == bookId);

        //if (entity == null)
        //{
        //    throw new NotFoundException("Cliente não encontrado.");
        //}

        //entity.Name = request.Name;
        //entity.Email = request.Email;

        //dbContext.Clients.Update(entity);

        //dbContext.SaveChanges();

        Console.WriteLine("Atualizado!");
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
