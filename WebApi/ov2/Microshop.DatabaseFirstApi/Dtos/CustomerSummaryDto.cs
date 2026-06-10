namespace Microshop.DatabaseFirstApi.Dtos;

public record CustomerSummaryDto(
	string FirstName,
	string LastName,
	string Email,
	string? Phone
);
