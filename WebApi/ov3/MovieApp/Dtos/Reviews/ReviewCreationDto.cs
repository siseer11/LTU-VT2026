using System.ComponentModel.DataAnnotations;

namespace MovieApp.Dtos.Reviews;

public record ReviewCreationDto(
	[Required]
	[Range(1,5)]
	int Rating,
	[Required]
	int ReviewerId,
	[StringLength(250, MinimumLength = 2, ErrorMessage = "The comment has to be between 2 - 250 characters.")]
	string? Comment
);