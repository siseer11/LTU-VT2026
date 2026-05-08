using System;

namespace FileSystem.Management;

public class Atacker(string name, int age, int jerseyNumber) : Player(name, age, jerseyNumber)
{
	public override void PrintPlayerType()
	{
		Console.WriteLine("This is a Atacker!");
	}
}

