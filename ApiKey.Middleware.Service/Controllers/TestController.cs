using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiKey.Shared;

namespace ApiKey.Middleware.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Ping()
        {
            //return Ok(new { Message = "API Key has found.", Time = DateTime.Now });
            return Ok(new PingResponseDto { Message = "API Key has found.", Time = DateTime.Now });
        }
    }
}
