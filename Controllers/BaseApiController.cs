using Identity_JWT_API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Identity_JWT_API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {

    }
}