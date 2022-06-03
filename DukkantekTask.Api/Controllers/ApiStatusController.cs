using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DukkantekTask.Api.Controllers
{
    /// <summary>
    /// Health check for api online status
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ApiStatusController : ControllerBase
    {
        [HttpGet("status")]
        public IActionResult Index()
        {
            return new StatusCodeResult((int)HttpStatusCode.OK);
        }
    }
}
