namespace MovieApp.Models;

public class Genre
{
	public int Id { get; set; }
	public required string Name { get; set; }
	public bool ChildrenSafe { get; set; }
	public ICollection<Movie> Movies { get; set; } = [];
}