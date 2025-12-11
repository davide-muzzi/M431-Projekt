using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Configuration;
using API.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.DTOs.Auth;
using Shared.DTOs.Teacher;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILehrerService _lehrerService;
    private readonly JwtConfig _jwtConfig;

    public AuthController(ILehrerService lehrerService, IOptionsMonitor<JwtConfig> optionsMonitor)
    {
        _lehrerService = lehrerService;
        _jwtConfig = optionsMonitor.CurrentValue;
    }
    private string GenerateJwtToken(TeacherDTO user)
    {
        JwtSecurityTokenHandler jwtTokenHandler = new JwtSecurityTokenHandler();

        byte[] key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(6),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        SecurityToken token = jwtTokenHandler.CreateToken(tokenDescriptor);

        string jwtToken = jwtTokenHandler.WriteToken(token);

        return jwtToken;
    }

    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<AuthResponseDTO>> Login([FromBody] LoginRequestDTO login)
    {
        TeacherDTO? existingUser = await _lehrerService.GetTeacherByEmail(login.Email);

        if(existingUser == null)
        {
            return BadRequest("Failed to log in. Please check credentials");
        }

        string jwtToken = GenerateJwtToken(existingUser);
        AuthResponseDTO toReturn = new AuthResponseDTO() { Token = jwtToken };
        return Ok(toReturn);
    }
}
