using ApiKey.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
