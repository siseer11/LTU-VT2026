using System;
using GarageAppV2.Contracts;
using GarageAppV2.Garage;
using GarageAppV2.Vehicles;

namespace GarageAppV2.Shared;

public class AppStore
{
	public static IVehiclesHandler Vehicles { get; } = new VehiclesHandler();
	public static IGarageHandler Garage { get; } = new GarageHandler();
	public static bool AppRunning { get; private set; } = true;
	public static bool CacheEnabled { get; private set; } = true;
	public static void CloseApp()
	{
		AppRunning = false;
	}

	public static void DisableCache()
	{
		CacheEnabled = false;
	}
}
