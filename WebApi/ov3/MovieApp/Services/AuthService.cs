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

	private string GenerateJwtToken(int userId, string userEmail)
	{
		List<Claim> claims = [
			new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
			new Claim(ClaimTypes.Email, userEmail)
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

		return tokenToReturn;
	}

	public async Task<GenericResult<UserWithTokenResponseDto, RegisterUserErrors>> RegisterUser(UserCreationDto userData)
	{
		var (email, imageUrl, name, password) = (userData.Email, userData.ImageURL, userData.Name, userData.Password);
		// check password is strong
		if (password.Length < 5)
			return new GenericResult<UserWithTokenResponseDto, RegisterUserErrors>() { ErrorCode = RegisterUserErrors.PasswordNotStrongEnough };

		// check email is unique
		if (await _context.Users.AnyAsync(u => u.Email == email))
			return new GenericResult<UserWithTokenResponseDto, RegisterUserErrors>() { ErrorCode = RegisterUserErrors.EmailAlreadyUsed };

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

		var tokenToReturn = GenerateJwtToken(newUser.Id, newUser.Email);

		return new GenericResult<UserWithTokenResponseDto, RegisterUserErrors>()
		{
			Success = true,
			Data = new(
				tokenToReturn,
				newUser.Email,
				newUser.Id.ToString()
			)
		};
	}

	public async Task<GenericResult<UserWithTokenResponseDto, LoginErrors>> LoginUser(UserLoginDto loginData)
	{
		var (email, passowrd) = (loginData.Email, loginData.Password);

		// checks
		var user = await _context.Users
			.Where(u => u.Email == email)
			.FirstOrDefaultAsync();

		if (user is null)
			return new() { Success = false, ErrorCode = LoginErrors.PasswordOrEmailWrong };

		var isPasswordCorrect = BC.Verify(passowrd, user.PasswordHash);
		if (!isPasswordCorrect)
			return new() { Success = false, ErrorCode = LoginErrors.PasswordOrEmailWrong };

		var tokenToReturn = GenerateJwtToken(user.Id, user.Email);

		return new GenericResult<UserWithTokenResponseDto, LoginErrors>()
		{
			Success = true,
			Data = new(
				tokenToReturn,
				user.Email,
				user.Id.ToString()
			)
		};
	}
}