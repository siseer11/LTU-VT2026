using System;

namespace GarageAppV2.Contracts;

public interface IGarageDb<T> : IEnumerable<T>
{
	int Capacity { get; }
	int NumberOfParkedVehicles { get; }
	bool Add(T vehicle);
	bool Remove(string registrationNumber);
	T? GetVehicle(string registrationNr);
}
