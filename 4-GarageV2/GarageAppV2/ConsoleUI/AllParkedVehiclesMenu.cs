using System;
using GarageAppV2.Shared;
using GarageAppV2.Vehicles;

namespace GarageAppV2.ConsoleUI;

public class AllParkedVehiclesMenu : Menu
{
	private void PressToGoBackToGarageMenu()
	{
		Console.WriteLine("Press anything to go back to garage menu.");
		Console.ReadKey(true);

		NavigateToGarageMenu();
	}

	public override void MenuHandler()
	{
		Console.Clear();
		ConsoleUtils.DisplayMenuHeader(["🅿️  Garage", "(all currently parked vehicles)"], '-');

		// get the list of parked vehicles
		IEnumerable<Vehicle> listOfParkedVehicles = AppStore.Garage.GetListOfParkedVehicles();

		if (listOfParkedVehicles.Count() == 0)
		{
			ConsoleUtils.LogWarning("There are no vehicles parked at the moment, bring some in.");
		}
		else
		{
			Console.WriteLine("\nList of all parked vehicles:\n");
			AppStore.Vehicles.RenderTableOfVehicles(listOfParkedVehicles);
		}
		Console.WriteLine("\n");
		PressToGoBackToGarageMenu();

	}
}
