using System.Numerics;
using GarageAppV2.Vehicles;

namespace GarageAppV2.Tests.Vehicles;

public class VehiclesHandlerTests
{
	[Fact]
	public void AddVehicle_IsTrue_IfNewVehicleAdded()
	{
		var v = new VehiclesHandler();

		bool added = v.AddVehicle(new Car("Honda", "Civc", "TLP123"));

		Assert.True(added);
	}

	[Fact]
	public void AddVehicle_IsFalse_IfSameVehicleAdded()
	{
		var v = new VehiclesHandler();

		v.AddVehicle(new Car("Honda", "Civc", "TLP123"));
		bool addedStatus = v.AddVehicle(new Car("Honda", "Civc", "TLP123"));

		Assert.False(addedStatus);
	}

	[Fact]
	public void RegistrationNrAlreadyUsed_IsTrue_IfExists()
	{
		var v = new VehiclesHandler();

		v.AddVehicle(new Car("Honda", "Civc", "TLP123"));
		bool registartionNrAlreadyUsed = v.RegistrationNrAlreadyUsed("TLP123");

		Assert.True(registartionNrAlreadyUsed);
	}

	[Fact]
	public void RegistrationNrAlreadyUsed_IsFalse_IfNotExistent()
	{
		var v = new VehiclesHandler();

		bool registartionNrAlreadyUsed = v.RegistrationNrAlreadyUsed("TLP123");

		Assert.False(registartionNrAlreadyUsed);
	}

	[Fact]
	public void GetVehicleByRegistrationNumber_IsNull_IfVehicleNotPresent()
	{
		var v = new VehiclesHandler();

		Vehicle? found = v.GetVehicleByRegistrationNumber("TLP123");

		Assert.Null(found);
	}

	[Fact]
	public void GetVehicleByRegistrationNumber_GivesRightVehicle_IfVehiclePresent()
	{
		var v = new VehiclesHandler();

		Vehicle myCar = new Car("Honda", "Civic", "TLP123");
		v.AddVehicle(myCar);
		Vehicle? vehicleFound = v.GetVehicleByRegistrationNumber("TLP123");

		Assert.Equal(myCar, vehicleFound);
	}



	public static IEnumerable<object[]> CorrectNumberOfRegisteredVehiclesTestCases =>
	[
		[
				Array.Empty<Vehicle>(),
				0
		],
		[
				new Vehicle[]
				{
						new Car("Honda", "Civic", "TLP123"),
						new Car("Honda", "Civic", "TLP122"),
				},
				2
		],
		[
				new Vehicle[]
				{
						new Car("Honda", "Civic", "TLP123"),
						new Car("Honda", "Civic", "TLP122"),
						new Car("Honda", "Civic", "TLP123"),
						new Car("Honda", "Civic", "TLP122"),
				},
				2
		],
				[
				new Vehicle[]
				{
						new Car("Honda", "Civic", "TLP123"),
						new Car("Honda", "Civic", "TLP122"),
						new Car("Honda", "Civic", "TLP124"),
						new Car("Honda", "Civic", "TLP125"),
						new Car("Honda", "Civic", "TLP126"),
				},
				5
		],
	];

	[Theory]
	[MemberData(nameof(CorrectNumberOfRegisteredVehiclesTestCases))]
	public void GetNumberOfRegisteredVehicles_ReturnsCorrectValue(Vehicle[] vehicles, int expectedNumber)
	{
		var v = new VehiclesHandler();

		foreach (var vehicle in vehicles)
			v.AddVehicle(vehicle);

		Assert.Equal(expectedNumber, v.GetNumberOfRegisteredVehicles());
	}
}
