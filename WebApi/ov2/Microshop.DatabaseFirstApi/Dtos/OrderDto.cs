namespace Microshop.DatabaseFirstApi.Dtos;

public record OrderDto(
	DateTime OrderDate,
	decimal TotalAmount,
	string Status
);