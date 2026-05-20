using GarageAppV2.ConsoleUI;
using GarageAppV2.Garage;
using GarageAppV2.Shared;
using GarageAppV2.Vehicles;

namespace GarageAppV2;

public class App
{
	private static void InitAppWithCacheData()
	{
		// Try to populate the vehicles
		bool vehiclesCacheSyncSuccess = AppStore.Vehicles.PopulateFromCache();

		// Try to generate the garage, with cache data
		bool garageCacheSyncSuccess = AppStore.Garage.SetupGarageFromCache();


		if (!vehiclesCacheSyncSuccess || !garageCacheSyncSuccess)
		{
			AppStore.DisableCache();
		}

	}

	public void Run()
	{
		// populate with data from files
		InitAppWithCacheData();
		Console.Clear();

		MainMenu mainMenu = new();
		mainMenu.Run();
	}
}
