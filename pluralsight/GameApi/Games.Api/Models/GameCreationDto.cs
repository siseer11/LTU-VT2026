namespace Games.Api.Models;

public record GameCreationDto(
	string Name,
	string Genre,
	bool HasMultiplayer,
	DateOnly ReleaseDate
);
