using System;

namespace Inheritance;

#region Old school
/*
public class Dog : Animal
{
	private string favouriteToy;

	public const int numberOfLegs = 4;

	public string FavouriteToy
	{
		get { return favouriteToy; }
		set { favouriteToy = value; }
	}

	public void Sound()
	{
		Console.WriteLine("Wof Wof!");
	}

	public Dog(string name, int age, string color, string favToy) : base(name, age, color)
	{
		FavouriteToy = favToy;
	}
}
*/
#endregion

#region New School
public class Dog(string name, int age, string color, string favToy) : Animal(name, age, color)
{
	public const int numberOfLegs = 4;
	public string FavouriteToy { get; set; } = favToy;

	public static string Sound()
	{
		return "Wof Wof!";
	}
}

#endregion