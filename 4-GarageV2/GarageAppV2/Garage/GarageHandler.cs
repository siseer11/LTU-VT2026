using GarageAppV2.ConsoleUI;
using GarageAppV2.Contracts;
using GarageAppV2.FileData;
using GarageAppV2.Shared;
using GarageAppV2.Vehicles;

namespace GarageAppV2.Garage;

public class GarageHandler : IGarageHandler
{
	private readonly int MaxCapacity = 200;
	private IGarageDb<Vehicle>? _vehiclesGarage;

	public bool IsGarageBuilt => _vehiclesGarage is not null;

	public void CreateGarage(int capacity)
	{
		if (capacity < 1 || capacity > MaxCapacity)
		{
			throw new ArgumentOutOfRangeException($"The garage can not be built, the number of vehicles has to be between: 1-{MaxCapacity}");
		}

		_vehiclesGarage = new GarageDb<Vehicle>(capacity);

		// save the empty garage to the file
		(bool successfull, string? errorMsg) = SyncFileWithLatestData();
	}

	public bool DeleteGarage()
	{
		(bool successfull, string? errorMsg) = FileUtils.DeleteGarageFile();

		if (successfull)
			_vehiclesGarage = null;

		return successfull;
	}

	public bool SetupGarageFromCache()
	{
		(bool garageCacheDataReadSuccess, string? garageCacheDataReadErrorMsg, var garageFileData) = FileUtils.ReadFromGarageFile();

		if (garageCacheDataReadSuccess)
		{
			if (garageFileData is not null)
			{
				try
				{
					CreateGarage(garageFileData.Capacity);
					foreach (var registrationNumber in garageFileData.ParkedCars)
					{
						if (registrationNumber is not null)
						{
							Vehicle? vehicleData = AppStore.Vehicles.GetVehicleByRegistrationNumber(registrationNumber);
							if (vehicleData is not null)
							{
								_vehiclesGarage!.Add(vehicleData);
							}
						}
					}
				}
				catch (Exception e)
				{
					ConsoleUtils.LogError("Something went wrong creating the garage!");
					ConsoleUtils.LogColor(e.Message, ConsoleColor.Red);
				}

			}

			return true;
		}
		else
		{
			return false;
		}
	}

	public bool CheckIfVehicleIsParkedByRegistrationNr(string registrationNr) => _vehiclesGarage?.GetVehicle(registrationNr) is not null;

	public IEnumerable<Vehicle> GetListOfParkedVehicles() => _vehiclesGarage is not null ? _vehiclesGarage.Where(v => v is not null) : [];

	public int GetNumberOfEmptySpots() => _vehiclesGarage is not null ? _vehiclesGarage.Capacity - _vehiclesGarage.NumberOfParkedVehicles : 0;

	private (bool successfull, string? errorMsg) SyncFileWithLatestData()
	{
		if (_vehiclesGarage is null)
			return (successfull: false, errorMsg: "Garage not initialized!");

		return FileUtils.SaveGarageDataToFile(new GarageState(_vehiclesGarage.Capacity, [.. _vehiclesGarage.Select(v => v?.RegistrationNr)]));
	}

	public bool ParkVehicle(Vehicle vehicle)
	{
		if (_vehiclesGarage is null)
			return false;

		bool parkedSuccessfullyLocally = _vehiclesGarage.Add(vehicle);

		if (parkedSuccessfullyLocally)
		{
			(bool fileSyncSuccess, string? errorMsg) = SyncFileWithLatestData();

			return fileSyncSuccess;
		}

		return parkedSuccessfullyLocally;
	}

	public bool TakeVehicleOut(string registrationNumber)
	{
		if (_vehiclesGarage is null)
			return false;

		bool takenOutSuccessfully = _vehiclesGarage.Remove(registrationNumber);

		if (takenOutSuccessfully)
		{
			(bool fileSyncSuccess, string? errorMsg) = SyncFileWithLatestData();

			return fileSyncSuccess;
		}

		return takenOutSuccessfully;
	}

	public int GetNumberOfParkedVehicles() => _vehiclesGarage is not null ? _vehiclesGarage.NumberOfParkedVehicles : 0;

}
