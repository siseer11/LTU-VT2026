using System;
using System.Drawing;
using GarageAppV1.Shared;
using GarageAppV1.Vehicles;

namespace GarageAppV1.ConsoleUI;

public class AddNewCarMenu : Menu
{
	public override void MenuHandler()
	{
		Console.Clear();
		ConsoleUtils.DisplayMenuHeader(["🏎️  Vehicles", "(register new vehicle)"], '-');

		// Step 1
		VehicleTypes[] vehicleTypes = Enum.GetValues<VehicleTypes>();

		(string label, VehicleTypes value)[] vehicleTypesMenuOptions = new (string label, VehicleTypes value)[vehicleTypes.Length];

		for (int i = 0; i < vehicleTypes.Length; i++)
		{
			VehicleTypes typeAtIndex = vehicleTypes[i];
			vehicleTypesMenuOptions[i] = (label: $"{VehicleUtils.GetIconByVehicleType(typeAtIndex)} {typeAtIndex}", value: typeAtIndex);
		}
		;

		VehicleTypes newVehicleType = ConsoleUtils.KeyboardControllMenu("\nSelect one of the supported types: \n", vehicleTypesMenuOptions.ToList(), ConsoleUtils.Directions.column);

		// Step 2 | Form
		Console.Clear();
		var formValues = ConsoleUtils.KeyboardControlForm(new KeyboardControlFormParams(
			Options: Vehicle.GetFormOptionsByVehicleType(newVehicleType),
			DisplayHeader: () => ConsoleUtils.DisplayMenuHeader([$"➕ Registration Form ({VehicleUtils.GetIconByVehicleType(newVehicleType)})"], '-'),
			HeaderNumberOfLines: 5,
			GoBackButtonLabel: "Go back (vehicle menu)",
			HandleGoBack: NavigateToVehiclesMenu
		));

		// Step 3 | Create the vehicle with the data
		if (App.AppRunning)
		{
			Console.Clear();
			ConsoleUtils.DisplayMenuHeader([$"✨ Registration Finished ({VehicleUtils.GetIconByVehicleType(newVehicleType)})"], '-');

			// TODO: This is not the best, but will do for now since we use arrays
			string brand = formValues[0].value;
			string model = formValues[1].value;
			string registrationNr = formValues[2].value;
			string yearString = formValues[3].value;
			string color = formValues[4].value;
			string nrOfEnginesString = formValues[5].value;
			string fuelTypeString = formValues[6].value;
			string numberOfSeatsString = formValues[7].value;
			string lengthString = formValues[8].value;

			// parse the values that are not accepted as strings
			FuelTypeEnum? fuelType = Enum.TryParse(fuelTypeString, out FuelTypeEnum parsedFuelType) ? parsedFuelType : null;
			int? year = int.TryParse(yearString, out int parsedYear) ? parsedYear : null;
			int? nrOfEngines = int.TryParse(nrOfEnginesString, out int parsedNrOfEngines) ? parsedNrOfEngines : null;
			int? nrOfSeats = int.TryParse(numberOfSeatsString, out int parsedNrOfSeats) ? parsedNrOfSeats : null;
			double? length = double.TryParse(lengthString, out double parsedLength) ? parsedLength : null;

			// Generate the new vehicle
			Vehicle? newVehicle = newVehicleType switch
			{
				VehicleTypes.Car => new Car(brand, model, registrationNr) { Color = color, FuelType = fuelType, Length = length, ManufacturingYear = year, NumberOfEngines = nrOfEngines, NumberOfSeats = nrOfSeats },
				VehicleTypes.Airplane => new Airplane(brand, model, registrationNr) { Color = color, FuelType = fuelType, Length = length, ManufacturingYear = year, NumberOfEngines = nrOfEngines, NumberOfSeats = nrOfSeats },
				VehicleTypes.Boat => new Boat(brand, model, registrationNr) { Color = color, FuelType = fuelType, Length = length, ManufacturingYear = year, NumberOfEngines = nrOfEngines, NumberOfSeats = nrOfSeats },
				VehicleTypes.Bus => new Buss(brand, model, registrationNr) { Color = color, FuelType = fuelType, Length = length, ManufacturingYear = year, NumberOfEngines = nrOfEngines, NumberOfSeats = nrOfSeats },
				VehicleTypes.Motorcycle => new Motorcycle(brand, model, registrationNr) { Color = color, FuelType = fuelType, Length = length, ManufacturingYear = year, NumberOfEngines = nrOfEngines, NumberOfSeats = nrOfSeats },
				_ => null
			};

			if (newVehicle is not null)
			{
				(bool savedSuccessfully, string? errorUserMsg, string? errorDetails) = VehiclesList.AddVehicle(newVehicle);
				if (savedSuccessfully)
				{
					ConsoleUtils.LogSuccess("The vehicle was succesfully added to your colection! Enjoy!");
					Console.WriteLine("\nNew vehicle details:");
					Console.WriteLine(newVehicle.GetBasicDetailsString());
				}
				else
				{
					ConsoleUtils.LogError(errorUserMsg);
					if (!string.IsNullOrEmpty(errorDetails))
						ConsoleUtils.LogColor($"({errorDetails})", ConsoleColor.Red);
				}
			}
			else
			{
				ConsoleUtils.LogError("Unexpected error, the vehicle could not be created!");
			}


			ConsoleUtils.KeyboardControllMenu("\n\nNavigate to:", [
				(label: " Vehicles ", value: NavigateToVehiclesMenu),
				(label: " Main Menu ", value: NavigateToMainMenu),
				(label: " Close App ", value: CloseApp)
			], ConsoleUtils.Directions.column)();
		}
		// GoBack();
	}
}
