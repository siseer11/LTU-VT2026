namespace StorageApi.Models;

public record class Product(
	int Id,
	string Name,
	int Price,
	string Category,
	string Shelf,
	int Count,
	string Description,
	DateOnly ExpirationDate
);
