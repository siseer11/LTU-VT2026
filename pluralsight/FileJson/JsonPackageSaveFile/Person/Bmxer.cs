using System;

namespace JsonPackageSaveFile.Person;

public class Bmxer(string name, int age, bool hasPegs) : Biker(name, age, 1)
{


	public override string Icon => "🚴‍♂️🤟";
	public bool HasPegs => hasPegs;

	public override void LogDetails()
	{
		Console.WriteLine($"{Name} is a BMXer 👍, {Age} years old, owns {NumberOfBikes}, Has pegs: {HasPegs}!\n Keep on jumping brotha {Icon}!");
	}

}
