using Identity.Contract;

using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly ILogger<IdentityController> _logger;
        private readonly AuthService _authService;

        public IdentityController(ILogger<IdentityController> logger, AuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpPost()]
        [Route("SignInUser")]
        public IActionResult SignInUser(LoginModel loginModel)
        {
            var user = InMemoryDB.Users.Single(x => x.Username == loginModel.Username);

            if (user is null || user.Password != loginModel.Password) { return Forbid(); }

            var token = _authService.Create(user);
            if (string.IsNullOrEmpty(token)) { return Unauthorized(); }
            return Ok(new { Token = token });
        }
    }
}