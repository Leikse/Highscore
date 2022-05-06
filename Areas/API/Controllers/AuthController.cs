using Highscore.Areas.API.Models;
using Highscore.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Highscore.Areas.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    public AuthController(
        UserManager<IdentityUser> userManager,
        IConfiguration configuration
        )
    {
        UserManager = userManager;
        Configuration = configuration;
    }

    private UserManager<IdentityUser> UserManager { get; }
    private IConfiguration Configuration { get; }

    [HttpPost]
    public async Task<IActionResult> Login(CredentialsDto credentialsDto)
    {
        var user = await UserManager.FindByNameAsync(credentialsDto.Username);

        var hasAccess = await UserManager.CheckPasswordAsync(user, credentialsDto.Password);

        if (!hasAccess)
        {
            return Unauthorized();
        }

        var tokenDto = GenerateToken(user);

        return Created("", tokenDto);
    }

    private TokenDto GenerateToken(IdentityUser identityUser)
    {
        var signingKey = Convert.FromBase64String(Configuration["Jwt:SigningSecret"]);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, identityUser.Email),
            new Claim(ClaimTypes.NameIdentifier, identityUser.Id)
        };

        var roles = UserManager.GetRolesAsync(identityUser).Result;

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(signingKey),
                SecurityAlgorithms.HmacSha256Signature),

            Subject = new ClaimsIdentity(claims)
        };

        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);

        return new TokenDto
        {
            Token = jwtTokenHandler.WriteToken(jwtSecurityToken)
        };
    }
}
