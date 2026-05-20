namespace GarageAppV2.ConsoleUI;

public class VehiclesMenu : Menu
{
	private (string label, Action value)[] MenuItems { get; set; } = [];

	public VehiclesMenu()
	{
		MenuItems = [
			(label: "1. Register new ", value: RegisterNewHandler),
			(label: "2. List all ", value: ListAllVehiclesHandler),
			(label: "3. Vehicle full details (by registration number) ", value: VehicleFullDetailsHandler),
			(label: "Main menu ", value: NavigateToMainMenu),
			(label: "Close app ", value: CloseApp)
		 ];
	}

	public override void MenuHandler()
	{
		Console.Clear();
		ConsoleUtils.DisplayMenuHeader(["🏎️  Vehicles", "(manage owned vehicels)"], '-');
		Console.WriteLine("\nSelect one (use tab/arrows):");
		ConsoleUtils.KeyboardControllMenu("", MenuItems.ToList(), ConsoleUtils.Directions.column)();
	}

	private void RegisterNewHandler()
	{
		AddNewVehicleMenu addNewVehicleMenu = new();
		NavigateToNewMenu(addNewVehicleMenu);
	}

	private void ListAllVehiclesHandler()
	{
		ListAllVehiclesMenu listAllVehiclesMenu = new();
		NavigateToNewMenu(listAllVehiclesMenu);
	}

	private void VehicleFullDetailsHandler()
	{
		VehicleFullDetailsMenu vehicleFullDetailsMenu = new();
		NavigateToNewMenu(vehicleFullDetailsMenu);
	}
}
