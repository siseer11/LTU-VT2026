using System;
using System.Text;
using GarageAppV1.ConsoleUI;
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

	public static string ReturnValueOfSize(string txt, int size, int paddingRight)
	{
		string trimmed = txt.Length > size ? $"{txt[..(size - 2)]}.." : txt;

		return $"{trimmed}{new string(' ', paddingRight + (size - trimmed.Length))}";
	}

	public static void PopulateWithDataFromFile()
	{
		(bool successfully, string? errorMsg, var vehiclesListFromFile) = FileUtils.ReadFromVehiclesFile();

		if (successfully && vehiclesListFromFile is not null)
		{
			Vehicles = vehiclesListFromFile;
		}
		else if (!successfully)
		{
			ConsoleUtils.LogError("Could not update the vehicles list with the data from file!");
			if (!string.IsNullOrEmpty(errorMsg))
				ConsoleUtils.LogColor($"({errorMsg})", ConsoleColor.Red);
		}

	}

	public static void ListTableWithAllVehicles()
	{
		if (Vehicles.Length == 0)
		{
			Console.WriteLine("There are no vehicles registered 😢");
		}
		else
		{
			int colGap = 4;
			int typeColWidth = 4;
			int registartionColWidth = 12;
			int brandColWidth = 10;
			int modelColWidth = 10;
			int colorColWidth = 8;
			int yearColWidth = 4;
			int parkedColWidth = 6;

			StringBuilder header = new();
			header.Append(ReturnValueOfSize("Registration", registartionColWidth, colGap));
			header.Append(ReturnValueOfSize("Brand", brandColWidth, colGap));
			header.Append(ReturnValueOfSize("Model", modelColWidth, colGap));
			header.Append(ReturnValueOfSize("Color", colorColWidth, colGap));
			header.Append(ReturnValueOfSize("Year", yearColWidth, colGap));
			header.Append(ReturnValueOfSize("Parked", parkedColWidth, colGap));
			header.Append(ReturnValueOfSize("Type", typeColWidth, 1));
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.WriteLine(header.ToString());
			Console.ResetColor();

			for (int i = 0; i < Vehicles.Length; i++)
			{
				Vehicle vehicle = Vehicles[i];
				// if (i % 2 == 0)
				// {
				// 	Console.BackgroundColor = ConsoleColor.Gray;
				// 	Console.ForegroundColor = ConsoleColor.Black;
				// }
				StringBuilder vehicleRow = new();
				vehicleRow.Append(ReturnValueOfSize(vehicle.RegistrationNr, registartionColWidth, colGap));
				vehicleRow.Append(ReturnValueOfSize(vehicle.Brand, brandColWidth, colGap));
				vehicleRow.Append(ReturnValueOfSize(vehicle.Model, modelColWidth, colGap));
				vehicleRow.Append(ReturnValueOfSize(vehicle.Color is not null ? vehicle.Color : "-", colorColWidth, colGap));
				vehicleRow.Append(ReturnValueOfSize(vehicle.ManufacturingYear is not null ? vehicle.ManufacturingYear.ToString()! : "-", yearColWidth, colGap));
				vehicleRow.Append(ReturnValueOfSize(vehicle.Parked ? "Yes" : "No", parkedColWidth, colGap));
				vehicleRow.Append(ReturnValueOfSize(vehicle.Icon, typeColWidth, 1));
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
