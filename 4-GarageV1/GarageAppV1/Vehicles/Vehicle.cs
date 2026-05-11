using System;
using System.Text;
using GarageAppV1.Shared;

namespace GarageAppV1.Vehicles;

public abstract class Vehicle
{
	public abstract string Icon { get; }
	public string Brand { get; }
	public string Model { get; }
	public string RegistrationNr { get; }
	private int? _manufacturingYear;
	private string? _color;

	public int? ManufacturingYear
	{
		get => _manufacturingYear; set
		{
			int currentYear = DateTime.Now.Year;
			if (value is not null && (value < 1500 || value > currentYear))
				throw new ArgumentException($"ManufacturingYear must be between 1500 - {currentYear}!");

			_manufacturingYear = value;
		}
	}
	public string? Color
	{
		get => _color;
		set
		{
			_color = string.IsNullOrEmpty(value) ? null : value;
		}
	}
	public int? NumberOfEngines { get; set; }
	public FuelTypeEnum? FuelType { get; set; }
	public int? NumberOfSeats { get; set; }
	public double? Length { get; set; }

	public bool Parked { get; private set; } = false;

	public string GetBasicDetailsString()
	{
		return $"{Icon} - [{RegistrationNr}] {Brand} {Model}";
	}

	public void SetParkedStatus(bool newStatus)
	{
		Parked = newStatus;
	}

	public Vehicle(string brand, string model, string registrationNr)
	{
		string registrationNrAllUpper = registrationNr.ToUpper();
		if (VehiclesList.RegistrationNrAlreadyUsed(registrationNrAllUpper))
		{
			throw new Exception("Registration number must be unique! Failed to register car.");
		}

		RegistrationNr = registrationNrAllUpper;
		Brand = brand;
		Model = model;
	}

	#region value checkers
	protected static (bool passed, string? errorMessage) CheckBrandValue(string text)
	{
		if (string.IsNullOrEmpty(text) || text.Trim().Length < 2)
			return (passed: false, errorMessage: "Brand value must have at least 2 characters");
		else
			return (passed: true, errorMessage: null);
	}

	protected static (bool passed, string? errorMessage) CheckModelValue(string text)
	{
		if (string.IsNullOrEmpty(text))
			return (passed: false, errorMessage: "Model value can not be empty!");
		else
			return (passed: true, errorMessage: null);
	}

	protected static (bool passed, string? errorMessage) CheckYearValue(string text)
	{
		if (string.IsNullOrEmpty(text))
			return (passed: true, errorMessage: null);

		if (!int.TryParse(text, out int parsedYear) || parsedYear < 1500 || parsedYear > DateTime.Now.Year)
			return (passed: false, errorMessage: $"Year must be between 1500 - {DateTime.Now.Year}");
		else
			return (passed: true, errorMessage: null);
	}

	protected static (bool passed, string? errorMessage) CheckRegistrationNumber(string text)
	{
		string upperText = (text ?? string.Empty).ToUpper();

		if (string.IsNullOrEmpty(upperText) || upperText.Length != 6)
			return (passed: false, errorMessage: $"Registration number must have 6 chars");
		else if (VehiclesList.RegistrationNrAlreadyUsed(upperText))
			return (passed: false, errorMessage: $"({upperText}) already used, please select another one!");
		else
			return (passed: true, errorMessage: null);
	}

	protected static (bool passed, string? errorMessage) CheckColorValue(string text)
	{
		// since is not an require field, pass it if its empty
		if (string.IsNullOrEmpty(text))
			return (passed: true, errorMessage: null);

		if (text.Trim().Length < 2)
			return (passed: false, errorMessage: "Color value must have at least 2 characters");
		else
			return (passed: true, errorMessage: null);
	}

	protected static (bool passed, string? errorMessage) CheckNumberOfEngines(string text)
	{
		// since is not an require field, pass it if its empty
		if (string.IsNullOrEmpty(text))
			return (passed: true, errorMessage: null);

		if (!int.TryParse(text, out int nrOfEngines) || nrOfEngines <= 0)
			return (passed: false, errorMessage: "Number of engines must be a valid int, >= 1");
		else
			return (passed: true, errorMessage: null);
	}

	protected static (bool passed, string? errorMessage) CheckFuelType(string text)
	{
		// since is not an require field, pass it if its empty
		if (string.IsNullOrEmpty(text))
			return (passed: true, errorMessage: null);

		// check if the enum contains the value (true -> case insensitive)
		StringBuilder str = new();

		foreach (FuelTypeEnum fuelType in Enum.GetValues<FuelTypeEnum>())
		{
			str.Append(fuelType);
			str.Append(", ");
		}
		str.Remove(str.Length - 2, 2);
		str.ToString();


		if (!Enum.TryParse<FuelTypeEnum>(text, true, out _))
			return (passed: false, errorMessage: $"Fuel type must be one of: {str}");
		else
			return (passed: true, errorMessage: null);
	}

	protected static (bool passed, string? errorMessage) CheckNumberOfSeats(string text)
	{
		// since is not an require field, pass it if its empty
		if (string.IsNullOrEmpty(text))
			return (passed: true, errorMessage: null);

		if (!int.TryParse(text, out int nrOfSeats) || nrOfSeats <= 0)
			return (passed: false, errorMessage: "Number of seats must be a valid int, >= 1");
		else
			return (passed: true, errorMessage: null);
	}

	protected static (bool passed, string? errorMessage) CheckLength(string text)
	{
		// since is not an require field, pass it if its empty
		if (string.IsNullOrEmpty(text))
			return (passed: true, errorMessage: null);

		if (!double.TryParse(text, out double length) || length <= 0)
			return (passed: false, errorMessage: "Length must be a valid double, >= 1");
		else
			return (passed: true, errorMessage: null);
	}

	#endregion

	#region Add Vehicle Form stuff
	public static FormOptionsType VehicleFormOptions = [
		(label: "Brand:", value: "", required: true, valueCheck: CheckBrandValue),
		(label: "Model:", value: "", required: true, valueCheck: CheckModelValue),
		(label: "Registration nr:", value: "", required: true, valueCheck: CheckRegistrationNumber),

		(label: "Year:", value: "", required: false, valueCheck: CheckYearValue),
		(label: "Color:", value: "", required: false, valueCheck: CheckColorValue),
		(label: "Number of engines:", value: "", required: false, valueCheck: CheckNumberOfEngines),
		(label: "Fuel type:", value: "", required: false, valueCheck: CheckFuelType),
		(label: "Number of seats:", value: "", required: false, valueCheck: CheckNumberOfSeats),
		(label: "Length:", value: "", required: false, valueCheck: CheckLength),
	];

	public static void LogShit()
	{
		foreach (var item in VehicleFormOptions)
		{
			Console.WriteLine($"{item.label}: {item.value}");
		}
	}

	// Override these in each vehicle, as needed using the "new" keyword instead of "overrite"
	public static FormOptionsType GetFormOptionsByVehicleType(VehicleTypes vehicleType)
	{
		return vehicleType switch
		{
			VehicleTypes.Car => [.. Car.VehicleFormOptions],
			VehicleTypes.Airplane => [.. Airplane.VehicleFormOptions],
			VehicleTypes.Boat => [.. Boat.VehicleFormOptions],
			VehicleTypes.Bus => [.. Buss.VehicleFormOptions],
			VehicleTypes.Motorcycle => [.. Motorcycle.VehicleFormOptions],
			_ => [.. VehicleFormOptions],
		};
	}
	#endregion
}
