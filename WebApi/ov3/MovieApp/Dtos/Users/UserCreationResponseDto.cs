namespace MovieApp.Dtos.Users;

public record UserCreationResponseDto
(
	string Token,
	string Email,
	string UserId
);