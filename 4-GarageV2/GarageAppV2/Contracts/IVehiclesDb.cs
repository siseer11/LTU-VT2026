using System;
using GarageAppV2.Vehicles;

namespace GarageAppV2.Contracts;

public interface IVehiclesDb : IEnumerable<Vehicle>
{
	int NumberOfVehicles { get; }
	bool AddVehicle(Vehicle vehicle);
	Vehicle? GetVehicleByRegistrationNr(string registrationNr);
	Vehicle[] GetArrayOfVehicles();
	IEnumerable<Vehicle> Filter(Func<Vehicle, bool> filter);
}
