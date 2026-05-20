using System.Collections;
using GarageAppV2.Contracts;
using GarageAppV2.Vehicles;

namespace GarageAppV2.Garage;

public class GarageDb<T> : IEnumerable<T>, IGarageDb<T> where T : Vehicle
{
	private T?[] _parkedVehicles;
	public int Capacity { get; private set; }
	public int NumberOfParkedVehicles => _parkedVehicles.Count(v => v is not null);

	public GarageDb(int capacity)
	{
		Capacity = capacity;
		_parkedVehicles = new T[capacity];
	}

	public bool Add(T vehicle)
	{
		if (NumberOfParkedVehicles >= Capacity) return false;

		for (int i = 0; i < Capacity; i++)
		{
			if (_parkedVehicles[i] == null)
			{
				_parkedVehicles[i] = vehicle;
				return true;
			}
		}

		return false;
	}

	public bool Remove(string registrationNumber)
	{
		for (int i = 0; i < Capacity; i++)
		{
			if (_parkedVehicles[i]?.RegistrationNr == registrationNumber)
			{
				_parkedVehicles[i] = null;
				return true;
			}
		}
		return false;
	}

	public T? GetVehicle(string registrationNr) => _parkedVehicles.FirstOrDefault(vehicle => vehicle?.RegistrationNr == registrationNr);

	public IEnumerator<T> GetEnumerator()
	{
		foreach (var vehicle in _parkedVehicles)
		{
			if (vehicle != null)
			{
				yield return vehicle;
			}
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
