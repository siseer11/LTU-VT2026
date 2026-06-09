using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiFirst.Models;

public class PointsOfInterestDto
{

	public int Id { set; get; }

	[Required(ErrorMessage = "The name field must be filled!")]
	[MinLength(2)]
	[MaxLength(50)]
	public string Name { set; get; } = string.Empty;
	[MaxLength(150)]
	public string? Description { set; get; }
}
