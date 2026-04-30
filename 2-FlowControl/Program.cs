

bool userClosedTheProgram = false;

do
{
	DisplayMainMenu();
	int userInput;

	// Try to parse the number the user typed in
	if (!int.TryParse(Console.ReadLine(), out userInput))
	{
		Console.WriteLine("--> Error! Try again, please insert an valid integer!");
		continue;
	}

	switch (userInput)
	{
		case 0:
			Console.WriteLine("\n--> Aplication closing! Thanks for using our app.");
			userClosedTheProgram = true;
			break;
		default:
			Console.WriteLine("\n--> Default input handler.");
			break;
	}

} while (!userClosedTheProgram);

void DisplayMainMenu()
{
	Console.WriteLine("=========");
	Console.WriteLine("Welcome to our main menu. You will navigate using numbers,\nthat corespond to the different actions shown below:");
	Console.WriteLine("=========\n");
	Console.WriteLine("0 - Close App");
	Console.Write("\nYour choice: ");
}