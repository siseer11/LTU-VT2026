using System;
using System.Runtime.InteropServices.JavaScript;
using GarageAppV1.Vehicles;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GarageAppV1.FileData;

public class FileUtils
{
	private static readonly string VehiclesFileName = "vehicles";
	private static readonly string appDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GarageApp");
	public static (bool successfull, string? errorMsg) SaveToFile(string data, string fileName)
	{
		try
		{
			// check if the directory exists, create it if not
			if (!Directory.Exists(appDirectoryPath))
			{
				Directory.CreateDirectory(appDirectoryPath);
				// Console.WriteLine("✅ Directory created!");
			}
			// check if file exists, create it if not
			string filePath = Path.Combine(appDirectoryPath, $"{fileName}.txt");

			// write to file
			File.WriteAllText(filePath, data);
			Console.WriteLine(data);
			Console.ReadLine();
			return (successfull: true, errorMsg: null);
		}
		catch (Exception e)
		{
			return (successfull: true, errorMsg: e.Message);
		}
	}

	private static JsonSerializerSettings JsonSettings = new()
	{
		TypeNameHandling = TypeNameHandling.All,
		Formatting = Formatting.Indented
	};

	public static T[]? ReadFromFile<T>(string fileName)
	{
		string filePath = Path.Combine(appDirectoryPath, $"{fileName}.txt");

		try
		{
			string fileData = File.ReadAllText(filePath);
			T[] deJsonified = JsonConvert.DeserializeObject<T[]>(fileData, JsonSettings)!;

			return deJsonified;
		}
		catch (FileLoadException fle)
		{
			Console.WriteLine($"🛑 File {fileName} could not be loaded!");
			Console.WriteLine(fle.Message);
		}
		catch (FileNotFoundException fnfe)
		{
			Console.WriteLine($"🛑 File {fileName} could not be found!");
			Console.WriteLine(fnfe.Message);
		}
		catch (Exception exp)
		{
			Console.WriteLine($"🛑 Something went wrong, reading from data file {fileName}!");
			Console.WriteLine(exp.Message);
		}

		return null;
	}

	public static Vehicle[]? ReadFromVehiclesFile()
	{
		return ReadFromFile<Vehicle>("VehiclesFileName");
	}

	public static (bool successfull, string? errorMsg) SaveToVehiclesFile(Vehicle[] data)
	{
		Console.WriteLine(data[0]);
		Console.ReadLine();
		return SaveToFile(JsonConvert.SerializeObject(data, JsonSettings), VehiclesFileName);
	}

};