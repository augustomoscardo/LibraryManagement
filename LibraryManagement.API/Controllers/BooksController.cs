using LibraryManagement.API.Communication.Responses;
using LibraryManagement.API.Controllers;
using LibraryManagement.Communication.Requests;
using LibraryManagement.Communication.Responses;
using LibraryMangement.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Library_Mangement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : BaseController
{
    //protected List<Book> _books = [];
    private static readonly List<Book> _books = [];

    private static readonly HashSet<string> _availableGenres =
    [
        "Programming",
        "Architecture",
        "Science Fiction",
        "Fantasy",
        "Mystery",
        "Romance",
        "History",
        "Biography",
        "Technology",
        "Action",
        "Adventure",
        "Thriller",
        "Horror",
        "Self-Help",
        "Health",
        "Drama",
    ];

    [HttpPost]
    [ProducesResponseType(typeof(ResponseCreatedBookJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status409Conflict)]
    public IActionResult CreateBook([FromBody] RequestCreateBookJson request)
    {
        if (string.IsNullOrEmpty(request.Title) || string.IsNullOrEmpty(request.Author) || string.IsNullOrEmpty(request.Genre))
        {
            return BadRequest(new ResponseErrorMessagesJson("Title, Author and Genre are required."));
        }   

        if (string.IsNullOrEmpty(request.Genre) || !_availableGenres.Contains(request.Genre))
        {
            return BadRequest(new ResponseErrorMessagesJson("Invalid genre."));
        }

        if (request.Price < 0 || request.Stock < 0)
        {
            return BadRequest(new ResponseErrorMessagesJson("Price and Stock cannot be negative."));
        }

        var existingBook = _books.Find(book => book.Title.Equals(request.Title, StringComparison.OrdinalIgnoreCase) && book.Author.Equals(request.Author, StringComparison.OrdinalIgnoreCase));

        if (existingBook != null) {
            return Conflict("A book with the same title and author already exists.");
        }

        var newBook = new Book
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Author = request.Author,
            Genre = request.Genre,
            Price = request.Price,
            Stock = request.Stock,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _books.Add(newBook);

        return Created(string.Empty, new ResponseCreatedBookJson { Id = newBook.Id, Title = newBook.Title });
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseAllBooksJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status400BadRequest)]
    public IActionResult GetBooks()
    {
        var books = _books;

        if (books == null || books.Count == 0) 
        {
            return NoContent();
        }

        return Ok(books);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ResponseBookJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status404NotFound)]
    public IActionResult GetBook([FromRoute] Guid id)
    {
        var book = _books.Find(book => book.Id == id);

        if (book == null)
        {
            var errorResponse = new ResponseErrorMessagesJson("Book not found!");

            return NotFound(errorResponse);
        }

        

        return Ok(book);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseBookJson), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status404NotFound)]
    public IActionResult UpdateBook([FromRoute] Guid id, [FromBody] RequestCreateBookJson request)
    {
        var book = _books.Find(book => book.Id == id);

        if (book == null)
        {
            var errorResponse = new ResponseErrorMessagesJson("Book not found!");

            return NotFound(errorResponse);
        }

        if (string.IsNullOrEmpty(request.Title) || string.IsNullOrEmpty(request.Author) || string.IsNullOrEmpty(request.Genre))
        {
            return BadRequest(new ResponseErrorMessagesJson("Title, Author and Genre are required."));
        }

        if (string.IsNullOrEmpty(request.Genre) || !_availableGenres.Contains(request.Genre))
        {
            return BadRequest(new ResponseErrorMessagesJson("Invalid genre."));
        }

        if (request.Price < 0 || request.Stock < 0)
        {
            return BadRequest(new ResponseErrorMessagesJson("Price and Stock cannot be negative."));
        }

        var existingBook = _books.Find(book => book.Title.Equals(request.Title, StringComparison.OrdinalIgnoreCase) && book.Author.Equals(request.Author, StringComparison.OrdinalIgnoreCase));

        if (existingBook != null)
        {
            return Conflict("A book with the same title and author already exists.");
        }

        book.Title = request.Title;
        book.Author = request.Author;
        book.Genre = request.Genre;
        book.Price = request.Price;
        book.Stock = request.Stock;
        book.UpdatedAt = DateTime.UtcNow;

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ResponseBookJson), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status404NotFound)]
    public IActionResult DeleteBook([FromRoute] Guid id) {
        var book = _books.Find(book => book.Id == id);

        if (book == null)
        {
            var errorResponse = new ResponseErrorMessagesJson("Book not found!");

            return NotFound(errorResponse);
        }

        _books.Remove(book);

        return NoContent();
    }
}
