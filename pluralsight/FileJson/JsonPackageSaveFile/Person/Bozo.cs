using System;

namespace JsonPackageSaveFile.Person;

public class Bozo
{
	private string? _color;

	public string Brand { get; }
	public string Model { get; }
	public string? Color
	{
		get => _color;
		set
		{
			if (string.IsNullOrEmpty(value) || value.Length < 2)
			{
				throw new Exception("NO COLOR LIKE THAT MY BROTHA!");
			}
			_color = value;
		}
	}

	public Bozo(string brand, string model)
	{
		if (brand.Length < 2)
		{
			throw new Exception("IMPOSIBLE BRA! Wtf is that for brand?");
		}
		Brand = brand;
		Model = model;
	}
}
