using System;

namespace Inheritance;

public class SmallDog(string name, int age, string color, string favToy) : Dog(name, age, color, favToy)
{

	public static new string Sound()
	{
		return "Zonk Zonk!";
	}
}
