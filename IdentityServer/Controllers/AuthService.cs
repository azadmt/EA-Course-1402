using Identity.Contract;
using IdentityServer.Controllers;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService
{
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Create(User user)
    {
        var handler = new JwtSecurityTokenHandler();

        var claims = GenerateClaims(user).Claims;
        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddDays(30),
    signingCredentials: new SigningCredentials(
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
        SecurityAlgorithms.HmacSha256Signature)
    );
        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(User user)
    {
        var ci = new ClaimsIdentity();

        ci.AddClaim(new Claim("id", user.Id.ToString()));
        ci.AddClaim(new Claim(ClaimTypes.Name, user.Username));
        ci.AddClaim(new Claim(ClaimTypes.GivenName, user.Username));
        ci.AddClaim(new Claim(ClaimTypes.Email, user.EmailAddress));

        foreach (var role in user.Roles)
        {
            ci.AddClaim(new Claim(ClaimTypes.Role, role));
        }

        var rolePermissions = PermissionManager.GetRolePermissions(user.Roles);
        foreach (var item in rolePermissions)
        {
            ci.AddClaim(new Claim("Permission", item));
        }

        var userPermissions = PermissionManager.GetUserPermissions(user.Username);
        foreach (var item in userPermissions)
        {
            ci.AddClaim(new Claim("Permission", item));
        }
        return ci;
    }
}