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
		App.appRunning = false;
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

	/*
	* This will be called while the menu is opened
	* is the function that calls the UI + handlers for each option
	*/
	public abstract void MenuHandler();

	public void Run()
	{
		while (Opened && App.appRunning)
		{
			MenuHandler();
		}
	}

}
