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
		RepeatTenTimes = 2,
		ThirdWordInSentance = 3
	}

	static readonly Dictionary<MenuSelections, string> menuSelections = new Dictionary<MenuSelections, string>
	{
		{MenuSelections.CloseApp, "Close App"},
		{MenuSelections.Cinema, "Cinema"},
		{MenuSelections.RepeatTenTimes, "Repeat 10 times"},
		{MenuSelections.ThirdWordInSentance, "Extract the 3rd word in a sentence"}
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
		Console.WriteLine("");
		foreach (var item in menuSelections)
		{
			Console.WriteLine($"{(int)item.Key} - {item.Value}");
		}
		Console.Write("\nNavigate to: ");
	}

	#region Menu handlers
	private static void HandleRepeatTenTimes()
	{
		Console.Clear();
		ConsoleUtils.DisplayMenuHeader(["Repeat 10 times"]);

		Console.WriteLine("\n\nInsert something you want to be repeated 10 times: ");
		string? userInput = Console.ReadLine();

		while (userInput == null || userInput.Trim().Length == 0)
		{
			Console.WriteLine("Invalid input, no empty inputs allowed! Try again:");
			userInput = Console.ReadLine();
		}

		for (int i = 1; i <= 10; i++)
		{
			Console.Write($"{i}.{userInput}");
			if (i < 10)
				Console.Write(", ");
		}
		Console.WriteLine("");
	}

	private static void HandleExtractThirdWordInSentance()
	{
		Console.Clear();
		ConsoleUtils.DisplayMenuHeader(["Extract 3rd word from sentance"]);

		Console.WriteLine("\n\nInsert the sentance (min 3 words): ");
		string? userInput = Console.ReadLine();

		while (userInput == null || userInput.Trim().Split(" ").Where(el => el.Trim().Length > 0).ToArray().Length < 3)
		{
			Console.WriteLine("Invalid input, the sentance must have at least 3 words, separated by a blank space:");
			userInput = Console.ReadLine();
		}

		string thirdWord = userInput.Trim().Split(" ").Where(el => el.Trim().Length > 0).ToArray()[2];

		Console.WriteLine($"The 3rd word in the sentance is: {thirdWord}");
	}
	#endregion

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
				case (int)MenuSelections.RepeatTenTimes:
					HandleRepeatTenTimes();
					ConsoleUtils.HandleQuestionAfterCaseHandled(CloseApp);
					break;
				case (int)MenuSelections.ThirdWordInSentance:
					HandleExtractThirdWordInSentance();
					ConsoleUtils.HandleQuestionAfterCaseHandled(CloseApp);
					break;
			}

		} while (!userClosedMenu);
	}
}
