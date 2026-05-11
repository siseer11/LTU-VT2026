using System;
using GarageAppV1.Vehicles;

namespace GarageAppV1.ConsoleUI;

public class AllParkedCarsMenu : Menu
{
	private void GoBackToGarageMenu()
	{
		GarageMenu garageMenu = new();
		NavigateToNewMenu(garageMenu);
	}
	private void PressToGoBackToGarageMenu()
	{
		Console.WriteLine("Press anything to go back to garage menu.");
		Console.ReadKey(true);

		GoBackToGarageMenu();
	}

	public override void MenuHandler()
	{
		Console.Clear();
		ConsoleUtils.DisplayMenuHeader(["🅿️  Garage", "(all currently parked vehicles)"], '-');

		// get the list of parked cars
		Vehicle[] listOfParkedVehicles = App.NewGarage!.GetListOfParkedCars();

		if (listOfParkedVehicles.Length == 0)
		{
			ConsoleUtils.LogColor("⚠️ There are no cars parked at the moment, bring some in.", ConsoleColor.Yellow);
		}
		else
		{
			Console.WriteLine("\nList of all parked vehicles:\n");
			VehiclesList.RenderTableOfVehicles(listOfParkedVehicles);
		}
		Console.WriteLine("\n");
		PressToGoBackToGarageMenu();

	}
}
