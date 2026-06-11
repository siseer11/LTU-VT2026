namespace Microshop.DatabaseFirstApi.Dtos;

public record OrderDto(
	int OrderId,
	DateTime OrderDate,
	decimal TotalAmount,
	string Status,
	int CustomerId
);