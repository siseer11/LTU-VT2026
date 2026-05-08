using System;

namespace Polymorphism;

public class Dog(string name, int age) : Animal(name, age)
{
	public override void MakeSound()
	{
		Console.WriteLine("Hau Hau!");
	}
}
