using System;
using GarageAppV2.Shared;
using GarageAppV2.Vehicles;

namespace GarageAppV2.ConsoleUI;

public class ParkVehicleMenu : Menu
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
		ConsoleUtils.DisplayMenuHeader(["🅿️  Garage", "(park a vehicle)"], '-');

		// Check to see if there are any spots available
		int numberOfAvailableSpots = AppStore.Garage.GetNumberOfEmptySpots();

		if (numberOfAvailableSpots == 0)
		{
			Console.WriteLine("\n");
			ConsoleUtils.LogError("There are no empty spots available! Take some vehicles out first.\n");
			PressToGoBackToGarageMenu();
		}
		else
		{
			ConsoleUtils.LogColor($"\nThere are {numberOfAvailableSpots} available spot(s). Welcome in.", ConsoleColor.Green);


			Vehicle[] vehiclesThatAreNotParked = AppStore.Vehicles.GetListOfVehiclesWithFilter(filter: (vehicle) => !AppStore.Garage.CheckIfVehicleIsParkedByRegistrationNr(vehicle.RegistrationNr)).ToArray();

			if (vehiclesThatAreNotParked.Length == 0)
			{
				ConsoleUtils.LogWarning("No vehicles that you can park, either all are parked, or you have to register some! ⚠️\n");
				PressToGoBackToGarageMenu();
			}
			else
			{
				Console.WriteLine("\nList of vehicles you can park:\n");
				AppStore.Vehicles.RenderTableOfVehicles(vehiclesThatAreNotParked);

				#region Get valid reg nr from input
				Console.WriteLine("\n\nType the registration number of the vehicle you want to park (or 'Exit' to go back): ");
				string? registrationNumber = Console.ReadLine();


				while (
					!(!string.IsNullOrEmpty(registrationNumber) && registrationNumber.ToUpper() == "EXIT") && (
						string.IsNullOrEmpty(registrationNumber) ||
						registrationNumber.Length != 6 ||
						AppStore.Vehicles.GetVehicleByRegistrationNumber(registrationNumber) is null ||
						AppStore.Garage.CheckIfVehicleIsParkedByRegistrationNr(registrationNumber)
					)
				)
				{
					ConsoleUtils.LogError("Invalid registration number!");
					ConsoleUtils.LogColor("(The registration number must have 6 characters, the vehicle must exist and not be parked) Try again:", ConsoleColor.Red);
					registrationNumber = Console.ReadLine();
				}
				#endregion

				if (registrationNumber.ToUpper() == "EXIT")
				{
					NavigateToGarageMenu();
				}
				else
				{
					// park the vehicle
					Vehicle vehicleToPark = AppStore.Vehicles.GetVehicleByRegistrationNumber(registrationNumber)!;
					bool parkedSuccessfully = AppStore.Garage.ParkVehicle(vehicleToPark);

					if (parkedSuccessfully)
					{
						ConsoleUtils.LogSuccess($"{vehicleToPark.Icon} Vehicle with registration [{vehicleToPark.RegistrationNr}] was parked successfully!\n");
						if (numberOfAvailableSpots == 1)
							ConsoleUtils.LogWarning($"There are no more available spots in the garage now!");
					}
					else
					{
						ConsoleUtils.LogError($"Something went wrong parking the vehicle with registration [{vehicleToPark.RegistrationNr}]!");
						Console.WriteLine();
					}
					PressToGoBackToGarageMenu();
				}
			}
		}
	}
}
