namespace MovieApp.Dtos;

public record UserDto(
	int Id,
	string Name,
	string ImageURL,
	bool IsAHater
);
