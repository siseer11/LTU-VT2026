using MovieApp.Dtos.Users;
using MovieApp.Enums;
using MovieApp.Results;

namespace MovieApp.Services;

public interface IAuthService
{
	Task<GenericResult<UserCreationResponseDto, RegisterUserErrors>> RegisterUser(UserCreationDto userData);
	Task<string> LoginUser(string email, string password);
}
