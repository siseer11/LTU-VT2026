using System;
using GarageAppV2.Shared;

namespace GarageAppV2.Vehicles;

public class Boat(string brand, string model, string registrationNr) : Vehicle(brand, model, registrationNr)
{
	public override string Icon => VehicleUtils.GetIconByVehicleType(VehicleTypes.Boat);
}

