namespace MovieApp.Models;

public class MovieDetails
{
	public int Id { get; set; }
	public required string Synopsis { get; set; }
	public required string Language { get; set; }
	public int Budget { get; set; }
	public int MovieId { get; set; }
	public Movie Movie { get; set; } = null!;
}