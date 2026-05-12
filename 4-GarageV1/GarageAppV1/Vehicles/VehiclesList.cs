using System;
using System.Text;
using GarageAppV1.ConsoleUI;
using GarageAppV1.FileData;

namespace GarageAppV1.Vehicles;

enum VehicleListTableColumns
{
	Registration,
	Brand,
	Model,
	Color,
	Year,
	Parked,
	Type
}

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
			ConsoleUtils.LogSuccess("Vehicles data populated successfully!");
		}
		else if (!successfully)
		{
			ConsoleUtils.LogError("Could not update the vehicles list with the data from file!");
			if (!string.IsNullOrEmpty(errorMsg))
				ConsoleUtils.LogColor($"({errorMsg})", ConsoleColor.Red);
		}

	}

	public static void RenderTableOfVehicles(Vehicle[] tableVehiclesList)
	{
		if (tableVehiclesList.Length == 0)
		{
			Console.WriteLine("No vehicles to be shown!");
		}
		else
		{
			int colGap = 4;
			(VehicleListTableColumns id, int columnGap, int columnWidht)[] tableColumnSettings = [
				(id: VehicleListTableColumns.Registration, columnGap: colGap, columnWidht: 12),
				(id: VehicleListTableColumns.Brand, columnGap: colGap, columnWidht: 10),
				(id: VehicleListTableColumns.Model, columnGap: colGap, columnWidht: 10),
				(id: VehicleListTableColumns.Color, columnGap: colGap, columnWidht: 8),
				(id: VehicleListTableColumns.Year, columnGap: colGap, columnWidht: 4),
				(id: VehicleListTableColumns.Parked, columnGap: colGap, columnWidht: 6),
				(id: VehicleListTableColumns.Type, columnGap: 1, columnWidht: 4),
			];

			List<Dictionary<VehicleListTableColumns, string>> tableRows = [];

			Dictionary<VehicleListTableColumns, string> tableHeader = new()
			{
				{VehicleListTableColumns.Registration, "Registration"},
				{VehicleListTableColumns.Brand, "Brand"},
				{VehicleListTableColumns.Model, "Model"},
				{VehicleListTableColumns.Color, "Color"},
				{VehicleListTableColumns.Year, "Year"},
				{VehicleListTableColumns.Parked, "Parked"},
				{VehicleListTableColumns.Type, "Type"},
			};
			tableRows.Add(tableHeader);

			// go over each vehicle and generate "a row" for the Table
			for (int i = 0; i < tableVehiclesList.Length; i++)
			{
				Vehicle vehicle = tableVehiclesList[i];

				bool isVehicleParked = false;
				if (App.NewGarage is not null)
					isVehicleParked = App.NewGarage.CheckIfVehicleIsParkedByRegistrationNr(vehicle.RegistrationNr);

				Dictionary<VehicleListTableColumns, string> vehicleRow = new()
				{
					{VehicleListTableColumns.Registration, vehicle.RegistrationNr},
					{VehicleListTableColumns.Brand, vehicle.Brand},
					{VehicleListTableColumns.Model, vehicle.Model},
					{VehicleListTableColumns.Color, vehicle.Color is not null ? vehicle.Color : "-"},
					{VehicleListTableColumns.Year, vehicle.ManufacturingYear is not null ? vehicle.ManufacturingYear.ToString()! : "-"},
					{VehicleListTableColumns.Parked, isVehicleParked ? "Yes" : "No"},
					{VehicleListTableColumns.Type, vehicle.Icon},
				};

				tableRows.Add(vehicleRow);
			}

			ConsoleUtils.RenderTable<VehicleListTableColumns>(tableColumnSettings, tableRows, showLinesInBetween: true);
		}

	}

	public static Vehicle[] GetListOfVehiclesWithFilter(Func<Vehicle, bool> filter)
	{
		return [.. Vehicles.Where(v => filter(v))];
	}

	public static void ListTableWithAllVehicles()
	{
		RenderTableOfVehicles(Vehicles);
	}

	public static Vehicle? GetVehicleByRegistrationNumber(string registrationNumber)
	{
		return Vehicles.FirstOrDefault(v => v.RegistrationNr == registrationNumber.ToUpper());
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
