namespace MovieApp.Models;

public class User
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public required string ImageURL { get; set; }
	public bool IsAHater { get; set; }
	public ICollection<Review> Reviews { get; set; } = [];
}