namespace MovieApp.Dtos;

public record MovieDetailsDto(
	int Id,
	string Synopsis,
	string Language,
	int Budget
);
