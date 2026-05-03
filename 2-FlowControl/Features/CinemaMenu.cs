using System;
using System.Reflection.Emit;
using _2_FlowControl.Utils;

namespace _2_FlowControl.Features;

public class CinemaMenu(MainMenu mainMenu)
{
	private readonly MainMenu _mainMenu = mainMenu;

	#region Enums
	public enum MenuSelections
	{
		GoBack = 0,
		BuyOneTicket = 1,
		BuyGroupTickets = 2,
		ShowPrices = 3
	}

	public enum PriceCategories
	{
		Youth,
		Pensioner,
		Standard
	}
	#endregion

	#region Constants
	static bool cinemaMenuClosed = false;

	static readonly Dictionary<PriceCategories, (int price, string description, string ageRangeDescription)> prices = new()
	{
		{PriceCategories.Youth, (price: 80, description: "Youth price", ageRangeDescription: "under 20")},
		{PriceCategories.Pensioner, (price: 90, description: "Pensioner price", ageRangeDescription: "over 64")},
		{PriceCategories.Standard, (price: 120, description: "Standard price", ageRangeDescription: "between 20 - 64")},
	};

	static readonly Dictionary<MenuSelections, string> menuSelections = new Dictionary<MenuSelections, string>
	{
		{MenuSelections.GoBack, "<- Back"},
		{MenuSelections.BuyOneTicket, "Buy one ticket"},
		{MenuSelections.BuyGroupTickets, "Buy group tickets"},
		{MenuSelections.ShowPrices, "Show prices"}
	};
	#endregion

	#region Helpers
	private static void Display()
	{
		Console.Clear();
		ConsoleUtils.DisplayMenuHeader(["CINEMA", "(navigate by typing a number)"], '-');
		foreach (var item in menuSelections)
		{
			Console.WriteLine($"{(int)item.Key} - {item.Value}");
		}
		Console.Write("\nNavigate to: ");
	}

	private static (int price, string description, string ageRangeDescription) GetTicketPriceWithDescriptionByAge(int age)
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
	#endregion

	#region Menu handlers
	private static void HandleBuyOneTicket()
	{
		Console.Clear();
		ConsoleUtils.DisplayMenuHeader(["BUY ONE TICKET", "(age needed to give you the correct price)"]);
		Console.Write("\nPlease enter your age: ");

		int customerAge = ConsoleUtils.GetValidIntFromUserInput("\nPlease enter a valid age value: ");

		(int price, string description, string ageRangeDescription) = GetTicketPriceWithDescriptionByAge(customerAge);

		/*
		* Make sure the client wants to get the ticket.
		*/
		bool userWantsToBuy = ConsoleUtils.YesOrNoUserConfirmation($"\nBased on your age ({customerAge}), your ticket will be {price}Kr - for a \"{description}\" ticket.\nProceed with the payment?\n");

		Console.Clear();
		if (userWantsToBuy)
		{
			Console.WriteLine("\nEnjoy the movie!");
		}
		else
		{
			Console.WriteLine("\nWell, maybe another day, see you!");
		}
	}

	private static void HandleBuyGroupTickets()
	{
		Console.Clear();
		ConsoleUtils.DisplayMenuHeader(["BUY GROUP TICKETS", "(group size and members age needed)"]);
		Console.Write("Enter the size of the group (min 2 - max 10): ");

		int groupSize = ConsoleUtils.GetValidIntFromUserInput("\nPlease enter a valid group size between 2 - 10: ", extraIntChecks: (i) => i >= 2 && i <= 10);
		Console.WriteLine($"\n======== Group size {groupSize} ========");
		Console.WriteLine("Please enter the age of every person in the group, one by one");

		// After the group size was determined, get ages for each client, set price and description acordingly
		(int age, int price, string description)[] group = new (int age, int price, string description)[groupSize];
		int totalPrice = 0;

		for (int i = 0; i < groupSize; i++)
		{
			Console.Write($"\n-----> Person {i + 1} age: ");
			int customerAge = ConsoleUtils.GetValidIntFromUserInput($"Please enter a valid age value for the person {i + 1}: ");

			(int price, string description, string ageRangeDescription) = GetTicketPriceWithDescriptionByAge(customerAge);
			totalPrice += price;

			group[i] = (age: customerAge, price, description);
			Console.WriteLine($"\n+ Person {i + 1} added successfully, subtotal: {totalPrice}Kr\n\n------------------------------");
		}

		// After we have all the ages for each person in the group, log the total and ask the user if they want to proceed
		Console.Clear();
		ConsoleUtils.DisplayMenuHeader(["Success!", "Everyone in the group was added.", "(detailed ticket below)"]);


		Console.WriteLine("\nCustomer\tAge\tTicket\t\tPrice\n");
		for (int i = 0; i < groupSize; i++)
		{
			(int age, int price, string description) = group[i];

			Console.WriteLine($"{i + 1}\t\t{age}\t{description}\t{price}Kr");
		}
		Console.WriteLine($"\nTotal:\t\t\t\t\t{totalPrice}Kr");

		bool userWantsToBuy = ConsoleUtils.YesOrNoUserConfirmation($"\n\nDo you want to proceed with the pruchase? (of {totalPrice}Kr)");

		Console.Clear();
		if (userWantsToBuy)
		{
			Console.WriteLine("\nEnjoy the movie!");
		}
		else
		{
			Console.WriteLine("\nMaybe next time!");
		}

	}

	private void HandleShowPrices()
	{
		Console.Clear();
		ConsoleUtils.DisplayMenuHeader(["Ticket prices", "(based on age)"]);

		Console.WriteLine("\n\nPrice\tCategory\n");
		foreach (var priceCategory in prices)
		{
			Console.WriteLine($"{priceCategory.Value.price}Kr\t{priceCategory.Value.description} ({priceCategory.Value.ageRangeDescription})");
		}

		bool closeApp = ConsoleUtils.UserConfirmation("", [(label: " Go back ", value: false), (label: " Close app ", value: true)]);
		if (closeApp)
			CloseApp();
	}
	private static void GoToMainMenu()
	{
		cinemaMenuClosed = true;
	}

	private void CloseApp()
	{
		_mainMenu.CloseApp();
		cinemaMenuClosed = true;
	}
	#endregion

	public void Run()
	{
		Display();

		do
		{
			Display();

			string invalidMenuSelectionMessage = $"  Please insert an valid value between 0 - {menuSelections.Count - 1}: ";
			int userMenuSelection = ConsoleUtils.GetValidIntFromUserInput(invalidMenuSelectionMessage, extraIntChecks: (i) => i <= menuSelections.Count - 1);

			switch (userMenuSelection)
			{
				case (int)MenuSelections.GoBack:
					GoToMainMenu();
					break;
				case (int)MenuSelections.BuyOneTicket:
					HandleBuyOneTicket();
					ConsoleUtils.HandleQuestionAfterCaseHandled(CloseApp, GoToMainMenu);
					break;
				case (int)MenuSelections.BuyGroupTickets:
					HandleBuyGroupTickets();
					ConsoleUtils.HandleQuestionAfterCaseHandled(CloseApp, GoToMainMenu);
					break;
				case (int)MenuSelections.ShowPrices:
					HandleShowPrices();
					break;
			}

		} while (!cinemaMenuClosed);

	}
}
