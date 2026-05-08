

using _2_FlowControl;


App app = new();
app.Run();



// // Asks the user if they want to perform more actions after an action was completed, or just quit the app
// void HandleQuestionAfterCaseHandled(Action closeApp)
// {
// 	Console.WriteLine("\n--------- Your action is completed.");
// 	Console.WriteLine("\nPress Esc, to close the app. \nPress anything else to go back to main menu.");

// 	ConsoleKey userInput = Console.ReadKey(true).Key;

// 	if (userInput == ConsoleKey.Escape)
// 	{
// 		closeApp();
// 	}

// }

// (int price, string description) GetTicketPriceWithDescriptionByAge(int age)
// {
// 	if (age < 20)
// 	{
// 		return prices[PriceCategories.Youth];
// 	}
// 	else if (age > 64)
// 	{
// 		return prices[PriceCategories.Pensioner];
// 	}

// 	return prices[PriceCategories.Standard];
// }



