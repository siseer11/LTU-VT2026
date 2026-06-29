using System.ComponentModel.DataAnnotations;

namespace MovieApp.Dtos.Users;

public class UserCreationDto
{
	[Required]
	[StringLength(50, MinimumLength = 2)]
	public required string Name { get; set; }
	[Required]
	[Url]
	public required string ImageURL { get; set; }
	[Required]
	[EmailAddress]
	public required string Email { get; set; }
	[Required]
	public required string Password { get; set; }
}