using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorageApi.Entities;

public class Product
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	[Required]
	[MaxLength(40)]
	public string Name { get; set; }

	[Required]
	public int Price { get; set; }

	[Required]
	[MaxLength(30)]
	public string Category { get; set; }

	[MaxLength(20)]
	public string? Shelf { get; set; }
	public int? Count { get; set; }

	[MaxLength(200)]
	public string? Description { get; set; }

	public DateOnly? ExpirationDate { get; set; }

	public Product(string name, string category, int price)
	{
		Name = name;
		Category = category;
		Price = price;
	}
}
