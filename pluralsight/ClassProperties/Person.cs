using System;

namespace ClassProperties;

public class Person
{
	private string name;
	private int age;
	private int onlyUpdateFromWithin = 0;

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
			age = value >= 1 ? value : 1;
		}
	}

	public int OnlyUpdateFromWhitin
	{
		get { return onlyUpdateFromWithin; }
		private set
		{
			onlyUpdateFromWithin = value;
		}
	}

	public Person(string name, int age)
	{
		Age = age;
		Name = name;
	}
}
