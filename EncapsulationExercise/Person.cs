using System;

namespace EncapsulationExercise;

public class Person
{
	private string _firstName;
	private string _lastName;
	private int _age;
	private decimal _salary;

	public string FirstName
	{
		get { return _firstName; }
		set
		{
			_firstName = value.Trim();
		}
	}

	public string LastName
	{
		get { return _lastName; }
		set
		{
			_lastName = value.Trim();
		}
	}

	public int Age
	{
		get => _age;
		set => _age = value;
	}

	public decimal Salary
	{
		get => _salary;
		set => _salary = value;
	}

	public Person(string firstName, string lastName, int age, decimal salary)
	{
		if (string.IsNullOrWhiteSpace(firstName))
			throw new ArgumentException("First name cannot be empty!", nameof(firstName));
		if (string.IsNullOrWhiteSpace(lastName))
			throw new ArgumentException("Last name cannot be empty!", nameof(lastName));
		if (age < 0)
			throw new ArgumentOutOfRangeException(nameof(age), "Age must be a positive value!");
		if (salary < 0)
			throw new ArgumentOutOfRangeException(nameof(salary), "Salary must be a positive value!");

		FirstName = firstName;
		LastName = lastName;
		Age = age;
		Salary = salary;
	}

	public void IncreaseSalary(decimal percentage)
	{
		decimal actualPercentage = percentage;
		if (Age < 30)
			actualPercentage /= 2;

		Salary += Salary * (actualPercentage / 100);
	}
	public override string ToString() => $"{FirstName} {LastName} receives {Salary} dollars.";
}
