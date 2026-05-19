using LibraryMangement.Entities;

namespace LibraryManagement.Communication.Responses;

public class ResponseAllBooksJson
{
    public List<Book> Books { get; set; } = [];
}
