using System;

namespace FileSystem.Management;

public class Defender(string name, int age, int jerseyNumber) : Player(name, age, jerseyNumber)
{
	public override void PrintPlayerType()
	{
		Console.WriteLine("This is a Defender!");
	}
}

