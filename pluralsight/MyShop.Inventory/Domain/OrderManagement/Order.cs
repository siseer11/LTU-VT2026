using System;

namespace MyShop.Inventory.Domain.OrderManagement;

public class Order
{
	public int Id { get; private set; }
	public DateTime OrderFulfilmentDate { get; private set; }
	public List<OrderItem> OrderItems { get; }
	public bool Fufilled { get; set; } = false;

	public Order()
	{
		Id = new Random().Next(999);

		int numberOfSec = new Random().Next(100);
		OrderFulfilmentDate = DateTime.Now.AddSeconds(numberOfSec);

		OrderItems = new List<OrderItem>();
	}
}
