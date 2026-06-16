using System.ComponentModel.DataAnnotations;

namespace MovieApp.Dtos.Actors;

public record ActorCreationDto(
	[Required]
	[StringLength(100, MinimumLength = 2, ErrorMessage = "The actor name must be between 2 - 100 characters.")]
	string Name,
	[Required]
	[Url]
	string ImageURL,
	[Required]
	DateOnly BirthDate
);