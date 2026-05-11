
using System.Reflection.Metadata;
using JsonPackageSaveFile.Person;
using Newtonsoft.Json;


(string label, string value, Func<string, (bool passed, string? errorMessage)>? valueCheck)[] options = [(label: "Brand:", value: "", valueCheck: null), (label: "Year:", value: "", valueCheck: null), (label: "Done", value: "", valueCheck: null)];
string[] errors = [];

bool userClosedIt = false;
int activeOne = 0;

bool inEditMode = false;

do
{
	Console.Clear();
	Console.CursorVisible = false;
	for (int i = 0; i < options.Length; i++)
	{
		if (i == activeOne)
		{
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.Write($"{options[i].label}");
			Console.ResetColor();
		}
		else
		{
			Console.Write($"{options[i].label}");
		}
		Console.Write(" ");
		Console.Write($"{options[i].value}\n");
	}

	if (inEditMode)
	{
		Console.WriteLine("\n\n Press enter to save!");
	}
	else
	{
		Console.WriteLine("\n\n Press enter to start editing!");
	}

	foreach (string error in errors)
	{
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine(error);
		Console.ResetColor();
	}


	ConsoleKeyInfo keyPressedInfo = Console.ReadKey(true);
	ConsoleKey keyPressed = keyPressedInfo.Key;

	if (keyPressed == ConsoleKey.Enter)
	{
		if (options[activeOne].label == "Done")
		{
			// check the fields
			if (!int.TryParse(options[1].value, out int age))
			{
				errors = [.. errors, "Please insert a valid year"];
			}
			else
			{
				userClosedIt = true;

			}

		}
		else
		{
			inEditMode = true;
			Console.SetCursorPosition(options[activeOne].label.Length + options[activeOne].value.Length + 1, activeOne);
			// VORKING
			// options[activeOne].value = "";
			// Console.Clear();
			// for (int i = 0; i < options.Length; i++)
			// {
			// 	if (i == activeOne)
			// 	{
			// 		Console.BackgroundColor = ConsoleColor.Gray;
			// 		Console.ForegroundColor = ConsoleColor.Black;
			// 		Console.Write($"> {options[i].label}");
			// 		Console.ResetColor();
			// 		Console.Write($" {options[i].value}\n");
			// 	}
			// 	else
			// 	{
			// 		Console.WriteLine($"{options[i].label} {options[i].value}");
			// 	}
			// }
		}
	}
	else if (keyPressed == ConsoleKey.Tab)
	{
		activeOne = (activeOne + 1) % options.Length;
	}

	// VORKING!
	// while (inEditMode)
	// {
	// 	Console.SetCursorPosition(options[activeOne].label.Length + options[activeOne].value.Length + 2, activeOne);
	// 	string value = Console.ReadLine() ?? string.Empty;

	// 	options[activeOne].value = value;
	// 	inEditMode = false;
	// }

	while (inEditMode)
	{
		Console.CursorVisible = true;
		ConsoleKeyInfo keyPressedInfoEdit = Console.ReadKey(true);
		ConsoleKey keyPressedEdit = keyPressedInfoEdit.Key;

		if (keyPressedEdit == ConsoleKey.Enter)
		{
			inEditMode = false;
		}
		else if (keyPressedEdit == ConsoleKey.Tab)
		{
			inEditMode = false;
			activeOne = (activeOne + 1) % options.Length;

		}
		else if (keyPressedEdit == ConsoleKey.Backspace && options[activeOne].value.Length > 0)
		{
			options[activeOne].value = options[activeOne].value[..^1];

			Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
			Console.Write(" ");
			Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
		}
		else if (char.IsLetterOrDigit(keyPressedInfoEdit.KeyChar) || keyPressedEdit == ConsoleKey.Spacebar || keyPressedEdit == ConsoleKey.OemComma || keyPressedEdit == ConsoleKey.OemPeriod || keyPressedEdit == ConsoleKey.Subtract)
		{
			Console.SetCursorPosition(options[activeOne].label.Length + options[activeOne].value.Length + 1, activeOne);
			options[activeOne].value += keyPressedInfoEdit.KeyChar;
			Console.Write(keyPressedInfoEdit.KeyChar);
		}
	}

	// else if (char.IsLetterOrDigit(keyPressedInfo.KeyChar))
	// {
	// 	// options[activeOne].value += keyPressedInfo.KeyChar;
	// 	Console.SetCursorPosition(options[activeOne].label.Length + options[activeOne].value.Length + 2, activeOne);
	// 	string value = Console.ReadLine() ?? string.Empty;

	// 	options[activeOne].value = value;
	// }

} while (!userClosedIt);

Console.BackgroundColor = ConsoleColor.Green;
Console.ForegroundColor = ConsoleColor.Black;

Console.WriteLine($"\t🏎️\tCongrats you built a {options[0].value} from {options[1].value}\t");


/*
Bozo myFried = new("Honda", "Civic") { Color = "red" };
Bozo mySecondFiend = new("Mazda", "CX-30");

Bozolan myFriedz = new("Honda", "Civic") { Color = "red" };
Bozolan mySecondFiendz = new("Mazda", "CX-30");

Console.WriteLine($"{myFried.Brand} - {myFried.Color}");
Console.WriteLine($"{mySecondFiend.Brand} - {mySecondFiend.Color ?? "unknown"}");

Console.WriteLine($"{myFriedz.Brand} - {myFriedz.Color}");
Console.WriteLine($"{mySecondFiendz.Brand} - {mySecondFiendz.Color ?? "unknown"}");

Biker biker = new("Boby B", 36, 2);
Snowboarder snowboarder = new("Boby J", 31, "Capita");
Bmxer bmxer = new("Bobby D", 12, true);

Person[] people = [biker, snowboarder, bmxer];

// foreach (var person in people)
// {
// 	Console.WriteLine("+---------------------+");
// 	person.LogSomething();
// 	person.LogDetails();
// 	Console.WriteLine($"Person is biker: {person is Biker} | Person is bmx-er: {person is Bmxer} | Person is snowboarder: {person is Snowboarder}");
// 	Console.WriteLine("+---------------------+\n");
// }

JsonSerializerSettings settings = new()
{
	TypeNameHandling = TypeNameHandling.All,
	Formatting = Formatting.Indented
};
string jsonified = JsonConvert.SerializeObject(people, settings);
// Console.WriteLine(jsonified);



void SaveToFile(string data, string fileName)
{
	string documentPath = "/Users/dragoscuciureanu/projects/LTU-VT2026/pluralsight/FileJson/JsonPackageSaveFile/data";
	// check if the directory exists, create it if not
	if (!Directory.Exists(documentPath))
	{
		Directory.CreateDirectory(documentPath);
		Console.WriteLine("✅ Directory created!");
	}
	// check if file exists, create it if not
	string filePath = Path.Combine(documentPath, $"{fileName}.txt");
	// if (!File.Exists(filePath))
	// {
	// 	File.Create(filePath);
	// 	Console.WriteLine("✅ File created!");
	// }
	// write to file
	File.WriteAllText(filePath, data);
}
// SaveToFile(jsonified, "test");

void ReadFromFile(string fileName)
{
	string documentPath = "/Users/dragoscuciureanu/projects/LTU-VT2026/pluralsight/FileJson/JsonPackageSaveFile/data";
	string filePath = Path.Combine(documentPath, $"{fileName}.txt");

	try
	{
		string fileData = File.ReadAllText(filePath);
		Person[] deJsonified = JsonConvert.DeserializeObject<Person[]>(fileData, settings)!;

		foreach (var person in deJsonified)
		{
			Console.WriteLine("+---------------------+");
			person.LogSomething();
			person.LogDetails();
			Console.WriteLine($"Person is biker: {person is Biker} | Person is bmx-er: {person is Bmxer} | Person is snowboarder: {person is Snowboarder}");
			Console.WriteLine("+---------------------+\n");
		}
	}
	catch (FileLoadException fle)
	{
		Console.WriteLine("🛑 File could not be loaded!");
		Console.WriteLine(fle.Message);
	}
	catch (FileNotFoundException fnfe)
	{
		Console.WriteLine("🛑 File could not be found!");
		Console.WriteLine(fnfe.Message);
	}
	catch (Exception exp)
	{
		Console.WriteLine("🛑 Something went wrong, reading from data file!");
		Console.WriteLine(exp.Message);
	}


}

ReadFromFile("test");

enum Bikes
{
	Bmx,
	Road
}
*/