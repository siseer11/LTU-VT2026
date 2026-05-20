using System;
using GarageAppV2.Shared;
using GarageAppV2.Vehicles;

namespace GarageAppV2.ConsoleUI;

public class GarageMenu : Menu
{
	private (string label, Action value)[] MenuItems { get; set; }

	public GarageMenu()
	{
		MenuItems = [
			(label: "1. Park vehicle  ", value: ParkVehicleHandler),
			(label: "2. Take vehicle out ", value: TakeVehicleOutHandler),
			(label: "3. List all parked vehicles ", value: ListAllParkedVehiclesHandler),
			// (label: "4. Search parked car by registration number ", value: () => Console.WriteLine("4")),
			(label: "4. Destroy garage ", value: DestroyGarageHandler),
			(label: "Main menu ", value: NavigateToMainMenu),
			(label: "Close app ", value: CloseApp)
		];
	}

	public override void MenuHandler()
	{
		Console.Clear();
		ConsoleUtils.DisplayMenuHeader(["🅿️  Garage", "(manage the vehicles in the garage)"], '-');

		// There is no garage defined, build one
		if (!AppStore.Garage.IsGarageBuilt)
		{
			ConsoleUtils.LogError("No garage found, you did not build one yet!");
			int garageCapacity = ConsoleUtils.GetValidInt(label: "\n🏗️  Let's build one! What's the capacity you need (1-50): ", minValue: 1, maxValue: 50);

			Console.WriteLine("\n🧱 Building...");
			Thread.Sleep(1000);
			Console.WriteLine("");
			ConsoleUtils.LogSuccess("Your garage is built! You can start park your vehicles now, enjoy!");

			AppStore.Garage.CreateGarage(garageCapacity);
			Console.WriteLine("\nPress any key to start using your garage!");
			Console.ReadKey(true);
		}
		else
		{
			Console.WriteLine("\nSelect one (use tab/arrows):");
			ConsoleUtils.KeyboardControllMenu("", MenuItems.ToList(), ConsoleUtils.Directions.column)();
		}
	}

	private void ParkVehicleHandler()
	{
		ParkVehicleMenu parkVehicleMenu = new();
		NavigateToNewMenu(parkVehicleMenu);
	}

	private void ListAllParkedVehiclesHandler()
	{
		AllParkedVehiclesMenu allParkedVehiclesMenu = new();
		NavigateToNewMenu(allParkedVehiclesMenu);
	}

	private void TakeVehicleOutHandler()
	{
		TakeVehicleOutMenu takeVehicleOutMenu = new();
		NavigateToNewMenu(takeVehicleOutMenu);
	}

	private void DestroyGarageHandler()
	{
		DestroyGarageMenu destroyGarageMenu = new();
		NavigateToNewMenu(destroyGarageMenu);
	}
}
