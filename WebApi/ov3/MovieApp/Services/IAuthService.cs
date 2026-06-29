using MovieApp.Dtos.Users;
using MovieApp.Enums;
using MovieApp.Results;

namespace MovieApp.Services;

public interface IAuthService
{
	Task<GenericResult<UserWithTokenResponseDto, RegisterUserErrors>> RegisterUser(UserCreationDto userData);
	Task<GenericResult<UserWithTokenResponseDto, LoginErrors>> LoginUser(UserLoginDto loginData);
}
