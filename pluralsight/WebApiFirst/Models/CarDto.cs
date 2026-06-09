namespace WebApiFirst.Models;

public record CarDto(
	int Id,
	string Name,
	string Color,
	DateOnly FabricationDate
);
