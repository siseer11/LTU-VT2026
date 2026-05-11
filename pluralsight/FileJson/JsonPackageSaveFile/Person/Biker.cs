using System;

namespace JsonPackageSaveFile.Person;

public class Biker : Person
{
	public int NumberOfBikes { get; private set; }
	public override string Icon => "🚲";
	public override void LogDetails()
	{
		Console.WriteLine($"{Name} is a biker, {Age} years old, owns {NumberOfBikes}!\n Keep on rolling brotha {Icon}!");
	}

	public Biker(string name, int age, int numberOfBikes) : base(name, age)
	{
		NumberOfBikes = numberOfBikes;
	}

}
