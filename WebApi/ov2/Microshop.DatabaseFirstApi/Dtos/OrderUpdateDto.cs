using System.ComponentModel.DataAnnotations;

namespace Microshop.DatabaseFirstApi.Dtos;

public partial class OrderUpdateDto
{
	[Range(0, double.MaxValue)]
	public decimal TotalAmount { get; set; }

	[Required]
	public string Status { get; set; } = null!;
}