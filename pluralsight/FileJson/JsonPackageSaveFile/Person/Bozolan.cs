using System;

namespace JsonPackageSaveFile.Person;

public class Bozolan(string brand, string model) : Bozo(brand, model)
{
	public void getColorString()
	{
		Console.WriteLine($"My color is : {Color}");
	}
}
