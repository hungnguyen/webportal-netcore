using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebPortal.Api.Controllers
{
    [Authorize]
    public class AuthorizeController : ControllerBase
    {
    }
}
