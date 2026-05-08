using System;
using MyShop.Inventory.Utils;

namespace MyShop.Inventory.Domain.ProductManagement;

public partial class Product
{
	private void UpdateLowStockStatus()
	{
		IsBelowStockTreshold = AmountInStock < StockTreshold;
	}

	private void DecreaseStock(int numberOfItems, string reason)
	{
		if (numberOfItems <= AmountInStock)
		{
			AmountInStock -= numberOfItems;
		}
		else
		{
			AmountInStock = 0;
		}

		UpdateLowStockStatus();
		ConsoleUtils.Log(reason);
	}
}
