using System;
using System.Security.Cryptography;

namespace Inheritance;

#region Old School
/*
public class Animal
{
	private string name;
	private int age;
	private string color;

	public string Name
	{
		get { return name; }
		set
		{
			name = value;
		}
	}

	public int Age
	{
		get { return age; }
		set
		{
			age = value > 0 ? value : 0;
		}
	}

	public string Color
	{
		get { return color; }
		set
		{
			color = value;
		}
	}

	// Constructor
	public Animal(string name, int age, string color)
	{
		Name = name;
		Age = age;
		Color = color;
	}
}
*/
#endregion

#region New School (more JS-like)
public class Animal(string name, int age, string color)
{
	private int _age = age;

	// For the ones we don't need any validation we can do this
	public string Name { set; get; } = name;
	public string Color { set; get; } = color;

	// for the ones we need validation, the best way is still to do it this way.	
	public int Age
	{
		get { return _age; }
		set
		{
			_age = value > 0 ? value : 0;
		}
	}

}

#endregion