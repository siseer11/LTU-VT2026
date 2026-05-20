using System.Collections;
namespace GarageAppV2.Vehicles;

public class VehiclesDb : IEnumerable<Vehicle>
{
	private Dictionary<string, Vehicle> _vehicles = [];
	public int NumberOfVehicles { get; private set; } = 0;

	public bool AddVehicle(Vehicle vehicle)
	{
		if (_vehicles.ContainsKey(vehicle.RegistrationNr))
		{
			return false;
		}

		_vehicles.Add(vehicle.RegistrationNr, vehicle);
		NumberOfVehicles++;

		return true;
	}

	public Vehicle? GetVehicleByRegistrationNr(string registrationNr)
	{
		_vehicles.TryGetValue(registrationNr, out Vehicle? v);

		return v;
	}

	public Vehicle[] GetArrayOfVehicles() => [.. _vehicles.Values];

	public IEnumerable<Vehicle> Filter(Func<Vehicle, bool> filter) => _vehicles.Values.Where(filter);

	public IEnumerator<Vehicle> GetEnumerator() => _vehicles.Values.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
