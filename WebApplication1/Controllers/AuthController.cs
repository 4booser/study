using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("new_list")]
    public class AuthController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("API работает");
        }

        [HttpPost("post")]
        public IActionResult Post(string _list)
        {
            Console.WriteLine(_list);
            return Ok("API работает");
        }
    }
}




 

