using System;
using GarageAppV2.Vehicles;

namespace GarageAppV2.Contracts;

public interface IGarageHandler
{
	bool IsGarageBuilt { get; }
	void CreateGarage(int capacity);
	bool DeleteGarage();
	bool SetupGarageFromCache();
	bool CheckIfVehicleIsParkedByRegistrationNr(string registrationNr);
	IEnumerable<Vehicle> GetListOfParkedVehicles();
	int GetNumberOfEmptySpots();
	bool ParkVehicle(Vehicle vehicle);
	bool TakeVehicleOut(string registrationNumber);
	int GetNumberOfParkedVehicles();
}
