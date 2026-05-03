using System;

namespace _2_FlowControl.Utils;

public class ConsoleUtils
{
	public enum Directions
	{
		row,
		column
	}

	// Helper that displays the menu header with a "divider line" at top and bottom, to keep it in sync everywhere
	public static void DisplayMenuHeader(string[] lines, char divider = '-')
	{
		int longestLine = 20;

		foreach (string line in lines)
			longestLine = Math.Max(longestLine, line.Length);

		string dividerString = new string(divider, longestLine);
		Console.WriteLine($"{dividerString}\n");
		foreach (string line in lines)
		{
			int lineLength = line.Length;
			Console.WriteLine(line.PadLeft((longestLine + lineLength) / 2));
		}
		Console.WriteLine($"\n{dividerString}");
	}

	public static int GetValidIntFromUserInput(string errorMessage = "\nPlease enter a valid integer: ", bool allowNegativeValues = false, Func<int, bool>? extraIntChecks = null)
	{
		int result;
		bool validIntInserted = false;
		do
		{
			if (int.TryParse(Console.ReadLine(), out result) &&
					allowNegativeValues ? true : result >= 0 &&
					extraIntChecks?.Invoke(result) != false
				)
			{
				validIntInserted = true;
			}
			else
			{
				Console.Write(errorMessage);
			}

		} while (!validIntInserted);

		return result;
	}


	// Helper that displays a selection menu, that the user can toggle between using keyboard
	public static T UserConfirmation<T>(string question, List<(string label, T value)> options, Directions direction = Directions.row)
	{
		if (options == null || options.Count == 0)
			throw new ArgumentException("Options cannot be empty.");

		bool userSelected = false;
		int activeIdx = 0;

		Console.WriteLine(question);
		Console.CursorVisible = false;

		do
		{
			foreach (var option in options)
			{
				if (options[activeIdx].label == option.label)
				{
					Console.BackgroundColor = ConsoleColor.Gray;
					Console.ForegroundColor = ConsoleColor.Black;
				}
				else
					Console.ResetColor();

				if (direction == Directions.row)
					Console.Write(option.label);
				else
					Console.WriteLine(option.label);
			}
			Console.ResetColor();


			ConsoleKey[] forwardKeys = [ConsoleKey.RightArrow, ConsoleKey.DownArrow, ConsoleKey.D, ConsoleKey.S, ConsoleKey.Tab];
			ConsoleKey[] backwardKeys = [ConsoleKey.LeftArrow, ConsoleKey.UpArrow, ConsoleKey.A, ConsoleKey.W];

			ConsoleKey userKeyPressed = Console.ReadKey(true).Key;
			if (userKeyPressed == ConsoleKey.Enter)
			{
				userSelected = true;
			}
			else
			{
				if (backwardKeys.Contains(userKeyPressed))
				{
					activeIdx = activeIdx - 1 < 0 ? options.Count - 1 : activeIdx - 1;
				}
				else if (forwardKeys.Contains(userKeyPressed))
				{
					activeIdx = (activeIdx + 1) % options.Count;
				}

				// Reset the cursor, move it at the top of the options, so next time it will write over the old one
				Console.SetCursorPosition(0, Console.CursorTop - (direction == Directions.row ? 0 : options.Count));
			}

		} while (!userSelected);


		Console.CursorVisible = true;
		return options[activeIdx].value;

	}

	public static bool YesOrNoUserConfirmation(string question)
	{
		return UserConfirmation(question, [(label: " YES ", value: true), (label: " NO ", value: false)]);
	}

	// // Asks the user if they want to perform more actions after an action was completed, or just quit the app
	public static void HandleQuestionAfterCaseHandled(Action closeApp, Action? returnToMainMenu)
	{
		Console.WriteLine("\n===== Your action is completed ======");
		bool userWantsToGoToMainMenu = UserConfirmation("\nDo you want to go back to main menu? Or close the app?", [(label: " Main Menu ", value: true), (label: " Close App ", value: false)]);

		if (userWantsToGoToMainMenu)
		{
			if (returnToMainMenu != null)
			{
				returnToMainMenu();
			}
		}
		else
		{
			closeApp();
		}

	}

}
