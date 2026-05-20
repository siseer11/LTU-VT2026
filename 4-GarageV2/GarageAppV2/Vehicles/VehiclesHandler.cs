using System;
using System.Text;
using GarageAppV2.ConsoleUI;
using GarageAppV2.Contracts;
using GarageAppV2.FileData;
using GarageAppV2.Shared;

namespace GarageAppV2.Vehicles;

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

public class VehiclesHandler : IVehiclesHandler
{
	private readonly VehiclesDb _vehiclesDb = new();

	public bool PopulateFromCache()
	{
		(bool successfully, string? errorMsg, var vehiclesListFromFile) = FileUtils.ReadFromVehiclesFile();
		if (successfully && vehiclesListFromFile is not null)
		{
			foreach (var vehicle in vehiclesListFromFile)
			{
				if (vehicle is not null)
				{
					_vehiclesDb.AddVehicle(vehicle);
				}
			}
		}

		return successfully;
	}

	public bool AddVehicle(Vehicle newVehicle)
	{
		bool savedToLocalDb = _vehiclesDb.AddVehicle(newVehicle);

		if (!savedToLocalDb)
		{
			return false;
		}

		(bool savedSuccessfullyToFile, string? errorMsg) = FileUtils.SaveToVehiclesFile(_vehiclesDb.GetArrayOfVehicles());

		return savedSuccessfullyToFile;
	}

	public bool RegistrationNrAlreadyUsed(string registrationNr) => _vehiclesDb.GetVehicleByRegistrationNr(registrationNr) is not null;

	public int GetNumberOfRegisteredVehicles() => _vehiclesDb.NumberOfVehicles;

	public Vehicle? GetVehicleByRegistrationNumber(string registrationNumber) => _vehiclesDb.GetVehicleByRegistrationNr(registrationNumber.Trim().ToUpper());
	public IEnumerable<Vehicle> GetListOfVehiclesWithFilter(Func<Vehicle, bool> filter) => _vehiclesDb.Filter(filter);

	public void RenderTableOfVehicles(IEnumerable<Vehicle> tableVehiclesList)
	{
		if (GetNumberOfRegisteredVehicles() == 0)
		{
			ConsoleUtils.LogWarning("There are no registered vehicles! Start by adding some first.");
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
			foreach (var vehicle in tableVehiclesList)
			{
				bool isVehicleParked = false;
				if (AppStore.Garage.IsGarageBuilt)
					isVehicleParked = AppStore.Garage.CheckIfVehicleIsParkedByRegistrationNr(vehicle.RegistrationNr);

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

	public void ListTableWithAllVehicles()
	{
		RenderTableOfVehicles(_vehiclesDb.GetArrayOfVehicles());
	}

}
