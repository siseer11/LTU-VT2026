using System;
using System.Text;
using MyShop.Inventory.Utils;
using MyShop.Inventory.Domain.ProductManagement;
using MyShop.Inventory.Domain.General;

namespace MyShop.Inventory.Domain.ProductManagement;

public partial class Product
{
	private static int StockTreshold = 10;
	public static void ChangeStockTreshold(int newValue)
	{
		StockTreshold = newValue;
	}

	#region Private props
	private string _name = string.Empty;
	private string? _description;

	#endregion

	#region GetSet
	public int Id { get; set; }
	public string Name
	{
		get => _name;
		set
		{
			_name = value.Length > 50 ? value[..50] : value;
		}
	}

	public string? Description
	{
		get => _description;
		set
		{
			if (value == null)
			{
				_description = string.Empty;
			}
			else
			{
				_description = value.Length > 250 ? value[..250] : value;
			}
		}
	}

	public UnitType UnitType { get; set; }
	public int AmountInStock { get; set; }

	public Price Price { get; set; }
	public bool IsBelowStockTreshold { get; private set; }

	public int MaxItemsInStock { get; private set; }
	#endregion

	#region Constructors
	public Product(int id) : this(id, string.Empty) { }
	public Product(int id, string name)
	{
		Id = id;
		Name = name;
	}

	public Product(int id, string name, string? description, Price price, UnitType unitType, int maxAmountInStock)
	{
		Id = id;
		Name = name;
		Description = description;
		UnitType = unitType;
		Price = price;

		MaxItemsInStock = maxAmountInStock;

		UpdateLowStockStatus();
	}


	#endregion

	public void UseProduct(int quantity)
	{
		if (quantity <= AmountInStock)
		{
			AmountInStock -= quantity;

			UpdateLowStockStatus();

			ConsoleUtils.Log($"✅ Amount in stock updated. Now we have {AmountInStock} items in stcok.");
		}
		else
		{
			ConsoleUtils.Log($"⛔ Not enough items in stock. {quantity} wanted, while only {AmountInStock} present.", ConsoleColor.Red);
		}
	}

	public void IncreaseStock()
	{
		AmountInStock++;
	}

	public void IncreaseStock(int amount)
	{
		int newStock = AmountInStock + amount;

		if (newStock <= MaxItemsInStock)
			AmountInStock = newStock;
		else
		{
			AmountInStock = MaxItemsInStock;
			ConsoleUtils.Log($"⚠️ {CreateSimpleProductRepresentation()} stock overflow. {newStock - MaxItemsInStock} item(s) ordered that couldn't be stored.", ConsoleColor.DarkYellow);
		}

		UpdateLowStockStatus();
	}

	public string CreateSimpleProductRepresentation()
	{
		return $"Product {Id} ({Name})";
	}

	public string DisplayDetailsShort()
	{
		return $"{Id}. {Name} \n{AmountInStock} items in stock";
	}

	public string DisplayDetailsFull()
	{
		StringBuilder sb = new();
		sb.Append($"{Id} {Name} \n{Description}\n{Price.ToString()}\n{AmountInStock} item(s) in stock");

		if (IsBelowStockTreshold)
			sb.Append("\n⚠️ STOCK LOW ⚠️");

		return sb.ToString();
	}

	public string DisplayDetailsFull(string extraDetails)
	{
		StringBuilder sb = new();
		sb.Append($"{Id} {Name} \n{Description}\n{Price.ToString()}\n{AmountInStock} item(s) in stock");
		sb.Append(extraDetails);

		if (IsBelowStockTreshold)
			sb.Append("\n⚠️ STOCK LOW ⚠️");

		return sb.ToString();
	}
}
