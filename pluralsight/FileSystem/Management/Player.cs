using System;

namespace FileSystem.Management;

public class Player(string name, int age, int jerseyNumber)
{
	public string Name { get; set; } = name;
	public int Age { get; set; } = age;
	public int JerseyNumber { get; set; } = jerseyNumber;

	public void PrintDetails()
	{
		Console.WriteLine($"{Name} is a player that has the number {JerseyNumber}, he is {Age} old.");
	}

	public virtual void PrintPlayerType()
	{
		Console.WriteLine("Undiscovered talent, still figuring it out!");
	}

	public static string GetPlayerType(Player player)
	{
		if (player is Goalkeeper)
			return "1";
		else if (player is Defender)
			return "2";
		else if (player is Midfielder)
			return "3";
		else if (player is Atacker)
			return "4";
		else
			return "0";
	}
}
