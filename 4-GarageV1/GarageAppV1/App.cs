using GarageAppV1.ConsoleUI;
using GarageAppV1.FileData;
using GarageAppV1.Garage;
using GarageAppV1.Vehicles;

namespace GarageAppV1;

public class App
{
	public static bool AppRunning { get; private set; } = true;

	private static GarageClass? _newGarage;

	public static GarageClass? NewGarage { get => _newGarage; private set => _newGarage = value; }

	public static void CloseApp()
	{
		AppRunning = false;
	}

	// we need new instance of garage, since in the future want to have option for multiple garages
	public static void CreateNewGarage(int capacity)
	{
		NewGarage = new GarageClass(capacity, null);
	}

	private static void PopulateGarageWithDataFromFile()
	{
		(bool successfully, string? errorMsg, var garageFileData) = FileUtils.ReadFromGarageFile();

		if (successfully && garageFileData is not null)
		{
			NewGarage = new GarageClass(garageFileData.Length, garageFileData);
			ConsoleUtils.LogSuccess("Garage data populated successfully!");
		}
		else if (!successfully)
		{
			ConsoleUtils.LogError("Could not create the garage with the data from local file!");
			if (!string.IsNullOrEmpty(errorMsg))
				ConsoleUtils.LogColor($"({errorMsg})", ConsoleColor.Red);

			Console.ReadKey(true);
			CloseApp();
		}
	}

	public void Run()
	{
		// populate with data from files
		Console.Clear();
		Console.WriteLine("Cache data loaded status:");
		VehiclesList.PopulateWithDataFromFile();
		PopulateGarageWithDataFromFile();

		Console.WriteLine("\n\nPress anyhting to start!");
		Console.ReadKey(true);

		MainMenu mainMenu = new();
		mainMenu.Run();
	}
}
