using Microsoft.AspNetCore.Mvc;
using SwapApp.Models;
using SwapApp.Services;

namespace SwapApp.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromForm] RegisterUserDto registerDto)
        {
            _accountService.RegisterUser(registerDto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromForm] LoginDto loginDto)
        {
            string token = _accountService.GenerateJwt(loginDto);
            return Ok(token);
        }
    }
}
