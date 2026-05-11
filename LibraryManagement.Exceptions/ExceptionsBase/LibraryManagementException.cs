using System.Net;

namespace LibraryManagement.Exceptions.ExceptionsBase;

public abstract class LibraryManagementException : SystemException
{
    public LibraryManagementException(string errorMessage) : base(errorMessage)
    {
    }

    public abstract List<string> GetErrors();
    public abstract HttpStatusCode GetHttpStatusCode();
}
