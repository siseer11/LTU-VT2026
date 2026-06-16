namespace MovieApp.Dtos.Actors;

public record ActorDto(
	int Id,
	string Name,
	string ImageURL,
	DateOnly BirthDate
);