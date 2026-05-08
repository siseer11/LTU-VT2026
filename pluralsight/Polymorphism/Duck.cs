using System;

namespace Polymorphism;

public class Duck(string name, int age, string color) : Animal(name, age)
{
	public string Color { get; set; } = color;
}
