using System;

namespace JsonPackageSaveFile.Person;

public class Snowboarder(string name, int age, string favouriteSnowboardBrand) : Person(name, age)
{
	public override string Icon => "🏂";

	public string FavouriteSnowboardBrand { get; } = favouriteSnowboardBrand;

	public override void LogDetails()
	{
		Console.WriteLine($"{Name} is a snowboarder 🥰, {Age} years old, favourite brand: {FavouriteSnowboardBrand}!\n Keep on sliding brotha {Icon}!");
	}

}
