using LibraryManagement.Communication.Responses;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.API.UseCases.Books.GetAll;

public class GetAllBooksUseCase
{
    public ResponseAllBooksJson Execute()
    {
        

        var books = new List<ResponseBookJson> {
            new() {
                Id = Guid.NewGuid(),
                Title = "The Great Gatsby",
                Author = "F. Scott Fitzgerald",
                Genre = "Classic",
                Price = 10.99m,
                Stock = 5,
                
            },
            new() {
                Id = Guid.NewGuid(),
                Title = "To Kill a Mockingbird",
                Author = "Harper Lee",
                Genre = "Classic",
                Price = 8.99m,
                Stock = 3
            }
        };

        return new ResponseAllBooksJson
        {
            Books = books
        };

        //var dbContext = new LibraryManagementDbContext();

        //var books = dbContext.Books.ToList();

        //return new ResponseAllBooksJson
        //{
        //    Books = books.Select(Books => new ResponseBookJson
        //    {
        //        Id = Books.Id,
        //        Title = Books.Title,
        //        Author = Books.Author,
        //        Genre = Books.Genre,
        //        Price = Books.Price,
        //        Stock = Books.Stock
        //    }).ToList()
        //};
    }
}
