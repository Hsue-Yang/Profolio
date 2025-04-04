using Microsoft.AspNetCore.Mvc;
using Profolio.Server.Filters;

namespace Profolio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(ExceptionFilter))]
    public class BaseController : ControllerBase
    {

    }
}