using System;

namespace StaticUsage;

public class Person
{
	public string _name;
	public int _age;

	public static double tax = 0.4;

	public Person(string name, int age)
	{
		_name = name;
		_age = age;
	}

	public void LogTax()
	{
		Console.WriteLine($"The tax for {_name}, age: {_age} is : {tax}");
	}

}
