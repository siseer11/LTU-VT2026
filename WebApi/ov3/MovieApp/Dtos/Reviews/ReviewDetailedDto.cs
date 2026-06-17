using MovieApp.Dtos.Movies;
using MovieApp.Dtos.Users;

namespace MovieApp.Dtos.Reviews;

public record ReviewDetailedDto(
	int Id,
	int Rating,
	string? Comment,
	bool? Edited,
	DateTime? EditedAt,
	DateTime CreatedAt,
	UserDto Reviewer,
	MovieDto Movie
);