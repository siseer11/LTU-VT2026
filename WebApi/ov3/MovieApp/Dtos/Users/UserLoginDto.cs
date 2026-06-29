using System.ComponentModel.DataAnnotations;

namespace MovieApp.Dtos.Users;

public class UserLoginDto
{
	[Required]
	[EmailAddress]
	public required string Email { get; set; }
	[Required]
	public required string Password { get; set; }
}