using System.Net;

namespace LibraryManagement.Exceptions.ExceptionsBase;

public class NotFoundException : LibraryManagementException
{
    public NotFoundException(string errorMessage) : base(errorMessage)
    {}

    public override List<string> GetErrors() => [Message];

    public override HttpStatusCode GetHttpStatusCode() => HttpStatusCode.NotFound;
}
