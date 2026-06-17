namespace MovieApp.Dtos.Users;

public record UserDto(
	int Id,
	string Name,
	string ImageURL,
	bool IsAHater
);
