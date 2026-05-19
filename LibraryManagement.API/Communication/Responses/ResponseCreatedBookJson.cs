namespace LibraryManagement.API.Communication.Responses;

public class ResponseCreatedBookJson
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
}
