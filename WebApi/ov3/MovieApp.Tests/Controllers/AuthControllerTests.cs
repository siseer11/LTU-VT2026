using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieApp.Controllers;
using MovieApp.Dtos.Users;
using MovieApp.Enums;
using MovieApp.Results;
using MovieApp.Services;

namespace MovieApp.Tests.Controllers;

public class AuthControllerTests
{
	private readonly Mock<IAuthService> _serviceMock;
	private readonly AuthController _controller;

	public AuthControllerTests()
	{
		_serviceMock = new Mock<IAuthService>();
		_controller = new(_serviceMock.Object);
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

	[Theory]
	[InlineData(RegisterUserErrors.EmailAlreadyUsed)]
	[InlineData(RegisterUserErrors.PasswordNotStrongEnough)]
	public async Task RegisterUser_ReturnsBadRequest_IfWrongInput(RegisterUserErrors errorType)
	{
		// Arrange
		var user = GenerateUserCreationDto();
		_serviceMock
			.Setup(x => x.RegisterUser(user))
			.ReturnsAsync(
				new GenericResult<UserWithTokenResponseDto, RegisterUserErrors>()
				{
					Success = false,
					ErrorCode = errorType
				}
			);

		// Act
		var response = await _controller.RegisterUser(user);

		// Assert
		Assert.IsType<BadRequestObjectResult>(response.Result);

		_serviceMock.Verify(
			x => x.RegisterUser(user),
			Times.Once
		);
	}

	[Fact]
	public async Task RegisterUser_ReturnsOk_IfInputIsCorrect()
	{
		// Arrange
		var user = GenerateUserCreationDto();
		_serviceMock.Setup(
			x => x.RegisterUser(user)
		).ReturnsAsync(
			new GenericResult<UserWithTokenResponseDto, RegisterUserErrors>()
			{
				Success = true,
				Data = new("token", user.Email, "1")
			}
		);

		// Act
		var response = await _controller.RegisterUser(user);

		// Assert
		var okResponse = Assert.IsType<OkObjectResult>(response.Result);
		Assert.IsType<UserWithTokenResponseDto>(okResponse.Value);

		_serviceMock.Verify(
			x => x.RegisterUser(user),
			Times.Once
		);
	}

}

// GetReviewById_ReturnsNotFound_IfNoReviewFound