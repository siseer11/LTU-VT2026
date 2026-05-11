using System;
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
			HandleGoBack: () =>
			{
				VehiclesMenu vehiclesMenu = new();
				NavigateToNewMenu(vehiclesMenu);
			}
		));

		// Step 3 | Create the vehicle with the data
		Console.WriteLine("A CAR WAS CREATED!");

		foreach (var formValue in formValues)
		{
			Console.WriteLine($"{formValue.label} : {formValue.value}");
		}
		Console.ReadLine();

		// GoBack();
	}
}
