namespace StorageApi.Models;

public record class ProductCreationDto(
	string Name,
	int Price,
	string Category,
	string? Shelf,
	int? Count,
	string? Description,
	DateOnly? ExpirationDate
);
