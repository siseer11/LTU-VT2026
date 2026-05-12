using System;
using GarageAppV1.Vehicles;

namespace GarageAppV1.ConsoleUI;

public class VehicleFullDetailsMenu : Menu
{
	private void PressToGoBackToVehicles()
	{
		Console.WriteLine("\n\nPress anything to go back to garage menu.");
		Console.ReadKey(true);

		NavigateToVehiclesMenu();
	}

	private void CleanAndRenderHeader()
	{
		Console.Clear();
		ConsoleUtils.DisplayMenuHeader(["🏎️  Vehicles", "(full details for vehicle)"], '-');
	}

	public override void MenuHandler()
	{

		CleanAndRenderHeader();

		if (VehiclesList.GetNumberOfRegisteredVehicles() == 0)
		{
			ConsoleUtils.LogWarning("There are no registered vehicles, register some first! ⚠️\n");
			PressToGoBackToVehicles();
		}
		else
		{
			#region Get valid reg nr from input
			Console.WriteLine("\n\nType the registration number of the vehicle you want the details for (or 'Exit' to go back): ");
			string? registrationNumber = Console.ReadLine();

			while (
				!(!string.IsNullOrEmpty(registrationNumber) && registrationNumber.ToUpper() == "EXIT") && (
					string.IsNullOrEmpty(registrationNumber) ||
					registrationNumber.Length != 6 ||
					VehiclesList.GetVehicleByRegistrationNumber(registrationNumber) is null ||
					App.NewGarage!.CheckIfVehicleIsParkedByRegistrationNr(registrationNumber)
				)
			)
			{
				ConsoleUtils.LogError("Invalid registration number!");
				ConsoleUtils.LogColor("(Invalid registration number, or car not found for it) Try again:", ConsoleColor.Red);
				registrationNumber = Console.ReadLine();
			}
			#endregion

			if (registrationNumber.ToUpper() == "EXIT")
			{
				PressToGoBackToVehicles();
			}
			else
			{
				CleanAndRenderHeader();
				Vehicle vehicleWithThisRegistrationNr = VehiclesList.GetVehicleByRegistrationNumber(registrationNumber)!;
				Console.WriteLine("\n\nDetails about the vehicle:\n");
				Console.WriteLine(vehicleWithThisRegistrationNr.GetBasicDetailsString());
				ConsoleUtils.LogColor(vehicleWithThisRegistrationNr.GetVehicleDetailsString(), ConsoleColor.DarkGray);

				PressToGoBackToVehicles();
			}
		}
	}
}
