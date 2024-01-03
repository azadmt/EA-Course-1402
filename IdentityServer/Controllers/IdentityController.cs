using Identity.Contract;

using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly ILogger<IdentityController> _logger;

        public IdentityController(ILogger<IdentityController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            var token = SignIn(loginModel);//Or retutn securitycontext as token
            if (string.IsNullOrEmpty(token)) { return Unauthorized(); }
            return Ok(new { Token = token });
        }

        [HttpGet]
        public IActionResult Get(string token)
        {
            var securityContext = InMemoryDB.Tokens[token];
            if (securityContext is null || securityContext.ExpireAt < DateTime.Now) { return Unauthorized(); }
            return Ok(securityContext);
        }

        private string SignIn(LoginModel loginModel)
        {
            string token = string.Empty;
            var user = InMemoryDB.Users.Single(x => x.Username == loginModel.Username);

            if (user is null || user.Password != loginModel.Password) { return token; }

            token = Guid.NewGuid().ToString();
            var permissions = new List<string>();

            InMemoryDB.Tokens.Add
                (token,
                new SecurityContext()
                {
                    Permissions = PermissionManager.GetUserPermissions(user),
                    Username = loginModel.Username,
                    ExpireAt = DateTime.Now.AddMinutes(30)
                });
            return token;
        }
    }
}