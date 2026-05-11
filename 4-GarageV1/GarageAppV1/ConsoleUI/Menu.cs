namespace GarageAppV1.ConsoleUI;

public abstract class Menu
{
	public bool Opened { get; set; } = true;

	public void GoBack()
	{
		Opened = false;
	}

	public static void CloseApp()
	{
		App.CloseApp();
	}

	public void NavigateToNewMenu(Menu NewMenu)
	{
		Opened = false; // close this menu
		NewMenu.Run();
	}

	public void NavigateToMainMenu()
	{
		MainMenu mainMenu = new();
		NavigateToNewMenu(mainMenu);
	}

	public void NavigateToGarageMenu()
	{
		GarageMenu garageMenu = new();
		NavigateToNewMenu(garageMenu);
	}

	/*
	* This will be called while the menu is opened
	* is the function that calls the UI + handlers for each option
	*/
	public abstract void MenuHandler();

	public void Run()
	{
		while (Opened && App.AppRunning)
		{
			MenuHandler();
		}
	}

}
