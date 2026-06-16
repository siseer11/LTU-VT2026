namespace MovieApp.Dtos;

public record ReviewDto(
	int Id,
	int Rating,
	string? Comment,
	DateTime CreatedAt,
	UserDto Reviewer
);