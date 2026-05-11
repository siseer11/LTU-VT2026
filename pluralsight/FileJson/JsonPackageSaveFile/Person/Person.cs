using System;

namespace JsonPackageSaveFile.Person;

public abstract class Person
{
	public string Name { get; private set; } = string.Empty;
	public int Age { get; private set; }

	public Person(string name, int age)
	{
		Age = age;
		Name = name;
	}

	public abstract void LogDetails();
	public abstract string Icon { get; }

	public void LogSomething()
	{
		Console.WriteLine($"{Icon} - {Name} - {Age}");
	}

}
