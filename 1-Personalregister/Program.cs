using _1_Personalregister;

EmployeesRegistry employeesRegistery = new();

int userSelection;
const int exitNumber = 3;

do
{
	Console.WriteLine("Select an option (by typing the corresponding number):");
	Console.WriteLine("1 - Add new employee");
	Console.WriteLine("2 - List all employees");
	Console.WriteLine($"{exitNumber} - Exit app");

	bool validNumber = int.TryParse(Console.ReadLine(), out userSelection);

	if (!validNumber)
	{
		Console.WriteLine("\nTry again! Please insert a valid number.\n");
		continue;
	}

	switch (userSelection)
	{
		case 1:
			{
				HandleNameAndSalaryInsertion();
				break;
			}
		case 2:
			{
				employeesRegistery.DisplayAllEmployees();
				break;
			}
		case 3:
			{
				Console.WriteLine("Aplication exited, Tanks for using our app!");
				break;
			}
		default:
			Console.WriteLine("Try again! Insert a number between 1-3.");
			break;
	}

} while (userSelection != exitNumber);


void HandleNameAndSalaryInsertion()
{
	bool validNewUserData = false;
	string? name = null;
	int salary = 0;

	Console.WriteLine("\nInsert the new employee details");
	do
	{
		if (name == null)
		{
			Console.WriteLine("Name:");

			string? insertedName = Console.ReadLine();

			if (insertedName == null || insertedName.Trim().Length < 2)
			{
				Console.WriteLine("\nPlease insert a valid name! It must have at least 2 letters.");
				continue;
			}

			name = insertedName!;
		}

		Console.WriteLine("Salary:");
		string? insertedSalary = Console.ReadLine();
		bool salaryParsedSuccess = int.TryParse(insertedSalary, out salary);

		if (!salaryParsedSuccess)
		{
			Console.WriteLine("\nPlease insert a valid int for the salary!");
			continue;
		}

		validNewUserData = true;
	} while (!validNewUserData);

	employeesRegistery.AddNewEmployee(name!, salary);
	Console.WriteLine("\nThe new employee was added successfully!\n");
}