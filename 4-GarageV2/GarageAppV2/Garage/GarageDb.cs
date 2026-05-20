using System.Collections;
using GarageAppV2.Vehicles;

namespace GarageAppV2.Garage;

public class GarageDb<T> : IEnumerable<T> where T : Vehicle
{
	private T?[] _parkedVehicles;
	public int Capacity { get; private set; }
	public int NumberOfParkedVehicles { get; private set; }

	public GarageDb(int capacity)
	{
		Capacity = capacity;
		_parkedVehicles = new T[capacity];
		NumberOfParkedVehicles = 0;
	}

	public bool Add(T vehicle)
	{
		if (NumberOfParkedVehicles >= Capacity) return false;

		for (int i = 0; i < Capacity; i++)
		{
			if (_parkedVehicles[i] == null)
			{
				_parkedVehicles[i] = vehicle;
				NumberOfParkedVehicles++;
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
				NumberOfParkedVehicles--;
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
