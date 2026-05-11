using GarageAppV1.ConsoleUI;
using GarageAppV1.Garage;

namespace GarageAppV1;

public class App
{
	public static bool appRunning = true;

	private static GarageClass? _newGarage;

	public static GarageClass? NewGarage { get => _newGarage; private set => _newGarage = value; }

	public static void CreateNewGarage(int capacity)
	{
		NewGarage = new GarageClass(capacity);
	}

	public void Run()
	{
		MainMenu mainMenu = new();
		mainMenu.Run();
	}
}
