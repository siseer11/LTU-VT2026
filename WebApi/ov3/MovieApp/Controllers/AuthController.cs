using Microsoft.AspNetCore.Mvc;
using MovieApp.Dtos.Users;
using MovieApp.Enums;
using MovieApp.Services;

namespace MovieApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService service) : ControllerBase
{

	private readonly IAuthService _service = service;

	[HttpPost("register")]
	public async Task<ActionResult<UserWithTokenResponseDto>> RegisterUser(UserCreationDto userData)
	{
		var response = await _service.RegisterUser(userData);

		if (response.Success == false)
		{
			return response.ErrorCode switch
			{
				RegisterUserErrors.EmailAlreadyUsed =>
					BadRequest(new { error = "Email already used, login instead" }),
				RegisterUserErrors.PasswordNotStrongEnough =>
					BadRequest(new { error = "Password is not strog enough, grow it stronger!" }),
				_ => StatusCode(500)
			};
		}

		return Ok(response.Data);
	}

	[HttpPost("login")]
	public async Task<ActionResult<UserWithTokenResponseDto>> LoginUser(UserLoginDto loginData)
	{
		var response = await _service.LoginUser(loginData);

		if (response.Success == false)
		{
			return response.ErrorCode switch
			{
				LoginErrors.PasswordOrEmailWrong =>
					BadRequest(new { error = "Password or Email not correct!" }),
				_ => StatusCode(500)
			};
		}


		return Ok(response.Data);
	}
}