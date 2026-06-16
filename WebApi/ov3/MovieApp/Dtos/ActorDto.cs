namespace MovieApp.Dtos;

public record ActorDto(
	int Id,
	string Name,
	string ImageURL,
	DateOnly BirthDate
);