using System;
using GarageAppV1.Vehicles;

namespace GarageAppV1.Garage;

public class GarageClass
{
	public static readonly int MaxCapacity = 200;
	public int GarageCapacity { get; private set; }
	private readonly Vehicle[] _vehiclesParked;

	public GarageClass(int garageCapacity)
	{
		if (garageCapacity < 1 || garageCapacity > MaxCapacity)
		{
			throw new ArgumentOutOfRangeException($"The garage can not be built, the number of cars has to be between: 1-{MaxCapacity}");
		}

		_vehiclesParked = new Vehicle[garageCapacity];
		GarageCapacity = garageCapacity;
	}

}
