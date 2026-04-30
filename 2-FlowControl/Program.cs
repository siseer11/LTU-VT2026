

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
	string invalidMenuSelectionMessage = $"-> Error! Try again, please insert an valid value between 0 - {menuSelections.Count - 1}: ";
	int userMenuSelection = GetValidIntFromUserInput(invalidMenuSelectionMessage, extraIntChecks: (i) => i <= menuSelections.Count - 1);

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

int GetValidIntFromUserInput(string errorMessage = "\nPlease enter a valid integer: ", bool allowNegativeValues = false, Func<int, bool>? extraIntChecks = null)
{
	int result;
	bool validIntInserted = false;
	do
	{
		if (int.TryParse(Console.ReadLine(), out result) &&
				allowNegativeValues ? true : result > 0 &&
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

void HandleBuyOneTicket()
{
	Console.Clear();
	Console.WriteLine("\n--------- Buy one ticket");
	Console.WriteLine("In order to give you the correct price, we need your age.");
	Console.Write("\nPlease enter your age: ");

	int customerAge = GetValidIntFromUserInput("\nPlease enter a valid age value: ");

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
}

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