

using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;

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
	{MenuSelections.BuyGroupTickets, "Buy group tickets"}
};

do
{
	DisplayMainMenu();
	string invalidMenuSelectionMessage = $"  Please insert an valid value between 0 - {menuSelections.Count - 1}: ";
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
		case (int)MenuSelections.BuyGroupTickets:
			HandleBuyGroupTickets();
			HandleQuestionAfterCaseHandled(CloseApp);
			break;
	}

} while (!userClosedTheProgram);

void DisplayMainMenu()
{
	Console.Clear();
	DisplayMenuHeader(["MAIN MENU", "(navigate by typing a number)"], '=');
	foreach (var item in menuSelections)
	{
		Console.WriteLine($"{(int)item.Key} - {item.Value}");
	}
	Console.Write("\nNavigate to: ");
}

void DisplayMenuHeader(string[] lines, char divider = '-')
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


#region Ask user for confirmation helpers
T UserConfirmation<T>(string question, List<(string label, T value)> options, Directions direction = Directions.row)
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

bool YesOrNoUserConfirmation(string question)
{
	return UserConfirmation(question, [(label: " YES ", value: true), (label: " NO ", value: false)]);
}
;
#endregion

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

#region Menu handlers
void HandleBuyOneTicket()
{
	Console.Clear();
	DisplayMenuHeader(["BUY ONE TICKET", "(age needed to give you the correct price)"]);
	Console.Write("\nPlease enter your age: ");

	int customerAge = GetValidIntFromUserInput("\nPlease enter a valid age value: ");

	(int price, string description) = GetTicketPriceWithDescriptionByAge(customerAge);

	/*
	* Make sure the client wants to get the ticket.
	*/
	bool userWantsToBuy = YesOrNoUserConfirmation($"\nBased on your age ({customerAge}), your ticket will be {price}Kr - for a \"{description}\" ticket.\nProceed with the payment?\n");

	if (userWantsToBuy)
	{
		Console.WriteLine("\nEnjoy the movie!");
	}
	else
	{
		Console.WriteLine("\nWell, maybe another day, see you!");
	}
}

void HandleBuyGroupTickets()
{
	Console.Clear();
	DisplayMenuHeader(["BUY GROUP TICKETS", "(group size and members age needed)"]);
	Console.Write("Enter the size of the group (min 2 - max 10): ");

	int groupSize = GetValidIntFromUserInput("\nPlease enter a valid group size between 2 - 10: ", extraIntChecks: (i) => i >= 2 && i <= 10);
	Console.WriteLine($"\n======== Group size {groupSize} ========");
	Console.WriteLine("Please enter the age of every person in the group, one by one");

	// After the group size was determined, get ages for each client, set price and description acordingly
	(int age, int price, string description)[] group = new (int age, int price, string description)[groupSize];
	int totalPrice = 0;

	for (int i = 0; i < groupSize; i++)
	{
		Console.Write($"\n-----> Person {i + 1} age: ");
		int customerAge = GetValidIntFromUserInput($"Please enter a valid age value for the person {i + 1}: ");

		(int price, string description) = GetTicketPriceWithDescriptionByAge(customerAge);
		totalPrice += price;

		group[i] = (age: customerAge, price, description);
		Console.WriteLine($"\n+ Person {i + 1} added successfully, subtotal: {totalPrice}Kr\n\n------------------------------");
	}

	// After we have all the ages for each person in the group, log the total and ask the user if they want to proceed
	Console.Clear();
	Console.WriteLine($"\n======== Success ========");
	Console.WriteLine($"Everyone in the group was added!");
	Console.WriteLine("Below you can see the detailes and the total");

	Console.WriteLine("\nCustomer\tAge\tTicket\t\tPrice\n");
	for (int i = 0; i < groupSize; i++)
	{
		(int age, int price, string description) = group[i];

		Console.WriteLine($"{i + 1}\t\t{age}\t{description}\t{price}Kr");
	}
	Console.WriteLine($"\nTotal:\t\t\t\t\t={totalPrice}Kr");

	Console.WriteLine("---------");
	Console.WriteLine($"\nIf you want to continue with the purchase (of {totalPrice}Kr) press Enter, any other key to stop it: ");

	if (Console.ReadKey(true).Key == ConsoleKey.Enter)
	{
		Console.WriteLine("\nEnjoy the movie!");
	}
	else
	{
		Console.WriteLine("\nMaybe next time!");
	}
	Console.WriteLine("\n================");
}
#endregion

#region Enums
enum Directions
{
	row,
	column
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
	BuyGroupTickets = 2,
}
#endregion