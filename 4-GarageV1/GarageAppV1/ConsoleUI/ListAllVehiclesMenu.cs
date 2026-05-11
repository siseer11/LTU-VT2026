using System;
using GarageAppV1.Vehicles;

namespace GarageAppV1.ConsoleUI;

public class ListAllVehiclesMenu : Menu
{
	public override void MenuHandler()
	{
		Console.Clear();
		ConsoleUtils.DisplayMenuHeader(["🏎️  Vehicles", "(list of all registered ones)"], '-');
		Console.WriteLine("");
		VehiclesList.ListTableWithAllVehicles();
		ConsoleUtils.KeyboardControllMenu("\n\nNavigate to:", [
			(label: "Vehicles ", value: NavigateToVehiclesMenu),
			(label: "Main Menu ", value: NavigateToMainMenu),
			(label: "Close App ", value: CloseApp)
		], ConsoleUtils.Directions.column)();
	}
}
