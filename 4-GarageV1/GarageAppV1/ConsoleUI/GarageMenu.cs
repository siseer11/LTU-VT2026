using System;
using GarageAppV1.Vehicles;

namespace GarageAppV1.ConsoleUI;

public class GarageMenu : Menu
{
	private (string label, Action value)[] MenuItems { get; set; }

	public GarageMenu()
	{
		MenuItems = [
			(label: "1. Park car  ", value: () => Console.WriteLine("1")),
			(label: "2. Take car out ", value: () => Console.WriteLine("2")),
			(label: "3. List all parked cars ", value: () => Console.WriteLine("3")),
			(label: "4. Search parked car by registration number ", value: () => Console.WriteLine("4")),
			(label: "5. Destroy garage ", value: () => Console.WriteLine("5")),
			(label: "Main menu ", value: NavigateToMainMenu),
			(label: "Close app ", value: CloseApp)
		];
	}

	public override void MenuHandler()
	{
		Console.Clear();
		ConsoleUtils.DisplayMenuHeader(["🅿️  Garage", "(manage the vehicles in the garage)"], '-');

		if (App.NewGarage is null)
		{
			ConsoleUtils.LogError("No garage found, you did not build one yet!");
			int garageCapacity = ConsoleUtils.GetValidInt(label: "\n🏗️  Let's build one! What's the capacity you need (1-50): ", minValue: 1, maxValue: 50);

			Console.WriteLine("\n🧱 Building...");
			Thread.Sleep(2000);
			Console.WriteLine("");
			ConsoleUtils.LogSuccess("Your garage is built! You can start park your cars now, enjoy!");

			App.CreateNewGarage(garageCapacity);
			Console.WriteLine("\nPress any key to start using your garage!");
			Console.ReadKey(true);
		}
		else
		{
			Console.WriteLine("\nSelect one (use tab/arrows):");
			ConsoleUtils.KeyboardControllMenu("", MenuItems.ToList(), ConsoleUtils.Directions.column)();

		}
	}
}
