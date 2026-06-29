namespace MovieApp.Dtos.Users;

public record UserWithTokenResponseDto
(
	string Token,
	string Email,
	string UserId
);