using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieApp.Data;
using MovieApp.Dtos.Users;
using MovieApp.Enums;
using MovieApp.Models;
using MovieApp.Results;
using BC = BCrypt.Net.BCrypt;

namespace MovieApp.Services;

public class AuthService(AppDbContext context, IConfiguration configuration) : IAuthService
{
	private readonly AppDbContext _context = context;
	private readonly IConfiguration _config = configuration;

	public async Task<GenericResult<UserCreationResponseDto, RegisterUserErrors>> RegisterUser(UserCreationDto userData)
	{
		var (email, imageUrl, name, password) = (userData.Email, userData.ImageURL, userData.Name, userData.Password);
		// check password is strong
		if (password.Length < 5)
			return new GenericResult<UserCreationResponseDto, RegisterUserErrors>() { ErrorCode = RegisterUserErrors.PasswordNotStrongEnough };

		// check email is unique
		if (await _context.Users.AnyAsync(u => u.Email == email))
			return new GenericResult<UserCreationResponseDto, RegisterUserErrors>() { ErrorCode = RegisterUserErrors.EmailAlreadyUsed };

		// hash password
		var hashedPassword = BC.HashPassword(password);

		User newUser = new()
		{
			ImageURL = imageUrl,
			Name = name,
			Email = email,
			PasswordHash = hashedPassword,
		};

		_context.Users
			.Add(newUser);

		await _context.SaveChangesAsync();

		List<Claim> claims = [
			new Claim(ClaimTypes.NameIdentifier, newUser.Id.ToString()),
			new Claim(ClaimTypes.Email, newUser.Email)
		];

		var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
		var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

		var jwtSecurityToken = new JwtSecurityToken(
			_config["Jwt.Issuer"],
			_config["Jwt.Audience"],
			claims,
			DateTime.UtcNow,
			DateTime.UtcNow.AddHours(1),
			signingCredentials
		);

		var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

		return new GenericResult<UserCreationResponseDto, RegisterUserErrors>()
		{
			Success = true,
			Data = new(
				tokenToReturn,
				newUser.Email,
				newUser.Id.ToString()
			)
		};
	}

	public Task<string> LoginUser(string email, string password)
	{
		throw new NotImplementedException();
	}
}