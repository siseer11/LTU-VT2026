namespace Games.Api.Models;

public record GameUpdateDto(
	string Name,
	string Genre,
	bool HasMultiplayer,
	DateOnly ReleaseDate
);
