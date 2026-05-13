using GarageAppV1.ConsoleUI;
using GarageAppV1.FileData;
using GarageAppV1.Garage;
using GarageAppV1.Vehicles;

namespace GarageAppV1;

public class App
{
	public static bool AppRunning { get; private set; } = true;
	public static bool CacheEnabled { get; private set; } = true;

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

	public static void DeleteGarage()
	{
		NewGarage = null;
	}

	private static void InitAppWithCacheData()
	{
		// Try to populate the vehicles
		(bool vehiclesCacheSyncSuccess, string? vehiclesCacheSyncErrorMsg) = VehiclesList.PopulateWithDataFromFile();

		// Try to generate the garage
		(bool garageCacheSyncSuccess, string? garageCacheSyncErrorMsg, var garageFileData) = FileUtils.ReadFromGarageFile();

		if (garageCacheSyncSuccess && garageFileData is not null)
		{
			NewGarage = new GarageClass(garageFileData.Length, garageFileData);
			// ConsoleUtils.LogSuccess("Garage data populated successfully!");
		}

		if (!vehiclesCacheSyncSuccess || !garageCacheSyncSuccess)
		{
			CacheEnabled = false;
		}

	}

	public void Run()
	{
		// populate with data from files
		Console.Clear();
		InitAppWithCacheData();

		MainMenu mainMenu = new();
		mainMenu.Run();
	}
}
