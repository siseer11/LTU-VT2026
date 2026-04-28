using System;

namespace _1_Personalregister;

public class Employee
{
	public string name;
	public int salary;

	public Employee(string n, int s)
	{
		name = n;
		salary = s;
	}

	public void DisplayEmployeeDetails()
	{
		Console.WriteLine($"\nName: \t{name}\nSalary: {salary}Kr\n");
	}
}
