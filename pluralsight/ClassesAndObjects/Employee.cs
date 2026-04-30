using System;

namespace ClassesAndObjects;

public class Employee
{
	public string firstName;
	public string lastName;
	public string email;

	public int numberOfWorkedHours;
	public double wage;
	public double hourlyRate;

	public DateTime birthDay;

	const int minWorkedHours = 1;

	public Employee(string firstNameValue, string lastNameValue, string emailValue)
	{
		firstName = firstNameValue;
		lastName = lastNameValue;
		email = emailValue;
	}

	public void PerformWork()
	{
		PerformWork(minWorkedHours);
	}

	public void PerformWork(int nthWorkedHours)
	{
		numberOfWorkedHours += nthWorkedHours;
		Console.WriteLine($"{firstName} {lastName} has worked for {numberOfWorkedHours} hour(s).");
	}

	public double RecieveWage(bool resetHours = true)
	{
		wage = numberOfWorkedHours * hourlyRate;

		Console.WriteLine($"{firstName} {lastName} has recieved {wage}, worked for {numberOfWorkedHours} hour(s).");

		if (resetHours)
			numberOfWorkedHours = 0;

		return wage;
	}

	public void DisplayEmployeeDetails()
	{
		Console.WriteLine($"First name: \t{firstName}\nLast name: \t{lastName}\nEmail: \t{email}\nBirthday: \t{birthDay.ToShortDateString()}");
	}
}
