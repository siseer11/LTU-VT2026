using EncapsulationExercise;
/*
int lines = 3;
List<Person> persons = [];

for (int i = 0; i < lines; i++)
{
	Console.Write($"Add data for person {i + 1}: ");
	try
	{
		string[] cmdArgs = Console.ReadLine()!.Split();
		Person person = new(cmdArgs[0], cmdArgs[1], int.Parse(cmdArgs[2]));
		persons.Add(person);
	}
	catch (Exception ex)
	{
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine(ex.Message);
		Console.ResetColor();
	}
}

persons.OrderBy(person => person.FirstName).ThenBy(person => person.Age).ToList().ForEach(person => Console.WriteLine(person.ToString()));
*/

int lines = 3;
List<Person> persons = [];

for (int i = 0; i < lines; i++)
{
	Console.Write($"Add data for person {i + 1}: ");
	try
	{
		string[] cmdArgs = Console.ReadLine()!.Split();
		Person person = new(cmdArgs[0], cmdArgs[1], int.Parse(cmdArgs[2]), decimal.Parse(cmdArgs[3]));
		persons.Add(person);
	}
	catch (Exception ex)
	{
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine(ex.Message);
		Console.WriteLine("");
	}
}

// persons.OrderBy(person => person.FirstName).ThenBy(person => person.Age).ToList().ForEach(person => Console.WriteLine(person.ToString()));

decimal bonus = 10;
persons.ForEach(p => p.IncreaseSalary(bonus));
persons.ForEach(p => Console.WriteLine(p.ToString()));