using GarageAppV2.Shared;
using GarageAppV2.Vehicles;

namespace GarageAppV2.Tests.Vehicles;

public class TestVehicle(string brand, string model, string registrationNr) : Vehicle(brand, model, registrationNr)
{
	public override string Icon => VehicleUtils.GetIconByVehicleType(VehicleTypes.Car);
};


public class VehiclesTests
{

	[Theory]
	[InlineData("")]
	[InlineData("x")]
	[InlineData("xpa")]
	[InlineData("xpa21")]
	public void CreatingVehicle_Throws_WhenPassedInvalidRegNr(string invalidRegistration)
	{
		Assert.Throws<Exception>(() => new TestVehicle("Honda", "Civic", invalidRegistration));
	}

	[Theory]
	[InlineData("")]
	[InlineData("x")]
	public void CreatingVehicle_Throws_WhenPassedInvalidBrand(string invalidBrand)
	{
		Assert.Throws<Exception>(() => new TestVehicle(invalidBrand, "xyz", "TTL123"));
	}

	[Fact]
	public void CreatingVehicle_Throws_WhenNoModelPassed()
	{
		Assert.Throws<Exception>(() => new TestVehicle("Honda", "", "TTL123"));
	}

	[Fact]
	public void CreatingVehicle_Succeeds_WhenBasicDataPassed()
	{
		var testVehicle = new TestVehicle("Honda", "Test", "TTT111");

		Assert.NotNull(testVehicle);
	}

	[Theory]
	[InlineData(100)]
	[InlineData(2080)]
	[InlineData(1499)]
	[InlineData(-14)]
	public void CreatingVehicle_Throws_WhenPassedInvalidYear(int invalidManufacturingYear)
	{
		Assert.Throws<ArgumentException>(() => new TestVehicle("Test", "Test", "TTL123") { ManufacturingYear = invalidManufacturingYear });
	}

	[Theory]
	[InlineData(1993)]
	[InlineData(2025)]
	[InlineData(1500)]
	public void CreatingVehicle_Succeeds_WhenPassedValidYear(int manufacturingYear)
	{
		var testVehicle = new TestVehicle("Honda", "Test", "TTT111") { ManufacturingYear = manufacturingYear };

		Assert.NotNull(testVehicle);
	}

	[Fact]
	public void CreatingVehicle_Throws_WhenPassedInvalidColorValue()
	{
		Assert.Throws<ArgumentException>(() => new TestVehicle("Test", "Test", "TTL123") { Color = "a" });
	}

	[Theory]
	[InlineData("red")]
	[InlineData("abz")]
	public void CreatingVehicle_Succeeds_WhenPassedValidColorValue(string colorValue)
	{
		var testVehicle = new TestVehicle("Honda", "Test", "TTT111") { Color = colorValue };
		Assert.NotNull(testVehicle);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(-1)]
	public void CreatingVehicle_Throws_WhenPassedInvalidNumberOfSeats(int invalidNumberOfseats)
	{
		Assert.Throws<ArgumentException>(() => new TestVehicle("Test", "Test", "TTL123") { NumberOfSeats = invalidNumberOfseats });
	}

	[Theory]
	[InlineData(4)]
	[InlineData(2)]
	[InlineData(120)]
	public void CreatingVehicle_Succeeds_WhenPassedValidNumberOfSeats(int numberOfseats)
	{
		var testVehicle = new TestVehicle("Test", "Test", "TTL123") { NumberOfSeats = numberOfseats };

		Assert.NotNull(testVehicle);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(-1)]
	public void CreatingVehicle_Throws_WhenPassedInvalidLength(int invalidLength)
	{
		Assert.Throws<ArgumentException>(() => new TestVehicle("Test", "Test", "TTL123") { Length = invalidLength });
	}

	[Theory]
	[InlineData(40)]
	[InlineData(12)]
	[InlineData(400)]
	public void CreatingVehicle_Succeeds_WhenPassedValidLength(int length)
	{
		var testVehicle = new TestVehicle("Test", "Test", "TTL123") { Length = length };

		Assert.NotNull(testVehicle);
	}

	[Fact]
	public void GetBasicDetailsString_Returns_CorrectString()
	{
		var testVehicle = new TestVehicle("Test", "Test", "TTL123");

		string testVehicleBasicDetails = testVehicle.GetBasicDetailsString();

		Assert.Equal($"{VehicleUtils.GetIconByVehicleType(VehicleTypes.Car)} - [TTL123] Test Test", testVehicleBasicDetails);
	}


	public static IEnumerable<object[]> VehiclesDetailsStringTestCases =>
	[
		[
				new Car("Honda", "Civic", "TLP123"),
				"Year: - | Color: - | NumberOfEngines: - | FuelType: - | NumberOfSeats: - | Length: -"
		],
		[
				new Car("Honda", "Civic", "TLP123"){ManufacturingYear = 2012, Color = "red", Length = 120, NumberOfEngines = 1, NumberOfSeats = 4, FuelType = FuelTypeEnum.Gasoline},
				"Year: 2012 | Color: red | NumberOfEngines: 1 | FuelType: Gasoline | NumberOfSeats: 4 | Length: 120"
		],
		[
				new Car("Honda", "Civic", "TLP123"){Color = "red", Length = 120, NumberOfEngines = 1},
				"Year: - | Color: red | NumberOfEngines: 1 | FuelType: - | NumberOfSeats: - | Length: 120"
		],
		[
				new Car("Honda", "Civic", "TLP123"){ManufacturingYear = 2012, FuelType = FuelTypeEnum.Gasoline},
				"Year: 2012 | Color: - | NumberOfEngines: - | FuelType: Gasoline | NumberOfSeats: - | Length: -"
		],
	];
	[Theory]
	[MemberData(nameof(VehiclesDetailsStringTestCases))]
	public void GetVehicleDetailsString_Returns_CorrectString(Vehicle v, string expectedString)
	{
		string vDetailsString = v.GetVehicleDetailsString();

		Assert.Equal(expectedString, vDetailsString);
	}

}