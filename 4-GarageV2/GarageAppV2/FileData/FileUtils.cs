using System;
using System.Runtime.InteropServices.JavaScript;
using GarageAppV2.Shared;
using GarageAppV2.Vehicles;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GarageAppV2.FileData;

public class FileUtils
{
	private static readonly string VehiclesFileName = "vehicles";
	private static readonly string GarageFileName = "garage";
	private static readonly string appDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GarageAppV2");
	#region Base methods
	public static (bool successfull, string? errorMsg) SaveToFile(string data, string fileName)
	{
		// If the app does not have "cacheEnabled" don't even try to save
		if (!AppStore.CacheEnabled)
		{
			return (successfull: true, errorMsg: null);
		}

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

	public static (bool successfully, string? errorMsg, T? data) ReadFromFile<T>(string fileName)
	{
		// If the app does not have "cacheEnabled" don't even try to save
		if (!AppStore.CacheEnabled)
		{
			return (successfully: true, errorMsg: null, data: default);
		}

		string filePath = Path.Combine(appDirectoryPath, $"{fileName}.txt");

		try
		{
			if (!File.Exists(filePath))
			{
				// File.Create(filePath);
				return (successfully: true, errorMsg: null, data: default);
			}

			string fileData = File.ReadAllText(filePath);
			T deJsonified = JsonConvert.DeserializeObject<T>(fileData, JsonSettings)!;

			return (successfully: true, errorMsg: null, data: deJsonified);
		}
		catch (Exception e)
		{
			return (successfully: false, errorMsg: e.Message, data: default);
		}
		// catch (FileLoadException fle)
		// {
		// 	Console.WriteLine($"🛑 File {fileName} could not be loaded!");
		// 	Console.WriteLine(fle.Message);
		// }
		// catch (FileNotFoundException fnfe)
		// {
		// 	Console.WriteLine($"🛑 File {fileName} could not be found!");
		// 	Console.WriteLine(fnfe.Message);
		// }
		// catch (Exception exp)
		// {
		// 	Console.WriteLine($"🛑 Something went wrong, reading from data file {fileName}!");
		// 	Console.WriteLine(exp.Message);
		// }
	}

	public static (bool successfull, string? errorMsg) DeleteFile(string fileName)
	{
		string filePath = Path.Combine(appDirectoryPath, $"{fileName}.txt");

		try
		{
			File.Delete(filePath);

			return (successfull: true, errorMsg: null);
		}
		catch (Exception e)
		{
			return (successfull: false, errorMsg: e.Message);
		}
	}
	#endregion

	#region Specialized methods
	public static (bool successfully, string? errorMsg, Vehicle[]? data) ReadFromVehiclesFile()
	{
		return ReadFromFile<Vehicle[]>(VehiclesFileName);
	}

	public static (bool successfull, string? errorMsg) SaveToVehiclesFile(Vehicle[] data)
	{
		return SaveToFile(JsonConvert.SerializeObject(data, JsonSettings), VehiclesFileName);
	}

	public static (bool successfully, string? errorMsg, GarageState? data) ReadFromGarageFile()
	{
		return ReadFromFile<GarageState>(GarageFileName);
	}
	public static (bool successfull, string? errorMsg) SaveGarageDataToFile(GarageState data)
	{
		return SaveToFile(JsonConvert.SerializeObject(data, JsonSettings), GarageFileName);
	}
	public static (bool successfull, string? errorMsg) DeleteGarageFile()
	{
		return DeleteFile(GarageFileName);
	}
	#endregion
};