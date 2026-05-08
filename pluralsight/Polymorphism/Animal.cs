using System;

namespace Polymorphism;

public class Animal(string name, int age)
{
	public string Name { get; set; } = name;
	public int Age { get; set; } = age;

	public virtual void MakeSound()
	{
		Console.WriteLine("Loud_animal_sound.mp3");
	}
}
