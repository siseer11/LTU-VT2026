using System.ComponentModel.DataAnnotations;

namespace Microshop.DatabaseFirstApi.Dtos;

public partial class OrderCreationDto
{
	[Required]
	public int CustomerId { get; set; }

	[Range(0, double.MaxValue)]
	public decimal TotalAmount { get; set; }

	[Required]
	public string Status { get; set; } = null!;

}
