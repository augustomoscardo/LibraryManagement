using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers;

public abstract class BaseController : ControllerBase
{
    protected string GetUserKey()
    {
        return Request.Headers["Library-User-Key"].ToString();
    }
}
