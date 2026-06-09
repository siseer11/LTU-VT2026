namespace WebApiFirst.Models;

public record CarCreationDto(
	string Name,
	string Color,
	DateOnly FabricationDate
);
