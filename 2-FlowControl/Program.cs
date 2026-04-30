

bool userClosedTheProgram = false;
var prices = new Dictionary<PriceCategories, (int price, string description)>
{
	{PriceCategories.Youth, (price: 80, description: "Youth price")},
	{PriceCategories.Pensioner, (price: 90, description: "Pensioner price")},
	{PriceCategories.Standard, (price: 120, description: "Standard price")},
};

var menuSelections = new Dictionary<MenuSelections, string>
{
	{MenuSelections.CloseApp, "Close App"},
	{MenuSelections.BuyOneTicket, "Buy one ticket"},
	{MenuSelections.BuyMultipleTickets, "Buy multiple tickets"}
};

do
{
	DisplayMainMenu();
	int userMenuSelection;

	// Try to parse the number the user inserted, show an error if not valid
	while (!int.TryParse(Console.ReadLine(), out userMenuSelection) || userMenuSelection > menuSelections.Count - 1)
	{
		Console.Write($"--> Error! Try again,\n please insert an valid value between 0 - {menuSelections.Count - 1}: ");
	}

	switch (userMenuSelection)
	{
		case (int)MenuSelections.CloseApp:
			CloseApp();
			break;
		case (int)MenuSelections.BuyOneTicket:
			HandleBuyOneTicket();
			HandleQuestionAfterCaseHandled(CloseApp);
			break;
		case (int)MenuSelections.BuyMultipleTickets:
			Console.WriteLine("Buy multiple tickets!");
			Console.ReadLine();
			break;
		default:
			Console.WriteLine("\n--> Default input handler.");
			break;
	}

} while (!userClosedTheProgram);


void DisplayMainMenu()
{
	Console.Clear();
	Console.WriteLine("========= Main menu =========");
	Console.WriteLine("\nYou will navigate using numbers,\nthat corespond to the different actions shown below:\n");
	Console.WriteLine("=============================\n");
	foreach (var item in menuSelections)
	{
		Console.WriteLine($"{(int)item.Key} - {item.Value}");
	}
	Console.Write("\nYour choice: ");
}

void CloseApp()
{
	Console.Clear();
	userClosedTheProgram = true;
	Console.WriteLine("\n--> Aplication closed! Thanks for using our app.");
}

// Asks the user if they want to perform more actions after an action was completed, or just quit the app
void HandleQuestionAfterCaseHandled(Action closeApp)
{
	Console.WriteLine("\n--------- Your action is completed.");
	Console.WriteLine("\nPress Esc, to close the app. \nPress anything else to go back to main menu.");

	ConsoleKey userInput = Console.ReadKey(true).Key;

	if (userInput == ConsoleKey.Escape)
	{
		closeApp();
	}

}

(int price, string description) GetTicketPriceWithDescriptionByAge(int age)
{
	if (age < 20)
	{
		return prices[PriceCategories.Youth];
	}
	else if (age > 64)
	{
		return prices[PriceCategories.Pensioner];
	}

	return prices[PriceCategories.Standard];
}

#region Case 1 handler
void HandleBuyOneTicket()
{
	int customerAge;
	bool validAgeInserted = false;
	Console.Clear();
	Console.WriteLine("\n--------- Buy one ticket");
	Console.WriteLine("In order to give you the correct price, we need your age.");
	Console.Write("\nPlease enter your age: ");
	do
	{
		if (!int.TryParse(Console.ReadLine(), out customerAge))
		{
			Console.Write("\nPlease enter a valid age value: ");
			continue;
		}

		(int price, string description) = GetTicketPriceWithDescriptionByAge(customerAge);

		Console.WriteLine($"\nBased on your age ({customerAge}), your ticket will be {price}Kr - for a \"{description}\" ticket.");

		/*
		* Make sure the client wants to get the ticket.
		*/
		Console.WriteLine($"\n-> Do you want to proceed with the purchase? You owe us: {price}Kr.\nPress enter to continue with the purchase, anything else to exit.");

		if (Console.ReadKey(true).Key == ConsoleKey.Enter)
		{
			Console.WriteLine("\nEnjoy the movie!");
		}
		else
		{
			Console.WriteLine("\nWell, maybe another day, see you!");
		}

		validAgeInserted = true;
	} while (!validAgeInserted);
}
#endregion

enum PriceCategories
{
	Youth,
	Pensioner,
	Standard
}

enum MenuSelections
{
	CloseApp = 0,
	BuyOneTicket = 1,
	BuyMultipleTickets = 2,
}