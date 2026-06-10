using System.ComponentModel.DataAnnotations;

namespace Microshop.DatabaseFirstApi.Dtos;

public partial class CustomerCreationDto
{
	[Required]
	[StringLength(50, MinimumLength = 2, ErrorMessage = "The First Name has to be between 2-50 characters.")]
	public string FirstName { get; set; } = null!;

	[Required]
	[StringLength(50, MinimumLength = 2, ErrorMessage = "The Last Name has to be between 2-50 characters.")]
	public string LastName { get; set; } = null!;

	[Required]
	[EmailAddress]
	public string Email { get; set; } = null!;

	[Phone]
	public string? Phone { get; set; }
}
