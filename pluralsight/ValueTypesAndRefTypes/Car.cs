using System;

namespace ValueTypesAndRefTypes;

public class Car(int fabYear, string carBrand)
{
	public readonly int year = fabYear;
	public string brand = carBrand;

	public CarTypeEnums carType = carBrand == "Mazda" ? CarTypeEnums.Bensin : CarTypeEnums.Electric;

	public void LogDetails()
	{
		Console.WriteLine($"Car Brand: {brand}, from year: {year}.");
	}
}
