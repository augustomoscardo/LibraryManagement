using Library_Mangement.Entities;
using LibraryManagement.API.UseCases.Books.Create;
using LibraryManagement.API.UseCases.Books.GetAll;
using LibraryManagement.Communication.Requests;
using LibraryManagement.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Library_Mangement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    // static so data persists across requests while the app runs
    private static readonly List<Book> books = new()
    {
        new Book { Title = "Clean Code", Author = "Robert C. Martin", Genre = "Programming", Price = 45.0m, Stock = 3 },
        new Book { Title = "Domain-Driven Design", Author = "Eric Evans", Genre = "Architecture", Price = 60.0m, Stock = 2 }
    };

    [HttpGet]
    [ProducesResponseType(typeof(ResponseAllBooksJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status400BadRequest)]
    public IActionResult GetBooks()
    {
        var useCase = new GetAllBooksUseCase();

        var response = useCase.Execute();
        Console.WriteLine(response);

        if (response.Books.Count == 0)
        {
            return NoContent();
        }

        return Ok(response.Books);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetBook([FromRoute] Guid id)
    {
       
        return Ok("Livro encontrado");
    }

    //[HttpPost]
    //[ProducesResponseType(typeof(ResponseShortBookJson), StatusCodes.Status201Created)]
    //[ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status400BadRequest)]
    //public IActionResult CreateBook([FromBody] RequestBookJson request)
    //{
    //    var useCase = new CreateBookUseCase();

    //    var response = useCase.Execute(request);

    //    return Created(string.Empty, response);
    //}

    //[HttpPut]
    //[Route("{id}")]
    //public IActionResult UpdateBook([FromRoute] Guid id) 
    //{ 

    //}

    //[HttpDelete]
    //public Task<IActionResult> DeleteBook([FromRoute] Guid id) { }
}
