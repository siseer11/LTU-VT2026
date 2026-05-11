using System;
using GarageAppV1.Vehicles;

namespace GarageAppV1.ConsoleUI;

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
		Vehicle[] listOfPakedVehicles = App.NewGarage!.GetListOfParkedCars();
		int numberOfVehiclesInGarage = listOfPakedVehicles.Length;

		if (numberOfVehiclesInGarage == 0)
		{
			Console.WriteLine("\n");
			ConsoleUtils.LogError("There are no parked cars! Bring some in!.\n");
			PressToGoBackToGarageMenu();
		}
		else
		{
			ConsoleUtils.LogColor($"\nThere are {numberOfVehiclesInGarage} vehicle(s). Take one out!", ConsoleColor.Green);

			Console.WriteLine("\nList of currently parked cars:\n");
			VehiclesList.RenderTableOfVehicles(listOfPakedVehicles);

			#region Get valid reg nr from input
			Console.WriteLine("\n\nType the registration number of the car you want to take out (or 'Exit' to go back): ");
			string? registrationNumber = Console.ReadLine();

			while (
				!(!string.IsNullOrEmpty(registrationNumber) && registrationNumber.ToUpper() == "EXIT") && (
					string.IsNullOrEmpty(registrationNumber) ||
					registrationNumber.Length != 6 ||
					VehiclesList.GetVehicleByRegistrationNumber(registrationNumber) is null ||
					!listOfPakedVehicles.Any(v => v.RegistrationNr == registrationNumber.ToUpper())
				)
			)
			{
				ConsoleUtils.LogError("Invalid registration number!");
				ConsoleUtils.LogColor("(The registration number must have 6 characters, the car must exist and be parked) Try again:", ConsoleColor.Red);
				registrationNumber = Console.ReadLine();
			}
			#endregion

			if (registrationNumber.ToUpper() == "EXIT")
			{
				NavigateToGarageMenu();
			}

			(bool takenOutSuccessfully, string? parkErrorMessage) = App.NewGarage!.TakeVehicleOut(registrationNumber);
			if (takenOutSuccessfully)
			{
				ConsoleUtils.LogSuccess($"Enjoy the ride!\n");
			}
			else
			{
				ConsoleUtils.LogError($"Something went wrong taking out the vehicle!");
				if (parkErrorMessage is not null)
					ConsoleUtils.LogColor(parkErrorMessage, ConsoleColor.Red);
				Console.WriteLine();
			}
			PressToGoBackToGarageMenu();
		}
	}
}
