using System;
using System.Text;
using GarageAppV1.FileData;

namespace GarageAppV1.Vehicles;

public class VehiclesList
{
	public static Vehicle[] Vehicles { get; private set; } = [];

	public static bool RegistrationNrAlreadyUsed(string registrationNr)
	{
		return Vehicles.Any((Vehicle v) => v.RegistrationNr == registrationNr);
	}

	public static string ReturnTrimmedValue(string txt, int maxLenght)
	{
		return txt.Length > maxLenght ? $"{txt[..(maxLenght - 2)]}.." : txt;
	}

	public static void PopulateWithDataFromFile()
	{
		var vehiclesListFromFile = FileUtils.ReadFromVehiclesFile();

		if (vehiclesListFromFile is not null)
			Vehicles = vehiclesListFromFile;
	}

	public static void ListTableWithAllVehicles()
	{
		if (Vehicles.Length == 0)
		{
			Console.WriteLine("There are no vehicles registered 😢");
		}
		else
		{
			Console.WriteLine("Type\tRegistration\tBrand\tModel\tColor\tYear\n");
			for (int i = 0; i < Vehicles.Length; i++)
			{
				Vehicle vehicle = Vehicles[i];
				// if (i % 2 == 0)
				// {
				// 	Console.BackgroundColor = ConsoleColor.Cyan;
				// }
				StringBuilder vehicleRow = new();
				vehicleRow.Append(vehicle.Icon);
				vehicleRow.Append($"\t{vehicle.RegistrationNr}");
				vehicleRow.Append($"\t\t{ReturnTrimmedValue(vehicle.Brand, 10)}");
				vehicleRow.Append($"\t{ReturnTrimmedValue(vehicle.Model, 10)}");
				vehicleRow.Append(string.IsNullOrEmpty(vehicle.Color) ? $"\tundefined" : $"\t{ReturnTrimmedValue(vehicle.Color, 10)}");
				vehicleRow.Append(vehicle.ManufacturingYear is not null ? $"\t{vehicle.ManufacturingYear}" : $"\tundefined");
				Console.WriteLine(vehicleRow.ToString());
				// Console.ResetColor();
			}
		}

	}

	public static (bool savedSuccessfully, string? errorUserMsg, string? errorDetails) AddVehicle(Vehicle newVehicle)
	{
		Vehicle[] updatedVehiclesArray = [.. Vehicles, newVehicle];
		// try saving to file
		(bool savedSuccessfully, string? errorMsg) = FileUtils.SaveToVehiclesFile(updatedVehiclesArray);

		if (savedSuccessfully)
		{
			Vehicles = [.. updatedVehiclesArray];
			return (savedSuccessfully: true, errorUserMsg: null, errorDetails: null);
		}
		else
		{
			return (savedSuccessfully: false, errorUserMsg: "Something went wrong, The vehicle could not be saved! ", errorDetails: errorMsg);
		}

	}



}
