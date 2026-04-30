

bool userClosedTheProgram = false;
var prices = new Dictionary<PriceCategories, (int price, string description)>
{
	{PriceCategories.Youth, (price: 80, description: "Youth price")},
	{PriceCategories.Pensioner, (price: 90, description: "Pensioner price")},
	{PriceCategories.Standard, (price: 120, description: "Standard price")},
};

do
{
	DisplayMainMenu();
	int userInput;

	// Try to parse the number the user inserted, show an error if not valid
	if (!int.TryParse(Console.ReadLine(), out userInput))
	{
		Console.WriteLine("--> Error! Try again, please insert an valid integer!");
		continue;
	}

	switch (userInput)
	{
		case 0:
			CloseApp();
			break;
		case 1:
			HandleBuyOneTicket();
			HandleQuestionAfterCaseHandled(CloseApp);
			break;
		default:
			Console.WriteLine("\n--> Default input handler.");
			break;
	}

} while (!userClosedTheProgram);


void DisplayMainMenu()
{
	Console.Clear();
	Console.WriteLine("=========");
	Console.WriteLine("Welcome to our main menu. You will navigate using numbers,\nthat corespond to the different actions shown below:");
	Console.WriteLine("=========\n");
	Console.WriteLine("0 - Close App");
	Console.WriteLine("1 - Buy one ticket");
	Console.Write("\nYour choice: ");
}

void CloseApp()
{
	Console.Clear();
	userClosedTheProgram = true;
	Console.WriteLine("\n--> Aplication closing! Thanks for using our app.");
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