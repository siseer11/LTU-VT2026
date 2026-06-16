using MovieApp.Dtos.Movies;

namespace MovieApp.Dtos.Actors;

public record ActorDetailedDto(
	int Id,
	string Name,
	string ImageURL,
	DateOnly BirthDate,
	List<MovieDto> Movies
);
