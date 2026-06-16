using System.ComponentModel.DataAnnotations;

namespace MovieApp.Dtos.Movies;

public partial class MovieCreationDto()
{
	[Required]
	[StringLength(100, MinimumLength = 2)]
	public required string Title { get; set; }

	[Required]
	[Range(1800, 2026)]
	public int Year { get; set; }

	[Required]
	[Url]
	public required string ImageURL { get; set; }

	[Required]
	public int GenreId { get; set; }

	[Required]
	[StringLength(500, MinimumLength = 10)]
	public required string Synopsis { get; set; }

	[Required]
	[StringLength(50, MinimumLength = 2)]
	public required string Language { get; set; }

	[Required]
	[Range(0, int.MaxValue)]
	public int Budget { get; set; }

	[Required]
	public required ICollection<int> Actors { get; set; }
}