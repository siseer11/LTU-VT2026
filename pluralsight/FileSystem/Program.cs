
using System.Text;
using FileSystem.Management;

string directoryPath = @"/Users/dragoscuciureanu/projects/LTU-VT2026/pluralsight/FileSystem/FilesFolder/";
string fileName = "test.txt";

void CheckAndCreateFile()
{
	string fullPath = $"{directoryPath}{fileName}";

	bool fileExists = File.Exists(fullPath);
	bool directoryExists = Directory.Exists(directoryPath);

	if (fileExists)
	{
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine("File exists!");
		Console.ResetColor();
	}
	else
	{
		if (!directoryExists)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Directory does not exists, let's create it.");
			Console.ResetColor();

			Directory.CreateDirectory(directoryPath);
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Directory created.");
			Console.ResetColor();
		}

		File.Create(fullPath);
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine("File created.");
		Console.ResetColor();
	}
}

// CheckAndCreateFile();


// Player Bobby = new("Bobby", 32, 999);
// Player Zolo = new Atacker("Zolo", 21, 10);
// Player Stonko = new Defender("Stonko", 120, 41);
// Player Ciobi = new Goalkeeper("Ciobi", 55, 99);
// Player Jionz = new Midfielder("Jionz", 29, 11);

// List<Player> players = [Bobby, Zolo, Stonko, Ciobi, Jionz];

// players.Add(new Goalkeeper("Gionco", 13, 9));

void LogWithColor(string txt, ConsoleColor color)
{
	Console.ForegroundColor = color;
	Console.WriteLine(txt);
	Console.ResetColor();
}



List<Player> players = [];
string baseDirectoryPath = @"/Users/dragoscuciureanu/projects/LTU-VT2026/pluralsight/FileSystem/";
string playerDataDirectoryPath = "playersLocalData/";
string playerDataFileName = "data.txt";
string filePath = $"{baseDirectoryPath}{playerDataDirectoryPath}{playerDataFileName}";

void GenerateDirectoryAndFileIfNotExisting()
{
	// check if directory exists
	if (!Directory.Exists($"{baseDirectoryPath}{playerDataDirectoryPath}"))
	{
		LogWithColor("Directory not existing, lets create it!", ConsoleColor.Red);

		Directory.CreateDirectory($"{baseDirectoryPath}{playerDataDirectoryPath}");
		LogWithColor("Directory created!", ConsoleColor.Green);
	}


	// create empty file
	if (!File.Exists(filePath))
	{
		LogWithColor("File not existing, lets create it!", ConsoleColor.Red);

		File.Create(filePath);
		LogWithColor("Empty file created!", ConsoleColor.Green);
	}
}

void SavePlayersToFile(List<Player> playes)
{
	// populate file with data
	StringBuilder sb = new();

	foreach (Player player in players)
	{
		sb.Append($"firstName:{player.Name};");
		sb.Append($"age:{player.Age};");
		sb.Append($"jerseyNumber:{player.JerseyNumber};");
		sb.Append($"position:{Player.GetPlayerType(player)}");
		sb.Append(Environment.NewLine);
	}

	File.WriteAllText(filePath, sb.ToString());
	LogWithColor("Players saved successfully to the local file!", ConsoleColor.DarkGreen);
}


void ReadPlayersFromFile()
{
	players.Clear();

	string[] fileContentLines = File.ReadAllLines(filePath);

	foreach (string line in fileContentLines)
	{
		string[] valuesSplited = line.Split(";");
		string name = valuesSplited[0].Split(":")[1];
		int age = int.Parse(valuesSplited[1].Split(":")[1]);
		int jerseyNumber = int.Parse(valuesSplited[2].Split(":")[1]);
		string position = valuesSplited[3].Split(":")[1];

		switch (position)
		{
			case "1":
				players.Add(new Goalkeeper(name, age, jerseyNumber));
				break;
			case "2":
				players.Add(new Defender(name, age, jerseyNumber));
				break;
			case "3":
				players.Add(new Midfielder(name, age, jerseyNumber));
				break;
			case "4":
				players.Add(new Atacker(name, age, jerseyNumber));
				break;
			default:
				players.Add(new Player(name, age, jerseyNumber));
				break;
		}
	}
}

ReadPlayersFromFile();
LogWithColor("+++ This is our team +++\n", ConsoleColor.Cyan);
foreach (var player in players)
{
	Console.WriteLine("\n============");
	player.PrintDetails();
	player.PrintPlayerType();
}
// GenerateDirectoryAndFileIfNotExisting();
// SavePlayersToFile(players);