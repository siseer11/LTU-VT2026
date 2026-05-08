using System;

namespace Interface;

public class Dog(string name, string color) : Animal(name)
{
	public string Color { get; set; } = color;

	public static void DogStuff()
	{
		Console.WriteLine("Go fetch!");
	}
}
