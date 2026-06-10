namespace Microshop.DatabaseFirstApi.Dtos;

public record CustomerDetailsDto(
	string FirstName,
	string LastName,
	string Email,
	string? Phone,
	ICollection<OrderDto> Orders
);
