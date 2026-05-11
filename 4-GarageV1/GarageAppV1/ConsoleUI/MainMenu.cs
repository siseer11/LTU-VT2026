namespace GarageAppV1.ConsoleUI;

public class MainMenu : Menu
{
	private (string label, Action value)[] _menuItems { get; set; }

	public MainMenu()
	{
		_menuItems = [
			(label: "1. Garage ", value: NavigateToGarageMenu),
			(label: "2. Vehicles ", value: NavigateToVehiclesMenu),
			(label: "Close App ", value: CloseApp)
		];
	}

	public override void MenuHandler()
	{
		Console.Clear();
		ConsoleUtils.DisplayMenuHeader(["Main menu", "(navigate using tab/arrwos)"], '=');
		Console.WriteLine("\nSelect one:");
		// Here we display the menu and call the return value (the value from the _menuItems) which handles the user selection
		ConsoleUtils.KeyboardControllMenu("", _menuItems.ToList(), ConsoleUtils.Directions.column)();
	}
}
