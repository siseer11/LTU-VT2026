namespace MovieApp.Models;

public class Actor
{
	public int Id { get; set; }
	public required string Name { get; set; }
	public required string ImageURL { get; set; }
	public DateOnly BirthDate { get; set; }
	public ICollection<Movie> Movies { get; set; } = [];
}