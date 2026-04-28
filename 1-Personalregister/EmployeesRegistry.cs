using System;

namespace _1_Personalregister;

public class EmployeesRegistry
{
	public List<Employee> listOfEmployees = [];

	public void AddNewEmployee(string name, int salary)
	{
		listOfEmployees.Add(new Employee(name, salary));
	}

	public void DisplayAllEmployees()
	{
		if (listOfEmployees.Count == 0)
		{
			Console.WriteLine("\nThere are no employees, go to linkedin and get some!\n");
		}
		else
		{
			Console.WriteLine("\n----- Employees list ------");
			foreach (Employee employee in listOfEmployees)
			{
				employee.DisplayEmployeeDetails();
			}
			Console.WriteLine("---------------------------\n");
		}
	}
}
