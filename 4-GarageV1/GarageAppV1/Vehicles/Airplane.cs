using System;

namespace GarageAppV1.Vehicles;

public class Airplane(string brand, string model, string registrationNr) : Vehicle(brand, model, registrationNr)
{
	public override string Icon => "✈️";
}

