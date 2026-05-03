using System;
using _2_FlowControl.Utils;

namespace _2_FlowControl.Features;

public class MainMenu()
{
	private bool userClosedMenu = false;
	public enum MenuSelections
	{
		CloseApp = 0,
		Cinema = 1,
	}

	static readonly Dictionary<MenuSelections, string> menuSelections = new Dictionary<MenuSelections, string>
	{
		{MenuSelections.CloseApp, "Close App"},
		{MenuSelections.Cinema, "Cinema"},

	};

	public void CloseApp()
	{
		Console.Clear();
		Console.WriteLine("\n--> Aplication closed! Thanks for using our app.");
		userClosedMenu = true;
	}

	private static void Display()
	{
		Console.Clear();
		ConsoleUtils.DisplayMenuHeader(["MAIN MENU", "(navigate by typing a number)"], '=');
		foreach (var item in menuSelections)
		{
			Console.WriteLine($"{(int)item.Key} - {item.Value}");
		}
		Console.Write("\nNavigate to: ");
	}

	public void Run()
	{
		do
		{
			Display();

			string invalidMenuSelectionMessage = $"  Please insert an valid value between 0 - {menuSelections.Count - 1}: ";
			int userMenuSelection = ConsoleUtils.GetValidIntFromUserInput(invalidMenuSelectionMessage, extraIntChecks: (i) => i <= menuSelections.Count - 1);

			switch (userMenuSelection)
			{
				case (int)MenuSelections.CloseApp:
					CloseApp();
					break;
				case (int)MenuSelections.Cinema:
					new CinemaMenu(this).Run();
					break;
			}

		} while (!userClosedMenu);
	}
}
