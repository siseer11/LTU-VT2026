using System;

namespace GarageAppV1.Vehicles;

public class VehiclesList
{
	public static Vehicle[] Vehicles { get; private set; } = [];

	public static bool RegistrationNrAlreadyUsed(string registrationNr)
	{
		return Vehicles.Any((Vehicle v) => v.RegistrationNr == registrationNr);
	}

	public static void AddVehicle(Vehicle newVehicle)
	{
		Vehicles = [.. Vehicles, newVehicle];
	}



}
