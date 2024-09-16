
using Microsoft.AspNetCore.Mvc;
using PracticeApp.Middleware;
using Repository;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        public IAuthInterface _authInterface;
        public AuthController(IAuthInterface authInterface)
        {
            _authInterface = authInterface;
        }

        [HttpGet("signin")]
        public IActionResult SignIn(string username, string password)
        {
            return Ok(_authInterface.SignIn(username, password));
        }
        [Authorize]
        [HttpGet("privatedata")]
        public IActionResult GetData(string jwtToken)
        {
            return Ok(_authInterface.GetData(jwtToken));
        }
    }
}
