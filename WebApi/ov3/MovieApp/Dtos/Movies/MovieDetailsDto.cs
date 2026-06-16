namespace MovieApp.Dtos.Movies;

public record MovieDetailsDto(
	int Id,
	string Synopsis,
	string Language,
	int Budget
);
