using MovieApp.Dtos.Users;

namespace MovieApp.Dtos.Reviews;

public record ReviewDto(
	int Id,
	int Rating,
	string? Comment,
	bool? Edited,
	DateTime? EditedAt,
	DateTime CreatedAt,
	UserDto Reviewer
);