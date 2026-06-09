using System;

namespace WebApiFirst.Models;

public class CityDto
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; }

	public int GetNumberOfPointsOfInterest { get => PointsOfInterests.Count; }

	public ICollection<PointsOfInterestDto> PointsOfInterests { get; set; } = new List<PointsOfInterestDto>();
}
