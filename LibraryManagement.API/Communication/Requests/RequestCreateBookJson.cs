namespace LibraryManagement.Communication.Requests;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class RequestCreateBookJson
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("author")]
    public string Author { get; set; } = string.Empty;

    [JsonPropertyName("genre")]
    public string Genre { get; set; } = string.Empty;

    [JsonPropertyName("price")]
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    [JsonPropertyName("stock")]
    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
}
