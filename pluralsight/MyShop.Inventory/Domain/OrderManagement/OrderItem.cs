using System;

namespace MyShop.Inventory.Domain.OrderManagement;

public class OrderItem
{
	public int Id { get; set; }
	public int ProductId { get; set; }
	public string ProductName { get; set; } = string.Empty;
	public int AmountOrdered { get; set; }

}
