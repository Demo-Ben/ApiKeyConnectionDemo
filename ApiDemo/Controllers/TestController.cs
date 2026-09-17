using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet(Name = "ping")]
        public IActionResult Ping()
        {
            return Ok(new { Message = "API is alive", Time = DateTime.Now });
        }
    }
}
