using System;

namespace GarageAppV2.ConsoleUI;

public partial class ConsoleUtils
{
	private static readonly ConsoleKey[] forwardKeys = [ConsoleKey.RightArrow, ConsoleKey.DownArrow, ConsoleKey.D, ConsoleKey.S, ConsoleKey.Tab];
	private static readonly ConsoleKey[] backwardKeys = [ConsoleKey.LeftArrow, ConsoleKey.UpArrow, ConsoleKey.A, ConsoleKey.W];
	public enum Directions
	{
		row,
		column
	}
	public static void LogColor(string msg, ConsoleColor color)
	{
		Console.ForegroundColor = color;
		Console.WriteLine(msg);
		Console.ResetColor();
	}

	public static void LogError(string? msg = "Something went wrong")
	{
		LogColor($"\n🛑 {msg}", ConsoleColor.Red);
	}

	public static void LogSuccess(string? msg = "Success!")
	{
		LogColor($"\n✅ {msg}", ConsoleColor.Green);
	}

	public static void LogWarning(string? msg = "Success!")
	{
		LogColor($"\n⚠️  {msg}", ConsoleColor.Yellow);
	}

	public static int GetValidInt(string? label = "Insert a number: ", string? errorMsg = "Wrong input, try again!", int? minValue = 0, int? maxValue = int.MaxValue)
	{
		Console.Write(label);
		int validInt;

		while (!int.TryParse(Console.ReadLine(), out validInt) || validInt < minValue || validInt > maxValue)
		{
			LogError(errorMsg);
			Console.Write(label);
		}


		return validInt;
	}

	/*
	* Will render a menu, based on the input List. The user can scroll throu it with the keyboard
	* when the user makes a selection, it will return the "value" of the selection. Value can be anything from a string to a function
	*/
	public static T KeyboardControllMenu<T>(string title, List<(string label, T value)> options, Directions direction = Directions.row)
	{
		if (options == null || options.Count == 0)
			throw new ArgumentException("Options cannot be empty.");

		bool userSelected = false;
		int activeIdx = 0;

		Console.WriteLine(title);
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
		return KeyboardControllMenu(question, [(label: " YES ", value: true), (label: " NO ", value: false)]);
	}


	// Helper that displays the menu header with a "divider line" at top and bottom, to keep it in sync everywhere
	public static void DisplayMenuHeader(string[] lines, char divider = '-')
	{
		int longestLine = 35;

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

}
