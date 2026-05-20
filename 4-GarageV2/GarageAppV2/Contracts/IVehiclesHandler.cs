using System;
using GarageAppV2.Vehicles;

namespace GarageAppV2.Contracts;

public interface IVehiclesHandler
{
	bool PopulateFromCache();
	bool AddVehicle(Vehicle newVehicle);
	bool RegistrationNrAlreadyUsed(string registrationNr);
	int GetNumberOfRegisteredVehicles();
	Vehicle? GetVehicleByRegistrationNumber(string registrationNumber);
	IEnumerable<Vehicle> GetListOfVehiclesWithFilter(Func<Vehicle, bool> filter);
	void RenderTableOfVehicles(IEnumerable<Vehicle> tableVehiclesList);
	void ListTableWithAllVehicles();
}
