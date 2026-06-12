using System.ComponentModel.DataAnnotations;

namespace WebApiFirst.Models;

public class PointsOfInterestsPartialUpdateDto
{
	[MinLength(2)]
	[MaxLength(50)]
	public string? Name { set; get; } = string.Empty;
	[MaxLength(150)]
	public string? Description { set; get; }
}