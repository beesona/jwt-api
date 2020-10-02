using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using jwtApi.Services;
using jwtApi.models;
using System.Diagnostics;

namespace jwtApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            Stopwatch runWatch = Stopwatch.StartNew();

            var user = _userService.Authenticate(model.Username, model.Password);
            runWatch.Stop();
            var callTime = runWatch.ElapsedMilliseconds;

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            user.RunTimeInMs = callTime.ToString();
            Response.Headers.Add("run-time-in-ms", callTime.ToString());
            return Ok(user);
        }
    }
}
