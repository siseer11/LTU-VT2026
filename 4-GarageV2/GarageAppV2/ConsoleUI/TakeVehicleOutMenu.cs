using System;
using GarageAppV2.Shared;
using GarageAppV2.Vehicles;

namespace GarageAppV2.ConsoleUI;

public class TakeVehicleOutMenu : Menu
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
		ConsoleUtils.DisplayMenuHeader(["🅿️  Garage", "(take a vehicle out)"], '-');
		// Check to see if there are any spots available
		IEnumerable<Vehicle> listOfPakedVehicles = AppStore.Garage.GetListOfParkedVehicles();

		int numberOfVehiclesInGarage = AppStore.Garage.GetNumberOfParkedVehicles();

		if (numberOfVehiclesInGarage == 0)
		{
			Console.WriteLine("\n");
			ConsoleUtils.LogError("There are no parked vehicles! Bring some in!.\n");
			PressToGoBackToGarageMenu();
		}
		else
		{
			ConsoleUtils.LogColor($"\nThere are {numberOfVehiclesInGarage} vehicle(s). Take one out!", ConsoleColor.Green);

			Console.WriteLine("\nList of currently parked vehicles:\n");
			AppStore.Vehicles.RenderTableOfVehicles(listOfPakedVehicles);

			#region Get valid reg nr from input
			Console.WriteLine("\n\nType the registration number of the vehicle you want to take out (or 'Exit' to go back): ");
			string? registrationNumber = Console.ReadLine();

			while (
				!(!string.IsNullOrEmpty(registrationNumber) && registrationNumber.ToUpper() == "EXIT") && (
					string.IsNullOrEmpty(registrationNumber) ||
					registrationNumber.Length != 6 ||
					AppStore.Vehicles.GetVehicleByRegistrationNumber(registrationNumber) is null ||
					!listOfPakedVehicles.Any(v => v.RegistrationNr == registrationNumber.ToUpper())
				)
			)
			{
				ConsoleUtils.LogError("Invalid registration number!");
				ConsoleUtils.LogColor("(The registration number must have 6 characters, the vehicle must exist and be parked) Try again:", ConsoleColor.Red);
				registrationNumber = Console.ReadLine();
			}
			#endregion

			if (registrationNumber.ToUpper() == "EXIT")
			{
				NavigateToGarageMenu();
			}
			else
			{
				bool takenOutSuccessfully = AppStore.Garage.TakeVehicleOut(registrationNumber);
				if (takenOutSuccessfully)
				{
					ConsoleUtils.LogSuccess($"Enjoy the ride!\n");
				}
				else
				{
					ConsoleUtils.LogError($"Something went wrong taking out the vehicle!");
					Console.WriteLine();
				}
				PressToGoBackToGarageMenu();
			}

		}
	}
}
