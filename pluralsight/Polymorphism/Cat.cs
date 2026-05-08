using System;

namespace Polymorphism;

public class Cat(string name, int age) : Animal(name, age)
{
	public override void MakeSound()
	{
		Console.WriteLine("Miaw Miaw!");
	}
}
