using System;
using GarageAppV2.Shared;
namespace GarageAppV2.ConsoleUI;

public record KeyboardControlFormParams(
	FormOptionsType Options,
	Action? DisplayHeader,
	int HeaderNumberOfLines = 0,
	string? GoBackButtonLabel = null,
	Action? HandleGoBack = null
);
public partial class ConsoleUtils
{
	/*
	*	Homemade form, that takes in options (aka fields), renders them, user can navigate to each field
	* type a value in each. Submit the "form" and the form will return the same options it got in, but with the value field
	* filled with the user typed data
	*/
	public static FormOptionsType KeyboardControlForm(KeyboardControlFormParams formParams)
	{
		var (options, DisplayHeader, headerNumberOfLines, goBackButtonLabel, HandleGoBack) = formParams;
		string[] errors = [];
		bool userClosedIt = false;
		bool inEditMode = false;
		int activeOne = 0;

		#region Constants
		const string _submitFieldLabel = "Submit";
		string[] actionButtons = ["Submit"];

		if (goBackButtonLabel is not null && HandleGoBack is not null)
		{
			actionButtons = [.. actionButtons, goBackButtonLabel];
		}

		int numberOfOptions = options.Length;
		int numberOfButtons = actionButtons.Length;
		int spaceBetweenOptionsAndButtons = 2;
		int totalNumberOfRows = numberOfButtons + numberOfOptions + spaceBetweenOptionsAndButtons;

		// extra keys that we allow the user to type and add them to the value
		ConsoleKey[] _extraInputValidKeys = [ConsoleKey.Spacebar, ConsoleKey.OemComma, ConsoleKey.OemPeriod, ConsoleKey.Subtract];
		#endregion

		#region Helpers
		void SetCursorAtCurrentActiveOne()
		{
			int leftPosition = options[activeOne].label.Length + options[activeOne].value.Length;
			int spaceBetweenLabelAndValue = 1;
			int requiredAsterixSpace = options[activeOne].required ? 1 : 0;
			Console.SetCursorPosition(leftPosition + spaceBetweenLabelAndValue + requiredAsterixSpace, activeOne + headerNumberOfLines - Console.WindowTop);
		}

		void HandleIncreaseActiveIndex()
		{
			if (activeOne == numberOfOptions - 1) // If we are at the end of the options, jump over the gap to the buttons
				activeOne += 1 + spaceBetweenOptionsAndButtons;
			else
				activeOne = (activeOne + 1) % totalNumberOfRows;
		}

		void HandleDecreaseActiveIndex()
		{
			if (activeOne == numberOfOptions + spaceBetweenOptionsAndButtons) // If we are at the end of the buttons, jump over the gap to the options
				activeOne -= spaceBetweenOptionsAndButtons + 1;
			else
				activeOne = activeOne - 1 < 0 ? totalNumberOfRows - 1 : activeOne - 1;
		}

		#endregion

		do
		{
			#region Render
			#region Top part
			Console.Clear();

			if (DisplayHeader is not null)
				DisplayHeader();
			Console.CursorVisible = false;

			for (int i = 0; i < options.Length; i++)
			{
				string labelAtIndex = $"{(options[i].required ? "*" : "")}{options[i].label}";
				if (i == activeOne)
				{
					Console.BackgroundColor = ConsoleColor.White;
					Console.ForegroundColor = ConsoleColor.Black;
					Console.Write(labelAtIndex);
					Console.ResetColor();
				}
				else
				{
					Console.Write(labelAtIndex);
				}
				Console.Write(" ");
				Console.Write($"{options[i].value}\n");
			}

			for (int i = 0; i < spaceBetweenOptionsAndButtons; i++)
			{
				Console.WriteLine();
			}

			for (int i = 0; i < actionButtons.Length; i++)
			{
				if (i + spaceBetweenOptionsAndButtons + numberOfOptions == activeOne)
				{
					Console.BackgroundColor = ConsoleColor.White;
					Console.ForegroundColor = ConsoleColor.Black;
					Console.WriteLine(actionButtons[i]);
					Console.ResetColor();
				}
				else
				{
					Console.WriteLine(actionButtons[i]);
				}
			}
			#endregion
			#region Footer
			Console.WriteLine("\n-----------------------------------");
			if (errors.Length > 0)
			{
				LogError($"Fix these ({errors.Length}) errors before submiting");
				foreach (string error in errors)
				{
					LogColor(error, ConsoleColor.Red);
				}
				Console.WriteLine("");
			}
			LogColor("(🤖 How to use:\n- Press enter to start/end editing\n- Press Tab/Arrows to navigate\n- (*) Marked fields are required)", ConsoleColor.DarkGray);

			#endregion
			#endregion

			ConsoleKeyInfo keyPressedInfo = Console.ReadKey(true);
			ConsoleKey keyPressed = keyPressedInfo.Key;

			if (keyPressed == ConsoleKey.Enter)
			{
				if (activeOne < numberOfOptions) // one of the input fields was pressed
				{
					inEditMode = true;
					SetCursorAtCurrentActiveOne();
				}
				else // one of the action buttons was pressed
				{
					int buttonIndex = activeOne - (numberOfOptions + spaceBetweenOptionsAndButtons);
					Console.WriteLine($"buttonIndex: {buttonIndex}");

					// Handle Submit
					if (actionButtons[buttonIndex] == _submitFieldLabel)
					{
						errors = []; // clear the errors
												 // check each of the fields, generate errors if needed
						for (int i = 0; i < options.Length; i++)
						{
							var option = options[i];
							if (option.valueCheck is not null)
							{
								(bool passed, string? errorMessage) = option.valueCheck(option.value);

								errorMessage ??= $"Input invalid for {option.label} field!";

								if (!passed)
									errors = [.. errors, errorMessage];
							}
							else if (option.required && string.IsNullOrEmpty(option.value))
							{
								errors = [.. errors, $"{option.label} field is required!"];
							}
						}

						// All the inputs are filled correctly, return them
						if (errors.Length == 0)
						{
							return options;
						}

					}
					else if (actionButtons[buttonIndex] == goBackButtonLabel)
					{
						HandleGoBack!();
					}
				}
			}
			else if (forwardKeys.Contains(keyPressed))
			{
				HandleIncreaseActiveIndex();
			}
			else if (backwardKeys.Contains(keyPressed))
			{
				HandleDecreaseActiveIndex();
			}

			/*
			* User pressed enter on a field, 
			* take his input and update the value of that specific field
			*/
			while (inEditMode)
			{
				Console.CursorVisible = true;
				ConsoleKeyInfo keyPressedWhileInEdditInfo = Console.ReadKey(true);
				ConsoleKey keyPressedWhileInEdit = keyPressedWhileInEdditInfo.Key;

				if (keyPressedWhileInEdit == ConsoleKey.Enter)
				{
					inEditMode = false;
				}
				else if (keyPressedWhileInEdit == ConsoleKey.Tab)
				{
					inEditMode = false;
					HandleIncreaseActiveIndex();
				}
				else if (keyPressedWhileInEdit == ConsoleKey.Backspace && options[activeOne].value.Length > 0)
				{
					options[activeOne].value = options[activeOne].value[..^1];

					Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
					Console.Write(" ");
					Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
				}
				else if (char.IsLetterOrDigit(keyPressedWhileInEdditInfo.KeyChar) || _extraInputValidKeys.Contains(keyPressedWhileInEdit))
				{
					SetCursorAtCurrentActiveOne();
					options[activeOne].value += keyPressedWhileInEdditInfo.KeyChar;
					Console.Write(keyPressedWhileInEdditInfo.KeyChar);
				}
			}
		} while (!userClosedIt && AppStore.AppRunning);


		return options;
	}
}
