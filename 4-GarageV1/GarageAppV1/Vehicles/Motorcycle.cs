using System;
using GarageAppV1.Shared;

namespace GarageAppV1.Vehicles;

public class Motorcycle(string brand, string model, string registrationNr) : Vehicle(brand, model, registrationNr)
{
	public override string Icon => VehicleUtils.GetIconByVehicleType(VehicleTypes.Motorcycle);
}

