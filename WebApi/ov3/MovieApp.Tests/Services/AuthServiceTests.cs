using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieApp.Data;
using MovieApp.Dtos.Users;
using MovieApp.Enums;
using MovieApp.Models;
using MovieApp.Services;
using MovieApp.Tests.Helpers;

namespace MovieApp.Tests.Services;

public class AuthServiceTests
{
	private readonly AppDbContext _context;
	private readonly IAuthService _service;

	public AuthServiceTests()
	{
		_context = TestDbContextFactory.Create();

		var configuration = new ConfigurationBuilder()
		.AddInMemoryCollection(new Dictionary<string, string?>
		{
			["Jwt:Key"] = "ThisIsATestJwtSecretKeyThatIsLongEnough",
			["Jwt:Issuer"] = "MovieApp",
			["Jwt:Audience"] = "MovieAppUsers"
		})
		.Build();

		_service = new AuthService(_context, configuration);
	}

	public async Task<User> CreateAndSaveUser(
		string Email = "test@gmail.com",
		string ImageURL = "https://media.test.test.jpg",
		string Name = "test1",
		string Password = "testtest"
	)
	{
		var user = new User()
		{
			Email = Email,
			ImageURL = ImageURL,
			Name = Name,
			PasswordHash = Password
		};

		_context.Users.Add(user);
		await _context.SaveChangesAsync();

		return user;
	}

	public static UserCreationDto GenerateUserCreationDto(
		string Email = "test@gmail.com",
		string ImageURL = "https://media.test.test.jpg",
		string Name = "test1",
		string Password = "testtest"
	)
	{
		return new UserCreationDto()
		{
			Email = Email,
			ImageURL = ImageURL,
			Name = Name,
			Password = Password
		};
	}

	[Fact]
	public async Task RegisterUser_ReturnsError_WhenEmailIsNotUnique()
	{
		// Arrange
		var email = "test@gmail.com";
		var firstUserIn = await CreateAndSaveUser(email);

		// Act
		var result = await _service.RegisterUser(GenerateUserCreationDto(email));

		// Assert
		Assert.False(result.Success);
		Assert.Equal(RegisterUserErrors.EmailAlreadyUsed, result.ErrorCode);
	}

	[Fact]
	public async Task RegisterUser_ReturnsError_WhenPasswordIsWeak()
	{
		// Arrange

		// Act
		var result = await _service.RegisterUser(GenerateUserCreationDto(Password: "123"));

		// Assert
		Assert.False(result.Success);
		Assert.Equal(RegisterUserErrors.PasswordNotStrongEnough, result.ErrorCode);
	}

	[Fact]
	public async Task RegisterUser_UpdatesDatabaseAndCreatesTheUser_WhenDataIsCorrect()
	{
		// Arrange
		var email = "test@gmail.com";
		var password = "strongPassword";
		var userToBeRegistered = GenerateUserCreationDto(Email: email, Password: password);
		// Act
		var result = await _service.RegisterUser(userToBeRegistered);

		// Assert
		Assert.True(result.Success);
		Assert.IsType<UserCreationResponseDto>(result.Data);
		Assert.Equal(result.Data.Email, email);

		var userFromDb = await _context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == result.Data.UserId);
		Assert.NotNull(userFromDb);
		Assert.Equal(userFromDb.Email, email);
		Assert.False(userFromDb.IsAHater);
		Assert.NotEqual(userToBeRegistered.Password, userFromDb.PasswordHash);
		Assert.True(BCrypt.Net.BCrypt.Verify(userToBeRegistered.Password, userFromDb.PasswordHash));
		Assert.False(string.IsNullOrWhiteSpace(result.Data.Token));

		var handler = new JwtSecurityTokenHandler();
		var token = handler.ReadJwtToken(result.Data.Token);
		Assert.Equal(email, token.Claims.First(c => c.Type == ClaimTypes.Email).Value);
	}

}
