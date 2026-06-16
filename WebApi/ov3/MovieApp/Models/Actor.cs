namespace MovieApp.Models;

public class Actor
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public required string ImageURL { get; set; }
	public DateOnly BirthDate { get; set; }
	public ICollection<Movie> Movies { get; set; } = [];
}