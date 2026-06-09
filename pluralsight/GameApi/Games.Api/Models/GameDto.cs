namespace Games.Api.Models;

public record GameDto(
	int Id,
	string Name,
	string Genre,
	bool HasMultiplayer,
	DateOnly ReleaseDate
);
