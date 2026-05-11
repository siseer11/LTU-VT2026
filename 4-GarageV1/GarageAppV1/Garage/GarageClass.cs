using GarageAppV1.ConsoleUI;
using GarageAppV1.FileData;
using GarageAppV1.Vehicles;

namespace GarageAppV1.Garage;

public class GarageClass
{
	public static readonly int MaxCapacity = 200;
	public int GarageCapacity { get; private set; }
	public readonly Vehicle[] ParkingSpots;

	public bool CheckIfCarIsParkedByRegistrationNr(string registrationNr)
	{
		if (ParkingSpots is not null)
		{
			return ParkingSpots.Any((Vehicle? parkedVehicle) => (
				parkedVehicle is not null ? parkedVehicle.RegistrationNr == registrationNr.ToUpper() : false
			));
		}
		else
			return false;
	}

	public Vehicle[] GetListOfParkedCars()
	{
		return [.. ParkingSpots.Where(parkingSpot => parkingSpot == null)];
	}

	private (bool successfull, string? errorMsg) SyncFileWithLatestData()
	{
		return FileUtils.SaveGarageDataToFile(ParkingSpots);
	}

	public int GetNumberOfEmptySpots()
	{
		int emptySpots = 0;

		foreach (Vehicle? parkingSpot in ParkingSpots)
		{
			if (parkingSpot is null)
			{
				emptySpots++;
			}
		}

		return emptySpots;
	}

	public (bool parkedSuccessfully, string? errorMessage) ParkVehicle(Vehicle vehicle)
	{
		int indexOfFirstEmptySpot = Array.FindIndex(ParkingSpots, p => p == null);

		if (indexOfFirstEmptySpot == -1)
			return (parkedSuccessfully: false, errorMessage: "No empty spots left!");
		else
		{
			ParkingSpots[indexOfFirstEmptySpot] = vehicle;

			// TODO: Handle this better;
			(bool dataSaveSuccessfullyToFile, string? dataSaveToFileError) = SyncFileWithLatestData();
			if (!dataSaveSuccessfullyToFile)
			{
				ConsoleUtils.LogError("Something went wrong when updating the database (file)");
				if (dataSaveToFileError is not null)
					ConsoleUtils.LogColor(dataSaveToFileError, ConsoleColor.Red);
			}

			return (parkedSuccessfully: true, errorMessage: null);
		}
	}

	public GarageClass(int garageCapacity, Vehicle[]? vehiclesInitialyParked)
	{
		if (garageCapacity < 1 || garageCapacity > MaxCapacity)
		{
			throw new ArgumentOutOfRangeException($"The garage can not be built, the number of cars has to be between: 1-{MaxCapacity}");
		}

		ParkingSpots = new Vehicle[garageCapacity];

		if (vehiclesInitialyParked is not null)
		{
			if (vehiclesInitialyParked.Length > garageCapacity) // THis should be impossible, only if user changes the data files should happen
			{
				ConsoleUtils.LogError("Fatal error! More cars than the garage limit!");
				Console.ReadKey(true);
				App.CloseApp();
			}
			else
			{
				for (int i = 0; i < vehiclesInitialyParked.Length; i++)
				{
					ParkingSpots[i] = vehiclesInitialyParked[i];
				}
			}
		}
		else
		{
			// First time we generate the garage, save/create the garage local (data) file
			(bool successfull, string? errorMsg) = SyncFileWithLatestData();
			if (successfull == false)
			{
				ConsoleUtils.LogError("Something went wrong saving garage file! Try again later");
				if (errorMsg is not null)
					ConsoleUtils.LogColor($"({errorMsg})", ConsoleColor.Red);

				Console.ReadKey(true);
				App.CloseApp();
			}
		}

		GarageCapacity = garageCapacity;
	}

}
