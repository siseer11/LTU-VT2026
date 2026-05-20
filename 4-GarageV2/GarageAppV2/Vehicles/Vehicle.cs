using System;
using System.Text;
using GarageAppV2.Shared;

namespace GarageAppV2.Vehicles;

public abstract class Vehicle
{
	public abstract string Icon { get; }
	public string Brand { get; }
	public string Model { get; }
	public string RegistrationNr { get; }
	private int? _manufacturingYear;
	private string? _color;
	private FuelTypeEnum? _fuelType;
	private int? _numberOfSeats;
	private int? _numberOfEngines;
	private double? _length;

	public int? ManufacturingYear
	{
		get => _manufacturingYear;
		set
		{
			(bool yearPassed, string? yearErrorMessage) = CheckYearValue(value?.ToString());

			if (!yearPassed)
				throw new ArgumentException(yearErrorMessage);

			_manufacturingYear = value;
		}
	}
	public string? Color
	{
		get => _color;
		set
		{
			(bool colorPassed, string? colorErrorMessage) = CheckColorValue(value?.ToString());
			if (!colorPassed)
				throw new ArgumentException(colorErrorMessage);

			_color = string.IsNullOrEmpty(value) ? null : value;
		}
	}
	public int? NumberOfEngines
	{
		get => _numberOfEngines;
		set
		{
			(bool numberOfEnginesPassed, string? numberOfEnginesErrorMessage) = CheckNumberOfEngines(value?.ToString());

			if (!numberOfEnginesPassed)
				throw new ArgumentException(numberOfEnginesErrorMessage);

			_numberOfEngines = value;
		}
	}
	public FuelTypeEnum? FuelType
	{
		get => _fuelType;
		set
		{
			(bool fuelTypePassed, string? fuelTypeErrorMessage) = CheckFuelType(value?.ToString());
			if (!fuelTypePassed)
				throw new ArgumentException(fuelTypeErrorMessage);

			_fuelType = value;
		}
	}
	public int? NumberOfSeats
	{
		get => _numberOfSeats;
		set
		{
			(bool nrOfSeatsPassed, string? nrOfSeatsErrorMessage) = CheckNumberOfSeats(value?.ToString());

			if (!nrOfSeatsPassed)
				throw new ArgumentException(nrOfSeatsErrorMessage);

			_numberOfSeats = value;
		}
	}
	public double? Length
	{
		get => _length;
		set
		{
			(bool lengthPassed, string? lengthErrorMessage) = CheckLength(value?.ToString());

			if (!lengthPassed)
				throw new ArgumentException(lengthErrorMessage);

			_length = value;
		}
	}
	public string GetBasicDetailsString()
	{
		return $"{Icon} - [{RegistrationNr}] {Brand} {Model}";
	}

	public string GetVehicleDetailsString()
	{
		return $"Year: {ManufacturingYear?.ToString() ?? "-"} | Color: {Color ?? "-"} | NumberOfEngines: {NumberOfEngines?.ToString() ?? "-"} | FuelType: {FuelType.ToString() ?? "-"} | NumberOfSeats: {NumberOfSeats?.ToString() ?? "-"} | Length: {Length?.ToString() ?? "-"}";
	}
	public Vehicle(string brand, string model, string registrationNr)
	{
		#region checks for input values
		(bool regNrPassed, string? regNrErrorMessage) = CheckRegistrationNumber(registrationNr);
		if (!regNrPassed)
		{
			throw new Exception(regNrErrorMessage);
		}

		(bool brandPassed, string? brandErrorMessage) = CheckBrandValue(brand);
		if (!brandPassed)
		{
			throw new Exception(brandErrorMessage);
		}

		(bool modelPassed, string? modelErrorMessage) = CheckModelValue(model);
		if (!modelPassed)
		{
			throw new Exception(modelErrorMessage);
		}
		#endregion

		RegistrationNr = registrationNr.ToUpper();
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

	protected static (bool passed, string? errorMessage) CheckYearValue(string? text)
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
		else if (AppStore.Vehicles.RegistrationNrAlreadyUsed(upperText))
			return (passed: false, errorMessage: $"({upperText}) already used, please select another one!");
		else
			return (passed: true, errorMessage: null);
	}

	protected static (bool passed, string? errorMessage) CheckColorValue(string? text)
	{
		// since is not an require field, pass it if its empty
		if (string.IsNullOrEmpty(text))
			return (passed: true, errorMessage: null);

		if (text.Trim().Length < 2)
			return (passed: false, errorMessage: "Color value must have at least 2 characters");
		else
			return (passed: true, errorMessage: null);
	}

	protected static (bool passed, string? errorMessage) CheckNumberOfEngines(string? text)
	{
		// since is not an require field, pass it if its empty
		if (string.IsNullOrEmpty(text))
			return (passed: true, errorMessage: null);

		if (!int.TryParse(text, out int nrOfEngines) || nrOfEngines <= 0)
			return (passed: false, errorMessage: "Number of engines must be a valid int, >= 1");
		else
			return (passed: true, errorMessage: null);
	}

	protected static (bool passed, string? errorMessage) CheckFuelType(string? text)
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

	protected static (bool passed, string? errorMessage) CheckNumberOfSeats(string? text)
	{
		// since is not an require field, pass it if its empty
		if (string.IsNullOrEmpty(text))
			return (passed: true, errorMessage: null);

		if (!int.TryParse(text, out int nrOfSeats) || nrOfSeats <= 0)
			return (passed: false, errorMessage: "Number of seats must be a valid int, >= 1");
		else
			return (passed: true, errorMessage: null);
	}

	protected static (bool passed, string? errorMessage) CheckLength(string? text)
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
