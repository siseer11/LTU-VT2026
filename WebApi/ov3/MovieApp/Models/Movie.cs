namespace MovieApp.Models;

public class Movie
{
	public int Id { get; set; }
	public required string Title { get; set; }
	public int Year { get; set; }
	public required string ImageURL { get; set; }
	public int GenreId { get; set; }
	public Genre Genre { get; set; } = null!;
	public MovieDetails MovieDetails { get; set; } = null!;
	public ICollection<Actor> Actors { get; set; } = [];
	public ICollection<Review> Reviews { get; set; } = [];
}
