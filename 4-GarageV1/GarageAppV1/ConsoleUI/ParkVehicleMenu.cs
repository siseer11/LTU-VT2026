using System;
using GarageAppV1.Vehicles;

namespace GarageAppV1.ConsoleUI;

public class ParkVehicleMenu : Menu
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
		ConsoleUtils.DisplayMenuHeader(["🅿️  Garage", "(park a car)"], '-');

		// Check to see if there are any spots available
		int numberOfAvailableSpots = App.NewGarage!.GetNumberOfEmptySpots();

		if (numberOfAvailableSpots == 0)
		{
			ConsoleUtils.LogError("\n\nThere are no empty spots available! Take some cars out first.\n");
			PressToGoBackToGarageMenu();
		}
		else
		{
			ConsoleUtils.LogColor($"\nThere are {numberOfAvailableSpots} available spot(s). Welcome in.", ConsoleColor.Green);


			Vehicle[] vehiclesThatAreNotParked = VehiclesList.GetListOfVehiclesWithFilter(filter: (vehicle) => !App.NewGarage!.CheckIfCarIsParkedByRegistrationNr(vehicle.RegistrationNr));

			if (vehiclesThatAreNotParked.Length == 0)
			{
				ConsoleUtils.LogColor("\n\n⚠️ No cars that you can park, either all are parked, or you have to register some! ⚠️\n", ConsoleColor.Yellow);
				PressToGoBackToGarageMenu();
			}
			else
			{
				Console.WriteLine("\nList of cars you can park:\n");
				VehiclesList.RenderTableOfVehicles(vehiclesThatAreNotParked);

				#region Get valid reg nr from input
				Console.WriteLine("\n\nType the registration number of the car you want to park (or 'Exit' to go back): ");
				string? registrationNumber = Console.ReadLine();


				while (
					!(!string.IsNullOrEmpty(registrationNumber) && registrationNumber.ToUpper() == "EXIT") && (
						string.IsNullOrEmpty(registrationNumber) ||
						registrationNumber.Length != 6 ||
						VehiclesList.GetVehicleByRegistrationNumber(registrationNumber) is null ||
						App.NewGarage!.CheckIfCarIsParkedByRegistrationNr(registrationNumber)
					)
				)
				{
					ConsoleUtils.LogError("Invalid registration number!");
					ConsoleUtils.LogColor("(The registration number must have 6 characters, the car must exist and not be parked) Try again:", ConsoleColor.Red);
					registrationNumber = Console.ReadLine();
				}
				#endregion

				if (registrationNumber.ToUpper() == "EXIT")
				{
					GoBackToGarageMenu();
				}

				// park the car
				Vehicle vehicleToPark = VehiclesList.GetVehicleByRegistrationNumber(registrationNumber)!;
				(bool parkedSuccessfully, string? parkErrorMessage) = App.NewGarage!.ParkVehicle(vehicleToPark);

				if (parkedSuccessfully)
				{
					ConsoleUtils.LogSuccess($"{vehicleToPark.Icon} Vehicle with registration [{vehicleToPark.RegistrationNr}] was parked successfully!\n");
				}
				else
				{
					ConsoleUtils.LogError($"Something went wrong parking the vehicle with registration [{vehicleToPark.RegistrationNr}]!");
					if (parkErrorMessage is not null)
						ConsoleUtils.LogColor(parkErrorMessage, ConsoleColor.Red);
					Console.WriteLine();
				}
				PressToGoBackToGarageMenu();
			}
		}



	}
}
