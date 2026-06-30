using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Laboration.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Laboration.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IConfiguration configuration): ControllerBase
{
	private readonly IConfiguration _config = configuration;

	[HttpPost("login")]
	public IActionResult Login([FromBody] LoginDto loginData)
	{
		string USER_NAME = "admin";
		string PASSWORD = "hemligt";
		string ROLE = "admin";

		if(loginData.Username != USER_NAME || loginData.Password != PASSWORD)
			return Unauthorized("Ogiltiga användaruppgifter.");

		var claims = new List<Claim>
		{
			new(ClaimTypes.Name, loginData.Username),
			new(ClaimTypes.Role, ROLE)
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Secret"]!));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			issuer: _config["JwtSettings:Issuer"],
			audience: _config["JwtSettings:Audience"],
			claims: claims,
			expires: DateTime.UtcNow.AddHours(1),
			signingCredentials: creds
		);

		return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token)});
	}
}
