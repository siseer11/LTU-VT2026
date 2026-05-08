using System;

namespace Interface;

public class Animal(string name) : IAnimal
{
	public string Name { get; set; } = name;

	public void DoSound()
	{
		Console.WriteLine("AnimalSound.mp3");
	}

	public string NameWithSomethingAtTheEnd(char endChar)
	{
		return $"{Name}{endChar}";
	}

	public static int ReturnANumber()
	{
		return new Random(10).Next();
	}
}
