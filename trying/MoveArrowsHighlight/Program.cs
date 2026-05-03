void MenuUpDown()
{
	bool userSelected = false;

	int activeIndex = 0;
	string[] menuItems = ["Item 1", "Item 2", "Item 3", "Close"];



	while (!userSelected)
	{
		Console.Clear();
		Console.WriteLine("------------");
		Console.WriteLine("    Menu    ");
		Console.WriteLine("------------");
		foreach (string menuItem in menuItems)
		{
			if (menuItems[activeIndex] == menuItem)
			{
				Console.BackgroundColor = ConsoleColor.DarkGray;
				Console.ForegroundColor = ConsoleColor.White;

				Console.WriteLine($" > {menuItem} ");
			}
			else
			{
				Console.ResetColor();
				Console.WriteLine(menuItem);
			}
			Console.ResetColor();
		}

		ConsoleKey userPressedKey = Console.ReadKey(true).Key;
		if (userPressedKey == ConsoleKey.Enter)
			userSelected = true;
		else if (userPressedKey == ConsoleKey.UpArrow)
		{
			activeIndex = activeIndex - 1 < 0 ? menuItems.Length - 1 : activeIndex - 1;
		}
		else if (userPressedKey == ConsoleKey.DownArrow)
		{
			activeIndex = (activeIndex + 1) % menuItems.Length;
		}
	}
	Console.WriteLine($"Oh wow, you selected {menuItems[activeIndex]}");
}
;
MenuUpDown();
// YesAndNo();
void YesAndNo()
{
	bool userSelected = false;
	string[] selections = [" YES ", " NO "];
	int activeIdx = 0;
	Console.WriteLine("THis would be some imporntatn Text,\n Wa wa wi wa!");
	do
	{
		// Console.Clear();
		Console.SetCursorPosition(0, Console.CursorTop);

		foreach (string selection in selections)
		{
			if (selections[activeIdx] == selection)
			{
				Console.BackgroundColor = ConsoleColor.Gray;
				Console.ForegroundColor = ConsoleColor.Black;
			}
			else
				Console.ResetColor();

			Console.Write(selection);
		}
		Console.ResetColor();
		ConsoleKey userKeyPressed = Console.ReadKey(true).Key;

		if (userKeyPressed == ConsoleKey.Enter)
		{
			userSelected = true;
		}
		else if (userKeyPressed == ConsoleKey.LeftArrow)
		{
			activeIdx = Math.Abs(activeIdx - 1);
		}
		else if (userKeyPressed == ConsoleKey.RightArrow)
		{
			activeIdx = (activeIdx + 1) % selections.Length;
		}

	} while (!userSelected);
	Console.WriteLine($"\nOh wow, you selected {selections[activeIdx]}");
}