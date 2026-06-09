using System;
using System.Runtime.CompilerServices;
using GarageAppV2.Garage;
using GarageAppV2.Vehicles;

namespace GarageAppV2.Tests.Garage;

public class GarageHandlerTests
{
	public static GarageHandler CreateGarage(int capacity = 20)
	{
		var _garage = new GarageHandler();
		_garage.CreateGarage(capacity);

		return _garage;
	}


	[Theory]
	[InlineData(0)]
	[InlineData(1000)]
	public void GarageCanNotBeBuiltWithInvalidNumber(int capacityOutOfBound)
	{
		Assert.Throws<ArgumentOutOfRangeException>(() => CreateGarage(capacityOutOfBound));
	}

	[Fact]
	public void CanParkVehicle()
	{
		bool parked = CreateGarage().ParkVehicle(new Car("Honda", "Civic", "TLP123"));

		Assert.True(parked);
	}

	[Fact]
	public void CanNotParkVehicleTwice()
	{
		var _garage = CreateGarage();

		_garage.ParkVehicle(new Car("Honda", "Civic", "TLP123"));
		bool parked = _garage.ParkVehicle(new Car("Honda", "Civic", "TLP123"));

		Assert.False(parked);
	}

	[Fact]
	public void CanNotParkVehicleIfGarageFull()
	{
		var _garage = CreateGarage(1);

		_garage.ParkVehicle(new Car("Honda", "Civic", "TLP123"));
		bool parked = _garage.ParkVehicle(new Car("Honda", "s2000", "XYZ123"));

		Assert.False(parked);
	}

	[Fact]
	public void CanTakeVehicleOut()
	{
		var _garage = CreateGarage();

		_garage.ParkVehicle(new Car("Honda", "Civic", "TLP123"));
		bool takenOutStatus = _garage.TakeVehicleOut("TLP123");

		Assert.True(takenOutStatus);
	}

	[Fact]
	public void CanNotTakeVehicleIfNotParked()
	{
		var _garage = CreateGarage();

		bool takenOutStatus = _garage.TakeVehicleOut("TLP123");

		Assert.False(takenOutStatus);
	}


	public static IEnumerable<object[]> CorrectNumberOfParkedVehiclesTestCases =>
	[
		[
				new Vehicle[]
				{
						new Car("Honda", "Civic", "TLP123"),
				},
				1
		],

		[
				new Vehicle[]
				{
						new Car("Honda", "Civic", "TLP123"),
						new Car("Honda", "Civic", "TLP122"),
				},
				2
		],
	];
	[Theory]
	[InlineData(null, 0)]
	[MemberData(nameof(CorrectNumberOfParkedVehiclesTestCases))]
	public void GetsCorrectNumberOfParkedVehicles(Vehicle[]? vehicles, int expectedNumber)
	{
		var _garage = CreateGarage(2);


		if (vehicles is not null)
		{
			foreach (Vehicle v in vehicles)
			{
				_garage.ParkVehicle(v);
			}
		}

		Assert.Equal(expectedNumber, _garage.GetNumberOfParkedVehicles());
	}


	public static int CorrectNumberOfEmptyParkingSpotsGarageSize = 10;
	public static IEnumerable<object[]> CorrectNumberOfEmptyParkingSpotsTestCases =>
	[
		[
				new Vehicle[]
				{
						new Car("Honda", "Civic", "TLP123"),
				},
				CorrectNumberOfEmptyParkingSpotsGarageSize - 1
		],

		[
				new Vehicle[]
				{
						new Car("Honda", "Civic", "TLP123"),
						new Car("Honda", "Civic", "TLP122"),
				},
				CorrectNumberOfEmptyParkingSpotsGarageSize - 2
		],
	];

	[Theory]
	[MemberData(nameof(CorrectNumberOfEmptyParkingSpotsTestCases))]
	public void GetsCorrectNumberOfFreeParkingSpots(Vehicle[]? vehicles, int expectedNumber)
	{
		var _garage = CreateGarage(CorrectNumberOfEmptyParkingSpotsGarageSize);

		if (vehicles is not null)
		{
			foreach (Vehicle v in vehicles)
			{
				_garage.ParkVehicle(v);
			}
		}

		Assert.Equal(expectedNumber, _garage.GetNumberOfEmptySpots());
	}

	[Fact]
	public void FindsVehicleByRegistration_IfPresent()
	{
		var _garage = CreateGarage();

		_garage.ParkVehicle(new Car("Honda", "Civic", "TLP123"));
		bool shouldExist = _garage.CheckIfVehicleIsParkedByRegistrationNr("TLP123");

		Assert.True(shouldExist);
	}

	[Fact]
	public void FindsVehicleByRegistration_IfNotPresent()
	{
		var _garage = CreateGarage();

		_garage.ParkVehicle(new Car("Honda", "Civic", "TLP123"));
		bool shouldNotExist = _garage.CheckIfVehicleIsParkedByRegistrationNr("TLP1x3");

		Assert.False(shouldNotExist);
	}

	[Fact]
	public void GetsCorrectListOfParkedVehicles()
	{
		var _garage = CreateGarage();
		Vehicle[] v = [new Car("Honda", "Civic", "TLP123"), new Car("Honda", "Civic", "TLP12x")];

		foreach (Vehicle vehicle in v)
			_garage.ParkVehicle(vehicle);

		var parkedVehicles = _garage.GetListOfParkedVehicles().ToArray();
		for (int i = 0; i < parkedVehicles.Length; i++)
			Assert.Equal(v[i], parkedVehicles[i]);
	}

	[Fact]
	public void ParkVehicle_WhenGarageNotCreated_ReturnsFalse()
	{
		var _garageShell = new GarageHandler();

		bool carParkedSuccessfully = _garageShell.ParkVehicle(new Car("Honda", "Civic", "TLP12x"));

		Assert.False(carParkedSuccessfully);
	}

	[Fact]
	public void TakeVehicleOut_WhenGarageNotCreated_ReturnsFalse()
	{
		var _garageShell = new GarageHandler();

		bool carTakenOutSuccessfully = _garageShell.TakeVehicleOut("TLP12x");

		Assert.False(carTakenOutSuccessfully);
	}

	[Fact]
	public void IsGarageBuilt_IsTrue_IfGarageIsBuilt()
	{
		var _garage = CreateGarage();

		Assert.True(_garage.IsGarageBuilt);
	}

	[Fact]
	public void IsGarageBuilt_IsFalse_IfGarageNotBuilt()
	{
		var _garageShell = new GarageHandler();

		Assert.False(_garageShell.IsGarageBuilt);
	}
}
